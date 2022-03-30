using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using QuizProjectFE.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace QuizProjectFE.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        public IActionResult QuizList()
        {
            string apiURL = "https://localhost:44388/api/quiz";

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage message = client.GetAsync(apiURL).Result;
                List<Quiz> quizzes = message.Content.ReadAsAsync<List<Quiz>>().Result;

                return View(quizzes);
            }
        }
        public IActionResult Index()
        {
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
        public IActionResult SetCreator(IFormCollection collection)
        {
            //Creator
            var creator = collection["creatorName"].ToString();
            HttpContext.Session.SetString("creator", creator);
            

            // Save the retrieved values in the session
            // Refresh the page
            return RedirectToAction(nameof(Index));

        }
    }
}
