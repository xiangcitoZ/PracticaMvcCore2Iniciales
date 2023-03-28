using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PracticaMvcCore2Iniciales.Models
{

    [Table("V_LIBRO_INDIVIDUAL")]
    public class VistaLibro
    {
        [Key]
        [Column("IDLIBRO")]
        public int IdLibro { get; set; }
        [Column("TITULO")]
        public string Titulo { get; set; }
        [Column("PORTADA")]
        public string Portada { get; set; }
        [Column("POSICION")]
        public int Posicion { get; set; }

    }
}
