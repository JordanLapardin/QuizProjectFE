using Microsoft.AspNetCore.Http;
using QuizProjectFE.Models;
using QuizProjectFE.Models.Testing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizProjectFE.Services
{
    public class InMemoryRequest<T> : IApiRequest<T> where T : class
    {
        BuiltinDatabase _db;

        public InMemoryRequest(BuiltinDatabase db, IHttpContextAccessor accessor)
        {
            _db = db;
            accessor.HttpContext.Session.SetString("Token", "Testing");
        }

        public T Create(string controllername, T entity)
        {
            switch (typeof(T).Name)
            {
                case nameof(Quiz):
                    Quiz quiz = entity as Quiz;
                    quiz.QuizId = _db.Quizzes.Count == 0 ? 1 : _db.Quizzes.OrderByDescending
                        (c => c.QuizId).FirstOrDefault().QuizId + 1;
                    _db.Quizzes.Add(quiz);
                    return quiz as T;
            }
            return entity;
        }

        public void Delete(string controllername, int id)
        {
            throw new NotImplementedException();
        }

        public T Edit(string controllername, int id, T entity)
        {
            throw new NotImplementedException();
        }

        public List<T> GetAll(string controllername)
        {
            switch (typeof(T).Name)
            {
                case nameof(Quiz):
                    return _db.Quizzes as List<T>;
                case nameof(Question):
                    foreach(var question in _db.questions)
                    {
                        question.Quiz = _db.Quizzes.Where(c => c.QuizId == question.QuizId).FirstOrDefault();
                    }
                    return _db.questions as List<T>;
                default:
                    return null;
            }
        }

        public List<T> GetChildren(string controllername, string enpoint, int id)
        {
            throw new NotImplementedException();
        }

        public T GetSingle(string controllername, int id)
        {
            throw new NotImplementedException();
        }
    }
}
