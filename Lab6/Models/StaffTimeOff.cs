namespace Lab6.Models
{
    public class StaffTimeOff
    {
        public int StaffTimeOff_ID { get; set; }
        public int Staff_ID { get; set; }
        public string ReasonCode { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public Staff Staff { get; set; }
        public RefTimeOffReason RefTimeOffReason { get; set; }
    }
}
