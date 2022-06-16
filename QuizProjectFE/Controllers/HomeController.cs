using CsvHelper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using QuizProjectFE.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using ChartJSCore.Models;
using ChartJSCore.Helpers;
using QuizProjectFE.Models.DTO;

namespace QuizProjectFE.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private HttpClient _client;

        public HomeController(ILogger<HomeController> logger, IHttpClientFactory _factory)
        {
            _client = _factory.CreateClient("ApiClient");
            _logger = logger;
        }
        public IActionResult QuizList()
        {
            string apiURL = "https://quizapi20220511163943.azurewebsites.net/api/quiz";

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

        public IActionResult Help()
        {
            return View();
        }

        public IActionResult DisplayChart()
        {
            var response = _client.GetAsync("Report/QuestionsPerQuizReport").Result;

            List<ReportVeiwModel> reportVeiws = response.Content.ReadAsAsync<List<ReportVeiwModel>>().Result;

            var NumberofQuestions = reportVeiws.Select(x => (double?)x.QuestionCount).ToList();
            var ChartLabels = reportVeiws.Select(x => x.QuizName).ToList();

            Chart chart = new Chart();
            chart.Type = Enums.ChartType.Bar;
            ChartJSCore.Models.Data data = new ChartJSCore.Models.Data();
            data.Labels = ChartLabels;

            List<ChartColor> BarColors = new List<ChartColor>()
            {
                ChartColor.FromRgba(255, 25, 25, 0.4),
                ChartColor.FromRgba(50, 255, 50, 0.4),
                ChartColor.FromRgba(75, 75, 255, 0.4)
            };

            BarDataset dataset = new BarDataset()
            {
                Label = "Number of Questions",
                Data = NumberofQuestions,
                BackgroundColor = BarColors,
                BorderColor = BarColors,
                Type = Enums.ChartType.Bar,
            };

            data.Datasets = new List<Dataset>();
            data.Datasets.Add(dataset);
            chart.Data = data;

            ViewData["chart"] = chart;

            return View();
        }

        //public IActionResult ExportData()
        //{
        //    //gather report data
        //    //shoot me im missing code
        //    var response = _client.GetAll("Report", "QuestionsPerQuizReport");
        //    //create string
        //    var stream = new MemoryStream();
        //    //create writeer
        //    using (var writer = new StreamWriter(stream, leaveOpen: true))
        //    {
        //        var cvs = new CsvWriter(writer, CultureInfo.CurrentCulture, true);
        //        cvs.WriteRecords(response);
        //    }
        //    stream.Position = 0;

        //    return File(stream, "application/octect-stream", $"ReportData_{DateTime.Now.ToString("ddMM_HHmmss")}.csv");
        //}

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
