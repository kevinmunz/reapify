using Advertisements.Models.CampaignCreatorModels;
using Advertisements.Models.ClientModels;
using Advertisements.Models.CreatorModels;
using Advertisements.Models.Enums;
using Advertisements.Services;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.EMMA;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;

namespace Advertisements.Controllers
{
    public class CreatorsController : Controller
    {
        private readonly IReposCreators reposCreators;
        private readonly IReposCampaigns reposCampaigns;
        private readonly IReposCampaignCreators reposCampaignCreators;
        private readonly IReposCountries reposCountries;
        private readonly IReposStates reposStates;

        public CreatorsController(IReposCreators reposCreators, IReposCampaigns reposCampaigns,
            IReposCampaignCreators reposCampaignCreators, IReposCountries reposCountries, IReposStates reposStates)
        {
            this.reposCreators = reposCreators;
            this.reposCampaigns = reposCampaigns;
            this.reposCampaignCreators = reposCampaignCreators;
            this.reposCountries = reposCountries;
            this.reposStates = reposStates;

        }

        public async Task<IActionResult> Index()
        {
            var creators = await reposCreators.GetAll();
            return View(creators);
        }

        [HttpGet]
        public async Task<IActionResult> IndexForEnrollments(int campaignId)
        {
            var campaign = await reposCampaigns.GetById(campaignId);
            if (campaign is null)
            {
                return RedirectToAction("NotFound", "Home");
            }

            var creators = await reposCreators.GetAll();
            var campaignCreators = await reposCampaignCreators.GetByCampaign(campaignId);
            var enrolledIds = campaignCreators.Select(x => x.CreatorId).ToHashSet(); ;

            var model = new IndexCreatorsVM
            {
                CampaignId = campaignId,
                Creators = creators.Select(c => new CreatorSelectionVM
                {
                    EntityId = c.EntityId,
                    CreationDate = c.CreationDate,
                    EntityType = c.EntityType,
                    Name = c.Name,
                    StateId = c.StateId,
                    FirstName = c.FirstName,
                    LastName = c.LastName,
                    CI = c.CI,
                    RegistrationPhone = c.RegistrationPhone,
                    RegistrationEmail = c.RegistrationEmail,
                    FollowerRange = c.FollowerRange,
                    CreatorStatus = c.CreatorStatus,
                    Selected = enrolledIds.Contains(c.EntityId)
                }).ToList()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateEnrollments(int campaignId, int[] selectedIds)
        {
            var campaign = await reposCampaigns.GetById(campaignId);
            if (campaign is null)
            {
                return RedirectToAction("NotFound", "Home");
            }

            var existingIds = (await reposCampaignCreators.GetByCampaign(campaignId))
                                .Select(cac => cac.CreatorId)
                                .ToHashSet();

            foreach (var creatorId in selectedIds)
            {
                if (existingIds.Contains(creatorId))
                {
                    continue;
                }

                var campaignCreator = new CampaignCreator
                {
                    CreationDate = DateTime.Now,
                    CampaignId = campaignId,
                    CreatorId = creatorId
                };

                await reposCampaignCreators.Create(campaignCreator);
            }

            return RedirectToAction("IndexByCampaign", "CampaignCreators", new { campaignId });
        }

        private async Task<IEnumerable<SelectListItem>> GetCountryNamesItems()
        {
            var countries = await reposCountries.GetAll();
            return countries.Select(x => new SelectListItem(x.Name, x.CountryId.ToString()));
        }

        private async Task<IEnumerable<SelectListItem>> GetStateNamesItems(int countryId)
        {
            var states = await reposStates.GetByCountry(countryId);
            return states.Select(x => new SelectListItem(x.Name, x.StateId.ToString()));
        }

        [HttpGet]
        public async Task<IActionResult> GetStates(int countryId)
        {
            var states = await reposStates.GetByCountry(countryId);
            var result = states.Select(x => new
            {
                value = x.StateId,
                text = x.Name
            });

            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var model = new CreateCreatorVM();
            model.CountryNames = await GetCountryNamesItems();
            model.StateNames = [];

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateCreatorVM model)
        {
            if (!ModelState.IsValid)
            {
                model.CountryNames = await GetCountryNamesItems();
                model.StateNames = await GetStateNamesItems(model.CountryId);

                return View(model);
            }

            var country = await reposCountries.GetById(model.CountryId);
            if (country == null)
            {
                return RedirectToAction("NotFound", "Home");
            }

            var state = await reposStates.GetById(model.StateId);
            if (state == null)
            {
                return RedirectToAction("NotFound", "Home");
            }

            model.CreationDate = DateTime.Now;
            model.CreatorStatus = CreatorStatus.Active;

            await reposCreators.Create(model);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult ImportExcel()
        {
            return View();
        }        

        [HttpGet]
        public async Task<IActionResult> Update(int creatorId)
        {
            var creator = await reposCreators.GetById(creatorId);
            if (creator is null)
            {
                return RedirectToAction("NotFound", "Home");
            }

            var model = EditViewModels.From(creator);

            model.CountryNames = await GetCountryNamesItems();
            model.StateNames = await GetStateNamesItems(model.CountryId);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Update(CreateCreatorVM model)
        {
            if (!ModelState.IsValid)
            {
                model.CountryNames = await GetCountryNamesItems();
                model.StateNames = await GetStateNamesItems(model.CountryId);

                return View(model);
            }

            var country = await reposCountries.GetById(model.CountryId);
            if (country == null)
            {
                return RedirectToAction("NotFound", "Home");
            }

            var state = await reposStates.GetById(model.StateId);
            if (state == null)
            {
                return RedirectToAction("NotFound", "Home");
            }

            await reposCreators.Update(model);

            return RedirectToAction("Index");
        }
    }
}
