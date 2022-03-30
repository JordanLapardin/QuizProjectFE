using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuizProjectFE.Models;
using System.Net.Http;

namespace QuizProjectFE.Controllers
{
    public class AuthController : Controller
    {
        private readonly HttpClient _client;
        public AuthController(IHttpClientFactory httpClientFactory)
        {
            _client = httpClientFactory.CreateClient("ApiClient");
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(UserStuff user)
        {
            HttpResponseMessage result = _client.PostAsJsonAsync("Token/GenerateToken", user).Result;

            if(result.IsSuccessStatusCode)
            {
                var token = result.Content.ReadAsStringAsync().Result;
                HttpContext.Session.SetString("Token", token.Trim('"'));
                return RedirectToAction("Index", "Home");
                
            }

            return View();
            
        }
    }
}
