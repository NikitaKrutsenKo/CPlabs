using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lab6.Models
{
    public class Ward
    {
        public int Ward_ID { get; set; }
        public string WardName { get; set; }
        public string WardLocation { get; set; }
        public string WardDescription { get; set; }
    }
}
