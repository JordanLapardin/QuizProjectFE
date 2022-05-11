using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;

namespace QuizProjectFE.Services
{
    public class API_Request<T> : IApiRequest<T>
    {
        //http client
        private readonly HttpClient _client;
        private readonly HttpContext _context;
        public API_Request(IHttpClientFactory httpClientFactory, IHttpContextAccessor accessor)
        {
            _context = accessor.HttpContext;
            _client = httpClientFactory.CreateClient("ApiClient");

            if (_context.Session.GetString("Token") != null)
            {
                _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _context.Session.GetString("Token"));
            }
        }
        //get all
        public List<T> GetAll(string controllername)
        {
            var response = _client.GetAsync(controllername).Result;
#if DEBUG
            response.EnsureSuccessStatusCode();
#endif

            var thing = response.Content.ReadAsAsync<List<T>>().Result;

            return thing;
        }
        //get single
        public T GetSingle(string controllername, int id)
        {

            var response = _client.GetAsync($"{controllername}/{id}").Result;
#if DEBUG
            response.EnsureSuccessStatusCode();
#endif
            var thing = response.Content.ReadAsAsync<T>().Result;

            return thing;

        }
        //create
        public T Create(string controllername, T entity)
        {
            var response = _client.PostAsJsonAsync(controllername, entity).Result;
#if DEBUG
            response.EnsureSuccessStatusCode();
#endif
            return response.Content.ReadAsAsync<T>().Result;
        }
        //edit
        public T Edit(string controllername, int id, T entity)
        {
            var response = _client.PutAsJsonAsync($"{controllername}/{id}", entity).Result;
#if DEBUG
            response.EnsureSuccessStatusCode();
#endif
            return response.Content.ReadAsAsync<T>().Result;
        }
        //delete
        public void Delete(string controllername, int id)
        {
            var response = _client.DeleteAsync($"{controllername}/{id}").Result;
#if DEBUG
            response.EnsureSuccessStatusCode();
#endif
        }

        public List<T> GetChildren(string controllername, string enpoint, int id)
        {
            var response = _client.GetAsync($"{controllername}/{enpoint}/{id}").Result;

            response.EnsureSuccessStatusCode();

            var enities = response.Content.ReadAsAsync<List<T>>().Result;
            return enities;
        }
    }
}

