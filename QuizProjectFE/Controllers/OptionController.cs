using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using QuizProjectFE.Models;
using QuizProjectFE.Models.DTO;
using QuizProjectFE.Services;
using System.Linq;

namespace QuizProjectFE.Controllers
{
    public class OptionController : Controller
    {
        private readonly IApiRequest<Question> _QuestionService;
        private readonly IApiRequest<Option> _optionService;

        private string controllername2 = "Question";
        private string controllername = "Option";

        public OptionController(IApiRequest<Option> request, IApiRequest<Question> request2)
        {
            _optionService = request;
            _QuestionService = request2;
        }

        
        // GET: OptionController
        public ActionResult Index()
        {
            var options = _optionService.GetAll(controllername);
            return View(options);
        }

        // GET: OptionController/Details/5
        public ActionResult Details(int id)
        {
            if (!HttpContext.Session.Keys.Any(c => c.Equals("Token")))
            {
                return RedirectToAction("Login", "Auth");
            }
            var option = _optionService.GetSingle(controllername,id);
            return View(option);
        }

        // GET: OptionController/Create
        public ActionResult Create()
        {
            if (!HttpContext.Session.Keys.Any(c => c.Equals("Token")))
            {
                return RedirectToAction("Login", "Auth");
            }
            var questions = _QuestionService.GetAll(controllername2);

            var questionselect = questions.Select(c => new SelectListItem
            {
                Value = c.QuestionId.ToString(),
                Text = c.QuestionText
            }).ToList();
            
            ViewBag.questionselect = questionselect;

            ViewData.Add("questionselect2",questionselect);

            return View();
        }

        // POST: OptionController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(OptionCreate questionCreate)
        {
            if (!HttpContext.Session.Keys.Any(c => c.Equals("Token")))
            {
                return RedirectToAction("Login", "Auth");
            }
            try
            {
                Option newOption = new Option()
                {
                    OptionText = questionCreate.OptionText,
                    OptionLetter = questionCreate.OptionLetter,
                    IsCorrect = questionCreate.IsCorrect,
                    QuestionId = questionCreate.QuestionId
                };

                _optionService.Create(controllername,newOption);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: OptionController/Edit/5
        public ActionResult Edit(int id)
        {
            if (!HttpContext.Session.Keys.Any(c => c.Equals("Token")))
            {
                return RedirectToAction("Login", "Auth");
            }
            return View(_optionService.GetSingle(controllername, id));
        }

        // POST: OptionController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Option option)
        {
            if (!HttpContext.Session.Keys.Any(c => c.Equals("Token")))
            {
                return RedirectToAction("Login", "Auth");
            }
            try
            {
                _optionService.Edit(controllername, id, option);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: OptionController/Delete/5
        public ActionResult Delete(int id)
        {
            if (!HttpContext.Session.Keys.Any(c => c.Equals("Token")))
            {
                return RedirectToAction("Login", "Auth");
            }
            return View(_optionService.GetSingle(controllername, id));
        }

        // POST: OptionController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            if (!HttpContext.Session.Keys.Any(c => c.Equals("Token")))
            {
                return RedirectToAction("Login", "Auth");
            }
            try
            {
                _optionService.Delete(controllername, id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
