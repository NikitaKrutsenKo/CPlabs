using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Lab6.Models
{
    public class Hospital
    {
        [Key]
        public int Hospital_ID { get; set; }
        public int Address_ID { get; set; }
        public string HospitalName { get; set; }
        public string OtherDetails { get; set; }

        [ForeignKey("Address_ID")]
        public Address Address { get; set; }
    }
}
