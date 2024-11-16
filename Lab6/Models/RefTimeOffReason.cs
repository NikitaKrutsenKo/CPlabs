using System.ComponentModel.DataAnnotations;

namespace Lab6.Models
{
    public class RefTimeOffReason
    {
        [Key]
        public string ReasonCode { get; set; }
        public string ReasonDescription { get; set; }
    }
}
