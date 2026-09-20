using Advertisements.Models.ClientModels;
using Advertisements.Models.WebhookMessageModels;
using Advertisements.Services;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace Advertisements.Controllers
{
    public class WebhookMessagesController : Controller
    {
        private readonly IReposCampaigns reposCampaigns;
        private readonly IServiceWebhookMessages serviceWebhookMessages;
        private readonly IReposClients reposClients;
        private readonly IReposMetrics reposMetrics;

        public WebhookMessagesController(IReposCampaigns reposCampaigns, IServiceWebhookMessages serviceWebhookMessages, IReposClients reposClients,
            IReposMetrics reposMetrics)
        {
            this.reposCampaigns = reposCampaigns;
            this.serviceWebhookMessages = serviceWebhookMessages;
            this.reposClients = reposClients;
            this.reposMetrics = reposMetrics;
        }

        [HttpGet]
        public async Task<IActionResult> Send(int campaignId)
        {
            var campaign = await reposCampaigns.GetById(campaignId);

            if (campaign is null) return NotFound();

            var webhookMessage = new WebhookMessage
            {
                CampaignId = campaignId,
                WebhookURL = campaign.WebhookURL,
            };            

            return View(webhookMessage);
        }

        [HttpPost]
        public async Task<IActionResult> Send(WebhookMessage webhookMessage)
        {
            var campaign = await reposCampaigns.GetById(webhookMessage.CampaignId);
            if (campaign is null)
            {
                return RedirectToAction("NotFound", "Home");
            }

            var client = await reposClients.GetById(campaign.ClientId);
            if (client is null)
            {
                return RedirectToAction("NotFound", "Home");
            }

            var webhookURL = webhookMessage.WebhookURL;

            var imageURL = "";

            if (webhookMessage.ImageURL is not null)
            {
                imageURL = webhookMessage.ImageURL;
            }

            if (webhookMessage.WebhookMessageType == Models.Enums.WebhookMessageType.CampaignStart)
            {
                var response = await serviceWebhookMessages.SendCampaignStartMessage(webhookURL, imageURL, campaign, client);
                if (response.IsSuccessStatusCode)
                {
                    TempData["Success"] = "Campaign Start Message sent successfully";
                }
                else
                {
                    TempData["Error"] = $"Campaign Start Message Error: {response.StatusCode}";
                }
            }
            else if (webhookMessage.WebhookMessageType == Models.Enums.WebhookMessageType.CCStart)
            {
                var response = await serviceWebhookMessages.SendCCStartMessage(webhookURL, imageURL, campaign);
                if (response.IsSuccessStatusCode)
                {
                    TempData["Success"] = "Content Creation Start Message sent successfully";
                }
                else
                {
                    TempData["Error"] = $"Content Creation Start Message Error: {response.StatusCode}";
                }
            }
            else if (webhookMessage.WebhookMessageType == Models.Enums.WebhookMessageType.CCEnd)
            {
                var response = await serviceWebhookMessages.SendCCEndMessage(webhookURL, imageURL, campaign);
                if (response.IsSuccessStatusCode)
                {
                    TempData["Success"] = "Content Creation End Message sent successfully";
                }
                else
                {
                    TempData["Error"] = $"Content Creation End Message Error: {response.StatusCode}";
                }
            }
            else if (webhookMessage.WebhookMessageType == Models.Enums.WebhookMessageType.CampaignEnd)
            {
                var metrics = await reposMetrics.GetByCampaign(webhookMessage.CampaignId);
                var winningMetrics = metrics.Where(m =>
                    (m.MetricStatus == Models.Enums.MetricStatus.Accepted ||
                    m.MetricStatus == Models.Enums.MetricStatus.Settled)
                    && m.CalculatedCreatorPayout > 0
                ).ToList();

                if (winningMetrics.Count == 0)
                {
                    TempData["Alert"] = "No Winning Metrics were found. Message not sent";
                    return RedirectToAction("Send", new { campaignId = webhookMessage.CampaignId });
                }

                var response = await serviceWebhookMessages.SendCampaignEndMessage(webhookURL, imageURL, campaign, winningMetrics);
                if (response.IsSuccessStatusCode)
                {
                    TempData["Success"] = "Campaign End Message sent successfully";
                }
                else
                {
                    TempData["Error"] = $"Campaign End Message Error: {response.StatusCode}";
                }
            }

            return RedirectToAction("Send", new { campaignId = webhookMessage.CampaignId });
        }



    }
}

