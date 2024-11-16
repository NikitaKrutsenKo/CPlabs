using System.ComponentModel.DataAnnotations;

namespace Lab6.Models
{
    public class Ward
    {
        [Key]
        public int Ward_ID { get; set; }
        public string WardName { get; set; }
        public string WardLocation { get; set; }
        public string WardDescription { get; set; }
    }
}
