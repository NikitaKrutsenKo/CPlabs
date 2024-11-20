using Microsoft.AspNetCore.Mvc;
using Lab5.Models;
using Microsoft.Extensions.Configuration;
using System.Net.Http.Headers;
using Lab5.Lab6GetToken;

namespace Lab5.Controllers
{
    public class HospitalController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public HospitalController(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient("HospitalApiClient");

            var token = await GetToken.GetAccessTokenAsync(_httpClientFactory, _configuration);

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await client.GetAsync("Hospital");

            if (!response.IsSuccessStatusCode)
            {
                return View("Error", $"API error: {response.StatusCode}");
            }

            var hospitals = await response.Content.ReadFromJsonAsync<List<Hospital>>();
            return View(hospitals);
        }

        public IActionResult Search()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Search(int id)
        {
            var client = _httpClientFactory.CreateClient("HospitalApiClient");

            var token = await GetToken.GetAccessTokenAsync(_httpClientFactory, _configuration);

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await client.GetAsync($"Hospital/{id}");

            if (!response.IsSuccessStatusCode)
            {
                ViewBag.ErrorMessage = "Hospital not found!";
                return View();
            }

            var hospital = await response.Content.ReadFromJsonAsync<Hospital>();
            return View("Details", hospital);
        }
    }
}
