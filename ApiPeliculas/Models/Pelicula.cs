namespace ApiPeliculas.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Pelicula
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Nombre { get; set; }
        public string RutaImegen { get; set; }
        public string Descripcion { get; set; }
        public string Duracion { get; set; }
        public enum TipoClasificacion { siete, trece, dieciseis, diecioocho}
        public TipoClasificacion Clasificacion { get; set; }
        public DateTime FechaCreacion { get; set; }

        [ForeignKey("categoriaId")]
        public int categoriaId { get; set; }
        public Categoria Categoria { get; set; }
    }
}
