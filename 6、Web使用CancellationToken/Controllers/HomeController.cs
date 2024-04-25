using _6_Web使用CancellationToken.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Security.Policy;
using System.Threading;
using System.Threading.Tasks;

namespace _6_Web使用CancellationToken.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> Index(CancellationToken cancellationToken)
        {
            await ConsoleTxt("https://www.youzack.com",100000, cancellationToken);
            return View();
        }
        static async Task ConsoleTxt(string url,int pin, CancellationToken cancellationToken)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                for (int i = 0; i < pin; i++)
                {
                    var res = await httpClient.GetAsync(url, cancellationToken);
                    string html = await res.Content.ReadAsStringAsync();
                    Debug.WriteLine(i);
                }
            }
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
