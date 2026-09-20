using Advertisements.Models;
using Advertisements.Models.IndustryModels;
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
    public class IndustriesController : Controller
    {
        private readonly IReposIndustries reposIndustries;

        public IndustriesController(IReposIndustries reposIndustries)
        {
            this.reposIndustries = reposIndustries;
        }
        public async Task<IActionResult> Index()
        {
            var industrys = await reposIndustries.GetAll();

            return View(industrys);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Industry industry)
        {
            if (!ModelState.IsValid)
            {
                return View(industry);
            }

            await reposIndustries.Create(industry);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult ImportExcel()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Update(int industryId)
        {
            var industry = await reposIndustries.GetById(industryId); 
            if (industry is null)
            {
                return RedirectToAction("NotFound", "Home");
            }

            return View(industry);
        }

        [HttpPost]
        public async Task<IActionResult> Update(Industry industry)
        {
            if (!ModelState.IsValid)
            {
                return View(industry);
            }

            await reposIndustries.Update(industry);

            return RedirectToAction("Index");
        }

    }
}
