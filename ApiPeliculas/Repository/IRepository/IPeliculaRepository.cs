namespace ApiPeliculas.Repository.IRepository
{
    using ApiPeliculas.Models;

    public interface IPeliculaRepository
    {
        ICollection<Pelicula> GetPelicula();
        Pelicula GetPeliculaById(int peliculaId);
        bool GetExists(string nombre);
        bool GetExists(int id);
        bool CreatePelicula(Pelicula pelicula);
        bool UpdatePelicula(Pelicula pelicula);
        bool DeletePelicula(Pelicula pelicula);
        ICollection<Pelicula> SearchPelicula(string nombre);
        ICollection<Pelicula> GetPeliculaByCategory(int catId);
        bool Save();
    }
}
