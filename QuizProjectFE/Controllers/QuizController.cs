using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuizProjectFE.Models;
using QuizProjectFE.Models.DTO;
using QuizProjectFE.Services;
using System;
using System.Linq;

namespace QuizProjectFE.Controllers
{
    public class QuizController : Controller
    {
        private readonly IApiRequest<Quiz> _quizService;

        private string controllername = "Quiz";

        public QuizController(IApiRequest<Quiz> request)
        {
            _quizService = request;
        }
        
        // GET: QuizController
        public ActionResult Index()
        {
            var quizzes = _quizService.GetAll(controllername);
            return View(quizzes);
        }

        // GET: QuizController/Details/5
        public ActionResult Details(int id)
        {
            if (!HttpContext.Session.Keys.Any(c => c.Equals("Token")))
            {
                return RedirectToAction("Login", "Auth");
            }
            Quiz quiz = _quizService.GetSingle(controllername, id);
            return View(quiz);
            
        }

        

        // GET: QuizController/Create
        public ActionResult Create()
        {
            if (!HttpContext.Session.Keys.Any(c => c.Equals("Token")))
            {
                return RedirectToAction("Login", "Auth");
            }
            return View();
        }

        // POST: QuizController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(QuizCreate quiz)
        {
            if (!HttpContext.Session.Keys.Any(c => c.Equals("Token")))
            {
                return RedirectToAction("Login", "Auth");
            }
            try
            {
                Quiz newquiz = new Quiz()
                {
                    QuizTitle = quiz.QuizTitle,
                    QuizTopic = quiz.QuizTopic,
                    CreatorName = quiz.CreatorName,
                    PassP = quiz.PassP
                };

                _quizService.Create(controllername, newquiz);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: QuizController/Edit/5
        public ActionResult Edit(int id)
        {
            if (!HttpContext.Session.Keys.Any(c => c.Equals("Token")))
            {
                return RedirectToAction("Login", "Auth");
            }

            return View(_quizService.GetSingle(controllername, id));
            
        }

        // POST: QuizController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Quiz quiz)
        {
            if (!HttpContext.Session.Keys.Any(c => c.Equals("Token")))
            {
                return RedirectToAction("Login", "Auth");
            }
            try
            {
                _quizService.Edit(controllername, id, quiz);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: QuizController/Delete/5
        public ActionResult Delete(int id)
        {
            if (!HttpContext.Session.Keys.Any(c => c.Equals("Token")))
            {
                return RedirectToAction("Login", "Auth");
            }
            return View(_quizService.GetSingle(controllername, id));
        }

        // POST: QuizController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Quiz quiz)
        {
            if (!HttpContext.Session.Keys.Any(c => c.Equals("Token")))
            {
                return RedirectToAction("Login", "Auth");
            }
            try
            {
                _quizService.Delete(controllername, id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
