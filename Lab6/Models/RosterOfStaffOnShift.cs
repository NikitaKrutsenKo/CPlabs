using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Lab6.Models
{
    public class RosterOfStaffOnShift
    {
        [Key]
        public int Roster_ID { get; set; }
        public int Staff_ID { get; set; }
        public int Shift_ID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        [ForeignKey("Staff_ID")]
        public Staff Staff { get; set; }

        [ForeignKey("Shift_ID")]
        public Shift Shift { get; set; }
    }
}
