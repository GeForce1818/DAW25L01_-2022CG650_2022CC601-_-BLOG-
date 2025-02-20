using System.ComponentModel.DataAnnotations;

namespace L01_2022CG650_2022CC601.Models
{
    public class publicaciones
    {
        [Key]
        public int publicacionId { get; set; }
        public string titulo { get; set; }
        public string descripcion { get; set; }
        public int usuarioId { get; set; }

    }
}
