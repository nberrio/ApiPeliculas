namespace ApiPeliculas.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Usuario
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string NombreUsuario { get; set; }
        [Required]
        public string Nombre { get; set; }
        [Required]
        public string Password { get; set; }
        public string Role { get; set; }
        public DateTime FechaCreacion { get; set; }
    }
}
