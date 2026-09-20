using Advertisements.Models.MetricModels;
using Advertisements.Services;
using ClosedXML.Excel;
using Dapper;
using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.EMMA;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Diagnostics;

namespace Advertisements.Controllers
{
    public class MetricsController : Controller
    {
        private readonly IReposMetrics reposMetrics;
        private readonly IReposCampaigns reposCampaigns;
        private readonly IReposCreators reposCreators;

        private readonly IReposCampaignCreators reposCampaignCreators;

        public MetricsController(IReposMetrics reposMetrics, IReposCampaigns reposCampaigns, IReposCreators reposCreators, IReposCampaignCreators reposCampaignCreators)
        {
            this.reposMetrics = reposMetrics;
            this.reposCampaigns = reposCampaigns;
            this.reposCreators = reposCreators;

            this.reposCampaignCreators = reposCampaignCreators;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var metrics = await reposMetrics.GetAll();
            return View(metrics);
        }

        [HttpGet]
        public async Task<IActionResult> IndexByCampaign(int campaignId)
        {
            var campaign = await reposCampaigns.GetById(campaignId);
            if (campaign == null)
            {
                return RedirectToAction("NotFound", "Home");
            }
            
            var model = new IndexMetricsVM
            {
                CampaignId = campaignId,
                Metrics = await reposMetrics.GetByCampaign(campaignId)
            };

            return View(model);
        }

        private async Task<IEnumerable<SelectListItem>> GetCampaignIdsItems()
        {
            var clients = await reposCampaigns.GetAll();
            return clients.Select(x => new SelectListItem(x.ProductId.ToString() + ", " + x.ClientName.ToString(), x.ProductId.ToString()));
        }

        private async Task<IEnumerable<SelectListItem>> GetCreatorNamesItems()
        {
            var creators = await reposCreators.GetAll();
            return creators.Select(x => new SelectListItem(x.Name.ToString(), x.EntityId.ToString()));
        }


        [HttpGet]
        public async Task<IActionResult> Create(int campaignCreatorId)
        {
            var campaignCreator = await reposCampaignCreators.GetById(campaignCreatorId);
            if (campaignCreator == null)
            {
                return RedirectToAction("NotFound", "Home");
            }

            var model = new Metric();
            model.SubmissionTimestamp = DateTime.Now.Date;
            model.CampaignCreatorId = campaignCreatorId;

            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> Create(CreateMetricVM model)
        {
            if (!ModelState.IsValid)
            {
                model.CreatorNames = await GetCreatorNamesItems();
                return View(model);
            }

            var campaignCreator = await reposCampaignCreators.GetById(model.CampaignCreatorId);
            if (campaignCreator == null)
            {
                return RedirectToAction("NotFound", "Home");
            }

            model.CreationDate = DateTime.Now;
            await reposMetrics.Create(model);

            return RedirectToAction("IndexByCampaign", new { campaignId = campaignCreator.CampaignId });
        }

        [HttpGet]
        public IActionResult ImportExcel(int campaignId)
        {
            var model = new ImportMetricsExcelVM();
            model.CampaignId = campaignId;

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ImportExcel(ImportMetricsExcelVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var campaign = await reposCampaigns.GetById(model.CampaignId);
            if (campaign is null)
            {
                return RedirectToAction("NotFound", "Home");
            }

            if (model.ExcelFile == null || model.ExcelFile.Length == 0)
            {
                TempData["Error"] = "No file submitted.";
                return RedirectToAction("Index");
            }

            using var stream = new MemoryStream();
            await model.ExcelFile.CopyToAsync(stream);
            using var workbook = new XLWorkbook(stream);
            var worksheet = workbook.Worksheet(1);
            var rows = worksheet.RowsUsed().Skip(1);

            int recordsProcessed = 0;

            using var connection = new SqlConnection(reposMetrics.ConnectionString);
            await connection.OpenAsync();
            using var transaction = connection.BeginTransaction();

            try
            {
                foreach (var row in rows)
                {
                    string submissionTimestampStr = row.Cell(1).GetString().Trim();
                    string emailStr = row.Cell(2).GetString().Trim();
                    string screenshotLinkStr = row.Cell(3).GetString().Trim();
                    string videoLinkStr = row.Cell(5).GetString().Trim();
                    string numViewsStr = row.Cell(6).GetString().Trim();
                    string numLikesStr = row.Cell(7).GetString().Trim();
                    string numCommentsStr = row.Cell(8).GetString().Trim();
                    string numSharesStr = row.Cell(9).GetString().Trim();
                    string numSavesStr = row.Cell(10).GetString().Trim();
                    string videoDurationStr = row.Cell(11).GetString().Trim();
                    string qrCodelinkStr = row.Cell(12).GetString().Trim();

                    // Null Validations
                    if (string.IsNullOrWhiteSpace(submissionTimestampStr))
                        throw new Exception("Null Submission Timestamp in some cell.");

                    if (string.IsNullOrWhiteSpace(emailStr))
                        throw new Exception("Null Email in some cell.");

                    if (string.IsNullOrWhiteSpace(screenshotLinkStr))
                        throw new Exception("Null Screenshot Link in some cell.");

                    if (string.IsNullOrWhiteSpace(videoLinkStr))
                        throw new Exception("Null Video Link in some cell.");

                    if (string.IsNullOrWhiteSpace(numViewsStr))
                        throw new Exception("Null Num Views in some cell.");

                    if (string.IsNullOrWhiteSpace(numLikesStr))
                        throw new Exception("Null Num Likes in some cell.");

                    if (string.IsNullOrWhiteSpace(numCommentsStr))
                        throw new Exception("Null Num Comments in some cell.");

                    if (string.IsNullOrWhiteSpace(numSharesStr))
                        throw new Exception("Null Num Shares in some cell.");

                    if (string.IsNullOrWhiteSpace(numSavesStr))
                        throw new Exception("Null Num Saves in some cell.");

                    if (string.IsNullOrWhiteSpace(videoDurationStr))
                        throw new Exception("Null Video Duration in some cell.");

                    if (string.IsNullOrWhiteSpace(qrCodelinkStr))
                        throw new Exception("Null QR Code Link in some cell.");
               
                    // Conversions
                    if (!DateTime.TryParse(submissionTimestampStr, out DateTime submissionTimestamp))
                        throw new Exception("Invalid Submission Timestamp in som cell: " + submissionTimestampStr);

                    var email = emailStr;
                    var screenshotLink = screenshotLinkStr;
                    var videoLink = videoLinkStr;

                    if (!int.TryParse(numViewsStr, out int numViews))
                        throw new Exception("Invalid Num Views in some cell: " + numViewsStr);

                    if (!int.TryParse(numLikesStr, out int numLikes))
                        throw new Exception("Invalid Num Likes in some cell: " + numLikesStr);

                    if (!int.TryParse(numCommentsStr, out int numComments))
                        throw new Exception("Invalid Num Comments in some cell: " + numCommentsStr);

                    if (!int.TryParse(numSharesStr, out int numShares))
                        throw new Exception("Invalid Num Shares in some cell: " + numSharesStr);

                    if (!int.TryParse(numSavesStr, out int numSaves))
                        throw new Exception("Invalid Num Saves in some cell: " + numSavesStr);
                    
                    if (!int.TryParse(videoDurationStr, out int videoDuration))
                        throw new Exception("Invalid Video Duration in some cell: " + videoDurationStr);

                    var qrCodelink = qrCodelinkStr;

                    var campaignId = campaign.ProductId;

                    var creator = await reposCreators.GetByEmail(email);
                    if (creator is null)
                    {
                        throw new Exception("No creator exists with the email: " + email);
                    }

                    var creatorId = creator.EntityId;
                    var campaignCreator = await reposCampaignCreators.GetByIds(campaignId, creatorId);
                    if (campaignCreator is null)
                    {
                        throw new Exception($"The enrollment with Campaign Id: {campaignId} and Creator: {creator.Name} doesn't exist.");
                    }

                    var campaignCreatorId = campaignCreator.CampaignCreatorId;                    

                    var metric = new Metric
                    {
                        CreationDate = DateTime.Now,
                        SubmissionTimestamp = submissionTimestamp,
                        CampaignCreatorId = campaignCreatorId,
                        ScreenshotLink = screenshotLink,
                        VideoLink = videoLink,
                        NumViews = numViews,
                        NumLikes = numLikes,
                        NumComments = numComments,
                        NumShares = numShares,
                        NumSaves = numSaves,
                        VideoDuration = videoDuration,
                        QRCodeLink = qrCodelink,
                        CalculatedClientPayment = 0m,
                        CalculatedCreatorPayout = 0m,
                    };

                    await reposMetrics.Create(metric, connection, transaction);
                    recordsProcessed++;
                }

                transaction.Commit();
                TempData["Success"] = $"{recordsProcessed} metrics successfully processed.";
            }
            catch (Exception ex)
            {
                transaction.Rollback(); // algo falló, deshacer todo
                TempData["Error"] = $"Error processing the file: {ex.Message}";
            }

            return RedirectToAction("IndexByCampaign", new { campaignId = campaign.ProductId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CalculateCosts(int campaignId)
        {
            try
            {
                if (!await reposMetrics.RecalculateCampaignPayments(campaignId)) return NotFound();
                TempData["Success"] = "Costs recalculated. Settled payments were preserved.";
            }
            catch (ArgumentException ex)
            {
                TempData["Error"] = ex.Message;
            }
            return RedirectToAction("IndexByCampaign", new { campaignId });
        }
        [HttpGet]
        public async Task<IActionResult> Update(int metricId)
        {
            var metric = await reposMetrics.GetById(metricId);
            if (metric is null)
            {
                return RedirectToAction("NotFound", "Home");
            }

            var model = EditViewModels.From(metric);

            model.CreatorNames = await GetCreatorNamesItems();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Update(Metric metric)
        {
            if (!ModelState.IsValid)
            {
                return View(metric);
            }

            await reposMetrics.Update(metric);

            return RedirectToAction("IndexByCampaign", new { campaignId = metric.CampaignId });
        }

        [HttpGet]
        public async Task<IActionResult> Exists(int campaignCreatorId)
        {
            var metric = await reposMetrics.GetByCampaignCreator(campaignCreatorId);
            if (metric is null)
            {
                return Json(new { exists = false });
            }

            return Json(new { exists = true });
        }


    }
}

