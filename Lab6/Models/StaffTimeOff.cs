using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Lab6.Models
{
    public class StaffTimeOff
    {
        [Key]
        public int StaffTimeOff_ID { get; set; }
        public int Staff_ID { get; set; }
        public string ReasonCode { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }

        [ForeignKey("Staff_ID")]
        public Staff Staff { get; set; }

        [ForeignKey("ReasonCode")]
        public RefTimeOffReason RefTimeOffReason { get; set; }
    }
}
