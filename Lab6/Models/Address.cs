using System.ComponentModel.DataAnnotations;

namespace Lab6.Models
{
    public class Address
    {
        [Key]
        public int Address_ID { get; set; }
        public string Line1 { get; set; }
        public string Line2 { get; set; }
        public string Area { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public string StateProvince { get; set; }
        public string Country { get; set; }
        public string OtherAddressDetails { get; set; }
    }
}
