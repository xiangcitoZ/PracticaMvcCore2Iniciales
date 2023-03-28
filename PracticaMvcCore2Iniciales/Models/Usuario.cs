﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PracticaMvcCore2Iniciales.Models
{
    [Table("USUARIOS")]
    public class Usuario
    {
        [Key]
        [Column("IDUSUARIO")]
        public int IdUsuario { get; set; }
        [Column("NOMBRE")]
        public string Nombre { get; set; }
        [Column("APELLIDOS")]
        public string Apellidos { get; set; }
        [Column("Email")]
        public string Email { get; set; }
        [Column("PASS")]
        public string Pass { get; set; }
        [Column("FOTO")]
        public string Foto { get; set; }
       

    }
}
