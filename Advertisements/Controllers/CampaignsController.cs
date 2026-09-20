
using Advertisements.Models.ClientModels;
using Advertisements.Models.CampaignModels;
using Advertisements.Models.Enums;
using Advertisements.Services;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using Microsoft.AspNetCore.Mvc.Rendering;
using Advertisements.Models.MetricModels;
using DocumentFormat.OpenXml.EMMA;
using QuestPDF.Fluent;

namespace Advertisements.Controllers
{
    public class CampaignsController : Controller
    {
        private readonly IReposCampaigns reposCampaigns;
        private readonly IReposClients reposClients;
        private readonly IReposMetrics reposMetrics;

        public CampaignsController(IReposCampaigns reposCampaigns, IReposClients reposClients, IReposMetrics reposMetrics)
        {
            this.reposCampaigns = reposCampaigns;
            this.reposClients = reposClients;
            this.reposMetrics = reposMetrics;

        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var campaigns = await reposCampaigns.GetAll();

            return View(campaigns);
        }

        private async Task<IEnumerable<SelectListItem>> GetClientNamesItems()
        {
            var clients = await reposClients.GetAll();
            return clients.Select(x => new SelectListItem(x.Name, x.EntityId.ToString()));
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var model = new CreateCampaignVM();
            model.ClientNames = await GetClientNamesItems();

            model.ComissionRate = 0.10m;

            model.StartDate = DateTime.Now.Date;
            model.EndDate = DateTime.Now.Date;

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateCampaignVM model)
        {
            if (!ModelState.IsValid)
            {
                model.ClientNames = await GetClientNamesItems();
                return View(model);
            }

            var client = await reposClients.GetById(model.ClientId);
            if (client == null)
            {
                return RedirectToAction("NotFound", "Home");
            }

            model.CreationDate = DateTime.Now;

            model.CreatorBudget = model.ClientBudget * (1m - model.ComissionRate);
            model.CreatorPayoutRate = model.ClientPaymentRate * (1m - model.ComissionRate);
            model.ClientPayment = 0m;
            model.CreatorPayout = 0m;
            model.Stage = Stage.Created;
            model.SalesGrowth = 0m;
            model.CampaignStatus = CampaignStatus.Pending;

            await reposCampaigns.Create(model);

            return RedirectToAction("Index");
        }        

        [HttpGet]
        public async Task<IActionResult> Update(int campaignId)
        {
            var campaign = await reposCampaigns.GetById(campaignId);
            if (campaign is null)
            {
                return RedirectToAction("NotFound", "Home");
            }

            var model = EditViewModels.From(campaign);

            model.ClientNames = await GetClientNamesItems();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Update(Campaign campaign)
        {
            if (!ModelState.IsValid)
            {
                return View(campaign);
            }

            campaign.CreatorBudget = campaign.ClientBudget * (1m - campaign.ComissionRate);
            campaign.CreatorPayoutRate = campaign.ClientPaymentRate * (1m - campaign.ComissionRate);

            await reposCampaigns.Update(campaign);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> ExportPDFReport(int campaignId)
        {
            var campaign = await reposCampaigns.GetById(campaignId);
            if (campaign is null)
            {
                return RedirectToAction("NoEncontrado", "Home");
            }

            var allMetrics = await reposMetrics.GetByCampaign(campaignId);

            var acceptedMetrics = allMetrics.Where(m => (m.MetricStatus == MetricStatus.Accepted) || (m.MetricStatus == MetricStatus.Settled));
            if (!acceptedMetrics.Any())
            {
                TempData["Error"] = $"No metrics were found.";
                return RedirectToAction("Index");
            }

            var numCreators = acceptedMetrics.Count();

            var totalViews = acceptedMetrics.Sum(m => m.NumViews);

            var extraViews = acceptedMetrics.Sum(m => m.NumViews % 1000);

            var totalEngagement = acceptedMetrics.Sum(m => (
                m.NumLikes + m.NumComments + m.NumShares + m.NumSaves
            ));

            var engagementRate = totalViews > 0
                ? (decimal)totalEngagement / totalViews
                : 0;

            var expectedClientPayment = (totalViews / 1000) * campaign.ClientPaymentRate;

            var extraViewsOptimization = (extraViews / 1000) * campaign.ClientPaymentRate;

            var agencyOptimization = campaign.ClientBudget - expectedClientPayment;

            var totalOptimization = extraViewsOptimization + agencyOptimization;

            IEnumerable<ReportMetricDto> reportMetrics = acceptedMetrics
                .Select(m => new ReportMetricDto
                {
                    CreatorName = m.CreatorName,
                    VideoLink = m.VideoLink,
                    NumViews = m.NumViews,
                    Engagement = m.NumLikes + m.NumComments + m.NumShares + m.NumSaves
                });

            var data = new CampaignReportData(
                ReportDate: DateTime.Now,
                ClientName: campaign.ClientName,
                StartDate: campaign.StartDate,
                EndDate: campaign.EndDate,
                Platform: campaign.Platform,
                NumCreators: numCreators,
                TotalViews: totalViews,
                TotalEngagement: totalEngagement,
                EngagementRate: engagementRate,
                ClientPaymentRate: campaign.ClientPaymentRate,
                ClientBudget: campaign.ClientBudget,
                ExpectedClientPayment: expectedClientPayment,
                ClientPayment: campaign.ClientPayment,
                ExtraViewsOptimization: extraViewsOptimization,
                AgencyOptimization: agencyOptimization,
                TotalOptimization: totalOptimization,
                ReportMetrics: reportMetrics
            );

            var document = new CampaignReport(data);

            byte[] pdfBytes = document.GeneratePdf();

            return File(pdfBytes, "application/pdf", $"Reporte Campaña Reapify {campaign.ClientName} {DateTime.Now:dd-MM-yyyy}.pdf");
        }

    }
}

