using System;
using System.ComponentModel.DataAnnotations;

namespace PersonasAPI.Models
{
    public class Persona
    {
        [Key]
        public int IDPersona { get; set; }
        public string? Nombre { get; set; }
        public string? Apellido { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public int? Edad { get; set; }
        public string? Celular { get; set; }
        public string? RUT { get; set; }
        public int? IDGrupoFamiliar { get; set; }
    }
}