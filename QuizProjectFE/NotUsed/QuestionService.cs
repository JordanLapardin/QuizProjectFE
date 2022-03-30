using QuizProjectFE.Models;
using QuizProjectFE.Models.DTO;
using System;
using System.Collections.Generic;
using System.Net.Http;

namespace QuizProjectFE.Services
{
    public class QuestionService
    {
        public static HttpClient _client;

        public QuestionService()
        {
            if( _client == null )
            {
                _client = new HttpClient();
                _client.BaseAddress = new Uri("https://localhost:44388/api/");

                _client.DefaultRequestHeaders.Clear();
                _client.DefaultRequestHeaders.Accept
                    .Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            }
        }
        //Get all Questions
        public List<Question> GetQuestions()
        {
            var response = _client.GetAsync("Question").Result;
#if DEBUG
            response.EnsureSuccessStatusCode();
#endif
            var questions = response.Content.ReadAsAsync<List<Question>>().Result;

            return questions;
        }
        //Get single question
        public Question GetTheOneQuestion(int id)
        {
            var response = _client.GetAsync($"Question/{id}").Result;
#if DEBUG
            response.EnsureSuccessStatusCode();
#endif
            var question = response.Content.ReadAsAsync<Question>().Result;
            return question;
        }
        //do later work

//        public List<TafeClass> GetTafeClassesForTeacherId(int id)
//        {
//            var response = _client.GetAsync($"TafeClass/GetForTeacherId/{id}").Result;
//#if DEBUG
//            response.EnsureSuccessStatusCode();
//#endif
//            var tafeClasses = response.Content.ReadAsAsync<List<TafeClass>>().Result;

//            return tafeClasses;
//        }

        //Create a New question
        public void CreateQuestion(QuestionCreate questionCreate)
        {
            var response = _client.PostAsJsonAsync("Question", questionCreate).Result;
#if DEBUG
            response.EnsureSuccessStatusCode();
#endif
        }
    }
}
