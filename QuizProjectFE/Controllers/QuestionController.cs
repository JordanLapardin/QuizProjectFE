using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using QuizProjectFE.Models;
using QuizProjectFE.Models.DTO;
using QuizProjectFE.Services;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace QuizProjectFE.Controllers
{
    public class QuestionController : Controller
    {
        IWebHostEnvironment _HostEnviroment;
        private readonly IApiRequest<Quiz> _quizService;
        private readonly IApiRequest<Question> _questionService;

        private string controllername2 = "Quiz";
        private string controllername = "Question";

        public QuestionController(IApiRequest<Question> request, IApiRequest<Quiz> request2, IWebHostEnvironment webHost)
        {
            _questionService = request;
            _quizService = request2;
            _HostEnviroment = webHost;
        }
       
        // GET: QuestionController
        public ActionResult Index()
        {
            var questions = _questionService.GetAll(controllername);
            return View(questions);
        }

        // GET: QuestionController/Details/5
        public ActionResult Details(int id)
        {
            if (!HttpContext.Session.Keys.Any(c => c.Equals("Token")))
            {
                return RedirectToAction("Login", "Auth");
            }
            var question = _questionService.GetSingle(controllername,id);
            return View(question);
        }

        // GET: QuestionController/Create
        public ActionResult Create()
        {
            if (!HttpContext.Session.Keys.Any(c => c.Equals("Token")))
            {
                return RedirectToAction("Login", "Auth");
            }

            var quizzes = _quizService.GetAll(controllername2);

            var quizselect = quizzes.Select(c => new SelectListItem
            {
                Value = c.QuizId.ToString(),
                Text = c.QuizTitle
            }).ToList();

            ViewBag.quizeselect = quizselect;

            ViewData.Add("QuizSelect2", quizselect);

            return View();
        }

        // POST: QuestionController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(QuestionCreate question)
        {
            if (!HttpContext.Session.Keys.Any(c => c.Equals("Token")))
            {
                return RedirectToAction("Login", "Auth");
            }
            try
            {
                Question newQuestion = new Question()
                {
                    QuestionTopic = question.QuestionTopic,
                    QuestionText = question.QuestionText,
                    QuestionImg = question.QuestionImg,
                    QuizId = question.QuizId
                };
                _questionService.Create(controllername,newQuestion);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: QuestionController/Edit/5
        public ActionResult Edit(int id)
        {
            if (!HttpContext.Session.Keys.Any(c => c.Equals("Token")))
            {
                return RedirectToAction("Login", "Auth");
            }
            return View(_questionService.GetSingle(controllername,id));
        }

        // POST: QuestionController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Question question)
        {
            if (!HttpContext.Session.Keys.Any(c => c.Equals("Token")))
            {
                return RedirectToAction("Login", "Auth");
            }
            try
            {
                _questionService.Edit(controllername,id,question);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: QuestionController/Delete/5
        public ActionResult Delete(int id)
        {
            if (!HttpContext.Session.Keys.Any(c => c.Equals("Token")))
            {
                return RedirectToAction("Login", "Auth");
            }
            return View(_questionService.GetSingle(controllername, id));
        }

        // POST: QuestionController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Question question)
        {
            if (!HttpContext.Session.Keys.Any(c => c.Equals("Token")))
            {
                return RedirectToAction("Login", "Auth");
            }
            try
            {
                _questionService.Delete(controllername, id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        [HttpPost]
        public async Task<IActionResult> Uploadfile(IFormFile file)
        {
            string folderRoot = Path.Combine(_HostEnviroment.ContentRootPath, "wwwroot\\Pictures\\Uploads");
            string filePath = Path.Combine(folderRoot, file.FileName);

            using (var fs = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(fs);
            }

            return Ok(new {success = true, message = "file Uploaded"});
        }
    }
}
