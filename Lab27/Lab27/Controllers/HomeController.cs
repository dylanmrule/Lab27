using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Lab27.Models;
using System.Net.Http;
using Lab27.API__Models;


namespace Lab27.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Zip(string zip)
        {
            ZipToLatLon modelVar = new ZipToLatLon();
            var client = new HttpClient();
            client.BaseAddress = new Uri("https://api.zip-codes.com");
            client.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (compatible; GrandCircus/1.0)");
            var modifiedEndPoint = "ZipCodesAPI.svc/1.0/QuickGetZipCodeDetails/" + zip + "?key=Y4P999GSKSNZ0PH2IJEF";
            var response = await client.GetAsync(modifiedEndPoint);
            var LatLon = await response.Content.ReadAsAsync<ZipToLatLon>();
            modelVar.Latitude = LatLon.Latitude;
            modelVar.Longitude = LatLon.Longitude;
            

           
            return RedirectToAction("Forecast", "Home", modelVar);
        
        }

        
        public async Task<IActionResult> Forecast(ZipToLatLon modelVar, string lat, string lon)
        {
            if (lat == null && lon == null){

                lat = modelVar.Latitude;
                lon = modelVar.Longitude;
            }
            

            var client = new HttpClient();
            client.BaseAddress = new Uri("https://forecast.weather.gov");
            client.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (compatible; GrandCircus/1.0)");
            var modifiedEndPoint = "MapClick.php?lat=" + lat + "&lon=" + lon + "&FcstType=json";
            var response = await client.GetAsync(modifiedEndPoint);
            var forecast = await response.Content.ReadAsAsync<Weather>();
            return View(forecast);
            
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
