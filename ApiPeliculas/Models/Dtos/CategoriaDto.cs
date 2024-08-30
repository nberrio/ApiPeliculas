namespace ApiPeliculas.Models.Dtos
{
    using System.ComponentModel.DataAnnotations;

    public class CategoriaDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public DateTime FechaCreacion { get; set; }
    }

    public class CrearCategoriaDto
    {
        [Required(ErrorMessage = "El nombre es obligatorio")]
        [MaxLength(60, ErrorMessage ="El numero maximo de caracteres es 100")]
        public string Nombre { get; set; }
    }

    public class GetCategoriaDto
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "El nombre es obligatorio")]
        [MaxLength(60, ErrorMessage = "El numero maximo de caracteres es 100")]
        public string Nombre { get; set; }
    }

    public class UpdateCategoriaDto 
    {
        [Required(ErrorMessage = "El nombre es obligatorio")]
        [MaxLength(60, ErrorMessage = "El numero maximo de caracteres es 100")]
        public string Nombre { get; set; }
        public DateTime FechaCreacion { get; set; }
    }
}
