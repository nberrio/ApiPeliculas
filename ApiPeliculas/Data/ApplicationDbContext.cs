namespace ApiPeliculas.Data
{
    using ApiPeliculas.Models;
    using Microsoft.EntityFrameworkCore;

    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext>options) : base(options)
        {
        }

        //Agregar los modelos aquí
        public DbSet<Categoria> Categoria { get; set; }
        public DbSet<Pelicula> peliculas { get; set; }
        public DbSet<Usuario> Usuario { get; set; }
    }
}
