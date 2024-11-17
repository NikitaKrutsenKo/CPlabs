using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Lab6.Models
{
    public class RosterOfStaffOnShift
    {
        public int Roster_ID { get; set; }
        public int Staff_ID { get; set; }
        public int Shift_ID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Staff Staff { get; set; }
        public Shift Shift { get; set; }
    }
}
