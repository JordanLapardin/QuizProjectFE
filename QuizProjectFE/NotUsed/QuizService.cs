using QuizProjectFE.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuizProjectFE.Models.DTO;

namespace QuizProjectFE.Services
{
    public class QuizService
    {
        public static HttpClient _client;

        public QuizService()
        {
            if (_client == null)
            {
                _client = new HttpClient();
                _client.BaseAddress = new Uri("https://localhost:44388/api/");

                _client.DefaultRequestHeaders.Clear();
                _client.DefaultRequestHeaders.Accept
                    .Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            }
        }
        //All Quizzes
        public List<Quiz> GetQuizzes()
        {
            var response = _client.GetAsync("Quiz").Result;
#if DEBUG
            response.EnsureSuccessStatusCode();
#endif

            var quizzes = response.Content.ReadAsAsync<List<Quiz>>().Result;

            return quizzes;
        }
        //Get single Quiz
        public Quiz GetSingleQuiz(int id)
        {
            var response = _client.GetAsync($"Quiz/{id}").Result;
#if DEBUG
            response.EnsureSuccessStatusCode();
#endif
            var quiz = response.Content.ReadAsAsync<Quiz>().Result;

            return quiz;
        }
        // Create a New Quiz
        public void CreateQuiz(QuizCreate quiz)
        {
            var response = _client.PostAsJsonAsync("Quiz", quiz).Result;
#if DEBUG
            response.EnsureSuccessStatusCode();
#endif

        }
        //edit
        public void GetOneQuiztoEdit( QuizCreate quiz, int id)
        {
            var response = _client.PutAsJsonAsync("Quiz",id).Result;
#if DEBUG
            response.EnsureSuccessStatusCode();
#endif
            
        }
    }
}

