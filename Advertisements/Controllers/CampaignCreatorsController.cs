using Advertisements.Models;
using Advertisements.Models.CampaignCreatorModels;
using Advertisements.Models.CampaignModels;
using Advertisements.Models.MetricModels;
using Advertisements.Services;
using ClosedXML.Excel;
using Dapper;
using DocumentFormat.OpenXml.EMMA;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Diagnostics;

namespace Advertisements.Controllers
{
    public class CampaignCreatorsController : Controller
    {
        private readonly IReposCampaignCreators reposCampaignCreators;
        private readonly IReposCampaigns reposCampaigns;

        public CampaignCreatorsController(IReposCampaignCreators reposCampaignCreators, IReposCampaigns reposCampaigns)
        {
            this.reposCampaignCreators = reposCampaignCreators;
            this.reposCampaigns = reposCampaigns;
        }
        public async Task<IActionResult> Index()
        {
            var campaignCreators = await reposCampaignCreators.GetAll();

            return View(campaignCreators);
        }

        public async Task<IActionResult> IndexByCampaign(int campaignId)
        {
            var campaign = await reposCampaigns.GetById(campaignId);
            if (campaign == null)
            {
                return RedirectToAction("NotFound", "Home");
            }

            var model = new IndexCampaignCreatorsVM {
                CampaignId = campaignId,
                CampaignCreators = await reposCampaignCreators.GetByCampaign(campaignId)
            };

            return View(model);
        }

        public async Task<IActionResult> CopyToClipBoard(int campaignId)
        {
            var campaign = await reposCampaigns.GetById(campaignId);
            if (campaign == null)
            {
                return RedirectToAction("NotFound", "Home");
            }

            var campaignCreators = await reposCampaignCreators.GetByCampaign(campaignId);
            var creatorEmails = campaignCreators.Select(x => x.CreatorEmail);

            return Json(new { creatorEmails = creatorEmails });
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CampaignCreator campaignCreator)
        {
            if (!ModelState.IsValid)
            {
                return View(campaignCreator);
            }

            await reposCampaignCreators.Create(campaignCreator);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult ImportExcel()
        {
            return View();
        }        

        [HttpGet]
        public async Task<IActionResult> Update(int campaignCreatorId)
        {
            var campaignCreator = await reposCampaignCreators.GetById(campaignCreatorId);
            if (campaignCreator is null)
            {
                return RedirectToAction("NotFound", "Home");
            }

            return View(campaignCreator);
        }

        [HttpPost]
        public async Task<IActionResult> Update(CampaignCreator campaignCreator)
        {
            if (!ModelState.IsValid)
            {
                return View(campaignCreator);
            }

            await reposCampaignCreators.Update(campaignCreator);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int campaignCreatorId)
        {
            var campaignCreator = await reposCampaignCreators.GetById(campaignCreatorId);
            if (campaignCreator is null)
            {
                return RedirectToAction("NotFound", "Home");
            }

            return View(campaignCreator);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteCampaignCreator(int campaignCreatorId)
        {

            var campaignCreator = await reposCampaignCreators.GetById(campaignCreatorId);
            if (campaignCreator is null)
            {
                return RedirectToAction("NotFound", "Home");
            }

            try
            {
                await reposCampaignCreators.Delete(campaignCreatorId);
                TempData["Success"] = "The enrollment was deleted successfully.";
            }
            catch (SqlException)
            {
                TempData["Error"] = "Error deleting the enrollment " + campaignCreatorId + ". It might be linked to other records.";
            }
            catch (Exception)
            {
                TempData["Error"] = "Unexpected Error Deleting the enrollment " + campaignCreatorId;
            }

            return RedirectToAction("IndexByCampaign", new { campaignId = campaignCreator.CampaignId });
        }

    }
}

