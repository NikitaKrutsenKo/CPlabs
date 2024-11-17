namespace Lab6.Models
{
    public class StaffPay
    {
        public int Pay_ID { get; set; }
        public int Staff_ID { get; set; }
        public DateTime PaymentDate { get; set; }
        public decimal GrossPay { get; set; }
        public decimal Deductions { get; set; }
        public decimal NetPay { get; set; }
        public Staff Staff { get; set; }
    }
}
