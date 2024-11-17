namespace Lab6.Models
{
    public class Shift
    {
        public int Shift_ID { get; set; }
        public string DayOrNight { get; set; }
        public string ShiftName { get; set; }
        public string ShiftDescription { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string OtherShiftDetails { get; set; }
    }
}
