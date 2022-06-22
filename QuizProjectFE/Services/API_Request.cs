using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;

namespace QuizProjectFE.Services
{
    public class API_Request<T> : IApiRequest<T>
    {

        ///<summary>
        ///http client & contexy set up
        ///</summary>
        ///<param name="_client">Http client</param>
        ///<param name="_context">Http context</param>
        ///<returns>Gives client authorization </returns>
        ///<created>Jordan, 20/06/22</created>
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
        /// <summary>
        /// Gets all of given controller name for example Quiz
        /// </summary>
        /// <param name="controllername"></param>
        /// <returns>Returns all of (controller name) </returns>
        public List<T> GetAll(string controllername)
        {
            var response = _client.GetAsync(controllername).Result;
#if DEBUG
            response.EnsureSuccessStatusCode();
#endif

            var thing = response.Content.ReadAsAsync<List<T>>().Result;

            return thing;
        }
        /// <summary>
        /// get single entry of given controller name for example Quiz
        /// </summary>
        /// <param name="controllername"></param>
        /// <param name="id"></param>
        /// <returns>Single entry with matching id </returns>
        public T GetSingle(string controllername, int id)
        {

            var response = _client.GetAsync($"{controllername}/{id}").Result;
#if DEBUG
            response.EnsureSuccessStatusCode();
#endif
            var thing = response.Content.ReadAsAsync<T>().Result;

            return thing;

        }
        /// <summary>
        /// create a entry of given controller name for example Quiz
        /// </summary>
        /// <param name="controllername"></param>
        /// <param name="entity"></param>
        /// <returns>Creates a entry from given details</returns>
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

