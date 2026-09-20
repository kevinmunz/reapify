using Advertisements.Models.Enums;
using Advertisements.Models.TransactionModels;
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
    public class TransactionsController : Controller
    {
        private readonly IReposTransactions reposTransactions;
        private readonly IReposCampaigns reposCampaigns;
        private readonly IReposCreators reposCreators;

        private readonly IReposEntities reposEntities;
        private readonly IReposProducts reposProducts;
        private readonly IReposMetrics reposMetrics;

        public TransactionsController(IReposTransactions reposTransactions, IReposCampaigns reposCampaigns, IReposCreators reposCreators,
            IReposEntities reposEntities, IReposProducts reposProducts, IReposMetrics reposMetrics)
        {
            this.reposTransactions = reposTransactions;
            this.reposCampaigns = reposCampaigns;
            this.reposCreators = reposCreators;

            this.reposEntities = reposEntities;
            this.reposProducts = reposProducts;
            this.reposMetrics = reposMetrics;
        }
        public async Task<IActionResult> Index()
        {
            var transactions = await reposTransactions.GetAll();

            return View(transactions);
        }

        private async Task<IEnumerable<SelectListItem>> GetEntityNamesItems()
        {
            var creators = await reposEntities.GetAll();
            return creators.Select(x => new SelectListItem(x.Name.ToString() + " - " + x.EntityType.ToString(), x.EntityId.ToString()));
        }

        private async Task<IEnumerable<SelectListItem>> GetProductIdsItems()
        {
            var clients = await reposProducts.GetAll();
            return clients.Select(x => new SelectListItem(x.ProductId.ToString() + " - " + x.ProductType.ToString(), x.ProductId.ToString()));
        }

        [HttpGet]
        public async Task<IActionResult> CreateForCampaign(int campaignId)
        {
            var campaign = await reposCampaigns.GetById(campaignId);
            if (campaign == null)
            {
                return RedirectToAction("NotFound", "Home");
            }            

            var transaction = new Transaction
            {
                Type = TranType.Deposit,
                Direction = Direction.Income,
                EntityId = campaign.ClientId,
                ProductId = campaign.ProductId
            };

            return View("Create", transaction);
        }

        [HttpGet]
        public async Task<IActionResult> CreateForMetric(int metricId)
        {
            var metric = await reposMetrics.GetById(metricId);
            if (metric == null)
            {
                return RedirectToAction("NotFound", "Home");
            }

            var transaction = new Transaction
            {
                Type = TranType.CreatorPayout,
                Direction = Direction.Expense,
                EntityId = metric.CreatorId,
                ProductId = metric.ProductId
            };

            return View("Create", transaction);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateTransactionVM model)
        {
            if (!ModelState.IsValid)
            {
                model.EntityNames = await GetEntityNamesItems();
                model.ProductIds = await GetProductIdsItems();
                return View(model);
            }

            var entity = await reposEntities.GetById(model.EntityId);
            if (entity == null)
            {
                return RedirectToAction("NotFound", "Home");
            }

            var product = await reposProducts.GetById(model.ProductId);
            if (product == null)
            {
                return RedirectToAction("NotFound", "Home");
            }

            model.CreationDate = DateTime.Now;
            await reposTransactions.Create(model);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult ImportExcel()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Update(int transactionId)
        {
            var transaction = await reposTransactions.GetById(transactionId);
            if (transaction is null)
            {
                return RedirectToAction("NotFound", "Home");
            }

            var model = EditViewModels.From(transaction);

            model.EntityNames = await GetEntityNamesItems();
            model.ProductIds = await GetProductIdsItems();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Update(CreateTransactionVM transaction)
        {
            if (!ModelState.IsValid)
            {
                return View(transaction);
            }

            await reposTransactions.Update(transaction);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Exists(int entityId, int productId)
        {
            var transaction = await reposTransactions.GetByIds(entityId, productId);
            if (transaction is null)
            {
                return Json(new { exists = false });
            }

            return Json(new { exists = true });
        }

    }
}

