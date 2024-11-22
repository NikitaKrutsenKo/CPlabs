using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using Lab13.Lab6GetToken;
using Lab13.Models;

namespace Lab13.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RosterController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public RosterController(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        // GET: api/Roster
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RosterViewModel>>> GetRosters()
        {
            var client = _httpClientFactory.CreateClient("HospitalApiClient");
            var token = await GetToken.GetAccessTokenAsync(_httpClientFactory, _configuration);

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await client.GetAsync("Roster");

            if (!response.IsSuccessStatusCode)
            {
                return StatusCode((int)response.StatusCode, $"API error: {response.StatusCode}");
            }

            var apiData = await response.Content.ReadFromJsonAsync<List<RosterDTO>>();

            if (apiData == null)
            {
                return NotFound("No data received from API.");
            }

            var rosters = apiData.Select(r => new RosterViewModel
            {
                Roster_ID = r.Roster_ID,
                StaffName = $"{r.Staff.FirstName} {r.Staff.LastName}",
                ShiftName = r.Shift.ShiftName,
                StartDate = r.StartDate,
                EndDate = r.EndDate
            }).ToList();

            return Ok(rosters);
        }

        // GET: api/Roster/Search
        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<RosterViewModel>>> SearchRosters(DateTime? startDate, DateTime? endDate, List<int>? staffIds, string? shiftNameStart)
        {
            var client = _httpClientFactory.CreateClient("HospitalApiClient");
            var token = await GetToken.GetAccessTokenAsync(_httpClientFactory, _configuration);

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var query = new List<string>();

            if (startDate.HasValue)
                query.Add($"startDate={startDate.Value.ToString("o")}");
            if (endDate.HasValue)
                query.Add($"endDate={endDate.Value.ToString("o")}");
            if (staffIds != null && staffIds.Any())
                query.Add($"staffIds={string.Join(",", staffIds)}");
            if (!string.IsNullOrEmpty(shiftNameStart))
                query.Add($"shiftNameStart={shiftNameStart}");

            var url = "Roster/search";
            if (query.Any())
                url += "?" + string.Join("&", query);

            var response = await client.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                return StatusCode((int)response.StatusCode, $"API error: {response.StatusCode}");
            }

            var apiData = await response.Content.ReadFromJsonAsync<List<RosterDTO>>();

            if (apiData == null)
            {
                return NotFound("No data received from API.");
            }

            var rosters = apiData.Select(r => new RosterViewModel
            {
                Roster_ID = r.Roster_ID,
                StaffName = $"{r.Staff.FirstName} {r.Staff.LastName}",
                ShiftName = r.Shift.ShiftName,
                StartDate = r.StartDate,
                EndDate = r.EndDate
            }).ToList();

            return Ok(rosters);
        }
    }
    
}
