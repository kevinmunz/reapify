using Advertisements.Models;
using Advertisements.Models.CampaignModels;
using Advertisements.Models.ClientModels;
using Advertisements.Models.CreatorModels;
using Advertisements.Models.Enums;
using Advertisements.Services;
using ClosedXML.Excel;
using Dapper;
using DocumentFormat.OpenXml.EMMA;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Diagnostics;

namespace Advertisements.Controllers
{
    public class ClientsController : Controller
    {
        private readonly IReposClients reposClients;
        private readonly IReposIndustries reposIndustries;

        private readonly IReposCountries reposCountries;
        private readonly IReposStates reposStates;

        public ClientsController(IReposClients reposClients, IReposIndustries reposIndustries, IReposCountries reposCountries, IReposStates reposStates)
        {
            this.reposClients = reposClients;
            this.reposIndustries = reposIndustries;

            this.reposCountries = reposCountries;
            this.reposStates = reposStates;
        }
        public async Task<IActionResult> Index()
        {
            var clients = await reposClients.GetAll();

            return View(clients);
        }

        private async Task<IEnumerable<SelectListItem>> GetIndustryNamesItems()
        {
            var clients = await reposIndustries.GetAll();
            return clients.Select(x => new SelectListItem(x.Name, x.IndustryId.ToString()));
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
            var model = new CreateClientVM();
            model.IndustryNames = await GetIndustryNamesItems();
            model.CountryNames = await GetCountryNamesItems();
            model.StateNames = [];

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateClientVM model)
        {
            if (!ModelState.IsValid)
            {
                model.IndustryNames = await GetIndustryNamesItems();
                model.CountryNames = await GetCountryNamesItems();
                model.StateNames = await GetStateNamesItems(model.CountryId);

                return View(model);
            }

            var industry = await reposIndustries.GetById(model.IndustryId);
            if (industry == null)
            {
                return RedirectToAction("NotFound", "Home");
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
            await reposClients.Create(model);

            return RedirectToAction("Index");
        }       

        [HttpGet]
        public async Task<IActionResult> Update(int clientId)
        {
            var client = await reposClients.GetById(clientId); 
            if (client is null)
            {
                return RedirectToAction("NotFound", "Home");
            }

            var model = EditViewModels.From(client);

            model.IndustryNames = await GetIndustryNamesItems();
            model.CountryNames = await GetCountryNamesItems();
            model.StateNames = await GetStateNamesItems(model.CountryId);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Update(CreateClientVM model)
        {
            if (!ModelState.IsValid)
            {
                model.IndustryNames = await GetIndustryNamesItems();
                model.CountryNames = await GetCountryNamesItems();
                model.StateNames = await GetStateNamesItems(model.CountryId);

                return View(model);
            }

            var industry = await reposIndustries.GetById(model.IndustryId);
            if (industry == null)
            {
                return RedirectToAction("NotFound", "Home");
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

            await reposClients.Update(model);

            return RedirectToAction("Index");
        }




    }
}
