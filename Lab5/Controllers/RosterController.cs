using Lab5.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;

namespace Lab5.Controllers
{
    public class RosterController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public RosterController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        // GET: /Roster
        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient("HospitalApiClient");
            var response = await client.GetAsync("Roster");

            if (!response.IsSuccessStatusCode)
            {
                return View("Error", $"API error: {response.StatusCode}");
            }

            // Десеріалізуємо JSON відповідь у список RosterDTO
            var apiData = await response.Content.ReadFromJsonAsync<List<RosterDTO>>();

            if (apiData == null)
            {
                return View("Error", "No data received from API.");
            }

            // Вибірка даних для відображення
            var rosters = apiData.Select(r => new RosterViewModel
            {
                Roster_ID = r.Roster_ID, 
                StaffName = $"{r.Staff.FirstName} {r.Staff.LastName}",
                ShiftName = r.Shift.ShiftName,
                StartDate = r.StartDate,
                EndDate = r.EndDate
            }).ToList();

            return View(rosters);
        }


        //GET: /Roster/Search
        public async Task<IActionResult> Search(DateTime? startDate, DateTime? endDate, List<int>? staffIds, string? shiftNameStart)
        {
            var client = _httpClientFactory.CreateClient("HospitalApiClient");
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
                return View();
            }

            var apiData = await response.Content.ReadFromJsonAsync<List<RosterDTO>>();
            var rosters = apiData.Select(r => new RosterViewModel
            {
                Roster_ID = r.Roster_ID,
                StaffName = $"{r.Staff.FirstName} {r.Staff.LastName}",
                ShiftName = r.Shift.ShiftName,
                StartDate = r.StartDate,
                EndDate = r.EndDate
            }).ToList();
            return View(rosters);
        }
    }
}
