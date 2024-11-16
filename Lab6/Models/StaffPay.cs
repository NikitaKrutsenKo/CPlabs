using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Lab6.Models
{
    public class StaffPay
    {
        [Key]
        public int Pay_ID { get; set; }
        public int Staff_ID { get; set; }
        public DateTime PaymentDate { get; set; }
        public decimal GrossPay { get; set; }
        public decimal Deductions { get; set; }
        public decimal NetPay { get; set; }

        [ForeignKey("Staff_ID")]
        public Staff Staff { get; set; }
    }
}
