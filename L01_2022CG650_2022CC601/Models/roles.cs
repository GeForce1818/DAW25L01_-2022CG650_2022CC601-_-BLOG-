using System.ComponentModel.DataAnnotations;

namespace L01_2022CG650_2022CC601.Models
{
    public class roles
    {
        [Key]
        public int rolId { get; set; }
        public string rol { get; set; }
    }
}
