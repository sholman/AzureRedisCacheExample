using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AzureRedisCache.Models;
using Microsoft.AspNetCore.Http;

namespace AzureRedisCache.Controllers
{
    public class HomeController : Controller
    {
        public async Task<IActionResult> Index()
        {
            await HttpContext.Session.LoadAsync();

            var sessionstartTime = HttpContext.Session.GetString("storedSessionStartTime");

            if (sessionstartTime == null)
            {
                sessionstartTime = DateTime.Now.ToLongTimeString();
                HttpContext.Session.SetString("storedSessionStartTime", sessionstartTime);
                await HttpContext.Session.CommitAsync();
            }

            ViewBag.SessionStartTime = sessionstartTime;
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
