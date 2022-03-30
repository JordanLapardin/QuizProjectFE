using QuizProjectFE.Models;
using QuizProjectFE.Models.DTO;
using System;
using System.Collections.Generic;
using System.Net.Http;

namespace QuizProjectFE.Services
{
    public class OptionService
    {
        public static HttpClient _client;

        public OptionService()
        {
            _client = new HttpClient();
            _client.BaseAddress = new Uri("https://localhost:44388/api/");

            _client.DefaultRequestHeaders.Clear();
            _client.DefaultRequestHeaders.Accept
                .Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        }
        //get all options
        public List<Option> GetOptions()
        {
            var response = _client.GetAsync("Option").Result;
#if DEBUG
            response.EnsureSuccessStatusCode();
#endif
            var options = response.Content.ReadAsAsync<List<Option>>().Result;
            
            return options;
        }
        //Get Single option
        public Option get1Option(int id)
        {
            var response = _client.GetAsync($"Option/{id}").Result;
#if DEBUG
            response.EnsureSuccessStatusCode();
#endif
            var option = response.Content.ReadAsAsync<Option>().Result;

            return option;
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
        public void CreateOption(OptionCreate optionCreate)
        {
            var response = _client.PostAsJsonAsync("Option",optionCreate).Result;
#if DEBUG
            response.EnsureSuccessStatusCode();
#endif
        }
    }
}
