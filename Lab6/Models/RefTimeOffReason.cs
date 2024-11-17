using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lab6.Models
{
    public class RefTimeOffReason
    {
        public string ReasonCode { get; set; }
        public string ReasonDescription { get; set; }
    }
}
