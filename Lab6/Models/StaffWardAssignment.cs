namespace Lab6.Models
{
    public class StaffWardAssignment
    {
        public int Assignment_ID { get; set; }
        public int Staff_ID { get; set; }
        public int Ward_ID { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public Staff Staff { get; set; }
        public Ward Ward { get; set; }
    }
}
