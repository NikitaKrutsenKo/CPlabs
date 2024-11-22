using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using Lab13.Lab6GetToken;
using Lab13.Models;

namespace Lab13.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HospitalController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public HospitalController(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Hospital>>> GetHospitals()
        {
            var client = _httpClientFactory.CreateClient("HospitalApiClient");

            var token = await GetToken.GetAccessTokenAsync(_httpClientFactory, _configuration);

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await client.GetAsync("Hospital");

            if (!response.IsSuccessStatusCode)
            {
                return StatusCode((int)response.StatusCode, $"API error: {response.StatusCode}");
            }

            var hospitals = await response.Content.ReadFromJsonAsync<List<Hospital>>();
            return Ok(hospitals);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Hospital>> GetHospital(int id)
        {
            var client = _httpClientFactory.CreateClient("HospitalApiClient");

            var token = await GetToken.GetAccessTokenAsync(_httpClientFactory, _configuration);

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await client.GetAsync($"Hospital/{id}");

            if (!response.IsSuccessStatusCode)
            {
                return NotFound("Hospital not found!");
            }

            var hospital = await response.Content.ReadFromJsonAsync<Hospital>();
            return Ok(hospital);
        }
    }

}
