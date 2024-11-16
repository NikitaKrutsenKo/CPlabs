using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Lab6.Models
{
    public class StaffWardAssignment
    {
        [Key]
        public int Assignment_ID { get; set; }
        public int Staff_ID { get; set; }
        public int Ward_ID { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }

        [ForeignKey("Staff_ID")]
        public Staff Staff { get; set; }

        [ForeignKey("Ward_ID")]
        public Ward Ward { get; set; }
    }
}
