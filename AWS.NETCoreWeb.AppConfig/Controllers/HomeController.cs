using AWS.NETCoreWeb.AppConfig.Models;
using AWS.NETCoreWeb.AppConfig.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace AWS.NETCoreWeb.AppConfig.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly IUserService _userService;

        private readonly IAppConfigDataService _appConfigDataService;

        public HomeController(IUserService userService
            , ILogger<HomeController> logger,
           IAppConfigDataService appConfigDataService)
        {
            _userService = userService;
            _logger = logger;
            _appConfigDataService = appConfigDataService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Privacy()
        {
            var appconfig = await _appConfigDataService.GetAppConfigData();
            if (!(appconfig is null) && appconfig.BoolEnableLimitResults)
            {
                return View(_userService.GetAll().Where(x=>x.Id<= appconfig.IntResultLimit));
            }
            return View(_userService.GetAll());
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
