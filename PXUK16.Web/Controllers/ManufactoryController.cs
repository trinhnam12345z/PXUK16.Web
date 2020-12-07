using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PXUK16.Web.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace PXUK16.Web.Controllers
{
    public class ManufactoryController : Controller
    {
        private readonly ILogger<ManufactoryController> _logger;

        public ManufactoryController(ILogger<ManufactoryController> logger)
        {
            _logger = logger;
        }
        public IActionResult Index()
        {
            List<Manufactory> categories = new List<Manufactory>();
            categories = Helper.ApiHelper<List<Manufactory>>.HttpGetAsync("api/manufactory/gets");
            return View(categories);
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(CreateManufactory model)
        {
            if (ModelState.IsValid)
            {
                var result = new CreateManufactoryResult();
                result = Helper.ApiHelper<CreateManufactoryResult>.HttpPostAsync("api/manufactory/create", "POST", model);
                if (result.ManufactoryId > 0)
                {
                    return RedirectToAction("index");
                }
                ModelState.AddModelError("", result.Message);
                return View(model);
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult Update()
        {
            return View();
        }
    }
}
