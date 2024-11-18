using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;

namespace Lab5.Models
{
    public class Hospital
    {
        public int Id { get; set; }
        public string HospitalName { get; set; }
        public Address Address { get; set; }
        public string OtherDetails { get; set; }
    }

    public class Address
    {
        public string Line1 { get; set; }
        public string City { get; set; }
        public string StateProvince { get; set; }
        public string PostalCode { get; set; }
    }

    public class Ward
    {
        public int Ward_ID { get; set; }
        public string WardName { get; set; }
        public string WardLocation { get; set; }
        public string WardDescription { get; set; }
    }

    public class RosterViewModel
    {
        public int Roster_ID { get; set; }
        public string StaffName { get; set; }
        public string ShiftName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }

    public class RosterDTO
    {
        [JsonPropertyName("roster_ID")]
        public int Roster_ID { get; set; }

        [JsonPropertyName("staff")]
        public StaffDTO Staff { get; set; }

        [JsonPropertyName("shift")]
        public ShiftDTO Shift { get; set; }

        [JsonPropertyName("startDate")]
        public DateTime StartDate { get; set; }

        [JsonPropertyName("endDate")]
        public DateTime EndDate { get; set; }
    }

    public class StaffDTO
    {
        [JsonPropertyName("firstName")]
        public string FirstName { get; set; }

        [JsonPropertyName("lastName")]
        public string LastName { get; set; }
    }

    public class ShiftDTO
    {
        [JsonPropertyName("shiftName")]
        public string ShiftName { get; set; }
    }
}
