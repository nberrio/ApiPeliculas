namespace ApiPeliculas.Repository
{
    using ApiPeliculas.Data;
    using ApiPeliculas.Models;
    using ApiPeliculas.Models.Dtos;
    using ApiPeliculas.Repository.IRepository;
    using AutoMapper;
    using System.Collections.Generic;
    using static System.Runtime.InteropServices.JavaScript.JSType;

    public class CategoriaRepository : ICategoriaRepository
    {

        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public CategoriaRepository(ApplicationDbContext _bd, IMapper _mapper)
        {
            context = _bd;
            mapper = _mapper;
        }

        public bool CreateCategoria(Categoria categoria)
        {
            categoria.FechaCreacion = DateTime.Now;
            context.Categoria.Add(categoria);
            return Save();
        }

        public bool DeleteCategoria(Categoria categoria)
        {
            context.Remove(categoria);
            return Save();
        }

        public ICollection<Categoria> GetCategoria()
        {
            ICollection<Categoria> categorias = context.Categoria.OrderBy(c => c.Nombre).ToList();
            return categorias;
        }

        public Categoria GetCategoriaById(int categoriad)
        {
            Categoria categoria = context.Categoria.FirstOrDefault(c => c.Id == categoriad);
            return categoria;
        }

        public bool GetExists(string nombre)
        {
            bool valor = context.Categoria.Any(c => c.Nombre.ToLower().Trim() == nombre.ToLower().Trim());
            return valor;
        }

        public bool GetExists(int id)
        {
            bool valor = context.Categoria.Any(c => c.Id == id);
            return valor;
        }

        public bool Save()
        {
            return context.SaveChanges() >= 0 ? true : false;
        }

        public bool UpdateCategoria(Categoria categoria)
        {
            categoria.FechaCreacion = DateTime.Now;
            context.Categoria.Update(categoria);
            return Save();
        }
    }
}
