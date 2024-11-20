using Microsoft.AspNetCore.Mvc;
using Lab5.Models;
using Lab5.Lab6GetToken;
using System.Net.Http.Headers;

namespace Lab5.Controllers
{
    public class WardController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public WardController(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient("HospitalApiClient");
            var token = await GetToken.GetAccessTokenAsync(_httpClientFactory, _configuration);

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await client.GetAsync("Ward");

            if (!response.IsSuccessStatusCode)
            {
                return View("Error", $"API error: {response.StatusCode}");
            }

            var wards = await response.Content.ReadFromJsonAsync<List<Ward>>();
            return View(wards);
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
            var response = await client.GetAsync($"Ward/{id}");

            if (!response.IsSuccessStatusCode)
            {
                ViewBag.ErrorMessage = "Ward not found!";
                return View();
            }

            var ward = await response.Content.ReadFromJsonAsync<Ward>();
            return View("Details", ward);
        }
    }
}
