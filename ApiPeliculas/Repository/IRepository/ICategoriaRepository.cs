namespace ApiPeliculas.Repository.IRepository
{
    using ApiPeliculas.Models;

    public interface ICategoriaRepository
    {
        ICollection<Categoria> GetCategoria();
        Categoria GetCategoriaById(int categoriaId);
        bool GetExists(string nombre);
        bool GetExists(int id);
        bool CreateCategoria(Categoria categoria);
        bool UpdateCategoria(Categoria categoria);
        bool DeleteCategoria(Categoria categoria);
        bool Save();
    }
}
