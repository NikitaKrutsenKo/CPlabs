namespace Lab6.Models
{
    public class Staff
    {
        public int Staff_ID { get; set; }
        public int Address_ID { get; set; }
        public DateTime BirthDate { get; set; }
        public DateTime DateJoinedHospital { get; set; }
        public DateTime? DateLeftHospital { get; set; }
        public int Hospital_ID { get; set; }
        public string Gender { get; set; }
        public string JobTitle { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Qualifications { get; set; }
        public string OtherDetails { get; set; }
        public Address Address { get; set; }
        public Hospital Hospital { get; set; }
    }
}
