using Microsoft.AspNetCore.Mvc;
using Lab13.Lab6GetToken;
using Lab13.Models;
using System.Net.Http.Headers;

namespace Lab13.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WardController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public WardController(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        // GET: api/Ward
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Ward>>> GetWards()
        {
            var client = _httpClientFactory.CreateClient("HospitalApiClient");
            var token = await GetToken.GetAccessTokenAsync(_httpClientFactory, _configuration);

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await client.GetAsync("v1/Ward");

            if (!response.IsSuccessStatusCode)
            {
                return StatusCode((int)response.StatusCode, $"API error: {response.StatusCode}");
            }

            var wards = await response.Content.ReadFromJsonAsync<List<Ward>>();

            if (wards == null)
            {
                return NotFound("No data received from API.");
            }

            return Ok(wards);
        }

        // GET: api/Ward/v2
        [HttpGet("v2")]
        public async Task<ActionResult<IEnumerable<WardV2>>> GetWardsV2()
        {
            var client = _httpClientFactory.CreateClient("HospitalApiClient");
            var token = await GetToken.GetAccessTokenAsync(_httpClientFactory, _configuration);

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await client.GetAsync("v2/Ward");

            if (!response.IsSuccessStatusCode)
            {
                return StatusCode((int)response.StatusCode, $"API error: {response.StatusCode}");
            }

            var wards = await response.Content.ReadFromJsonAsync<List<WardV2>>();

            if (wards == null)
            {
                return NotFound("No data received from API.");
            }

            return Ok(wards);
        }

        // GET: api/Ward/Search
        [HttpGet("search")]
        public async Task<ActionResult<Ward>> SearchWard(int id)
        {
            var client = _httpClientFactory.CreateClient("HospitalApiClient");
            var token = await GetToken.GetAccessTokenAsync(_httpClientFactory, _configuration);

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await client.GetAsync($"Ward/{id}");

            if (!response.IsSuccessStatusCode)
            {
                return NotFound("Ward not found!");
            }

            var ward = await response.Content.ReadFromJsonAsync<Ward>();

            if (ward == null)
            {
                return NotFound("No data received from API.");
            }

            return Ok(ward);
        }
    }
}
