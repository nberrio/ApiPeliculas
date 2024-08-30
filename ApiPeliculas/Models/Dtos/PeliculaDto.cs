namespace ApiPeliculas.Models.Dtos
{
    using System.ComponentModel.DataAnnotations.Schema;
    using System.ComponentModel.DataAnnotations;

    public class PeliculaDto
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        [MaxLength(60, ErrorMessage = "El numero maximo de caracteres es 100")]
        public string Nombre { get; set; }
        public string RutaImegen { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        [MaxLength(60, ErrorMessage = "El numero maximo de caracteres es 100")]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        [MaxLength(60, ErrorMessage = "El numero maximo de caracteres es 100")]
        public string Duracion { get; set; }
        public enum TipoClasificacion { siete, trece, dieciseis, diecioocho }
        public TipoClasificacion Clasificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public int categoriaId { get; set; }
    }
}
