namespace ApiPeliculas.Repository
{
    using ApiPeliculas.Data;
    using ApiPeliculas.Models;
    using ApiPeliculas.Models.Dtos;
    using ApiPeliculas.Repository.IRepository;
    using AutoMapper;
    using Microsoft.AspNetCore.Mvc.Formatters;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using static System.Runtime.InteropServices.JavaScript.JSType;

    public class PeliculaRepository : IPeliculaRepository
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public PeliculaRepository(ApplicationDbContext _bd, IMapper _mapper) 
        {
            context = _bd;
            mapper = _mapper;
        }

        public bool CreatePelicula(Pelicula pelicula)
        {
            pelicula.FechaCreacion = DateTime.Now;
            context.peliculas.Add(pelicula);
            return Save();
        }

        public bool DeletePelicula(Pelicula pelicula)
        {
            context.Remove(pelicula);
            return Save();
        }

        public bool GetExists(string nombre)
        {
            bool valor = context.peliculas.Any(c => c.Nombre.ToLower().Trim() == nombre.ToLower().Trim());
            return valor;
        }

        public bool GetExists(int id)
        {
            bool valor = context.peliculas.Any(c => c.Id == id);
            return valor;
        }

        public ICollection<Pelicula> GetPelicula()
        {
            ICollection<Pelicula> pelicula = context.peliculas.OrderBy(c => c.Nombre).ToList();
            return pelicula;
        }

        public Pelicula GetPeliculaById(int peliculaId)
        {
            Pelicula pelicula = context.peliculas.FirstOrDefault(c => c.Id == peliculaId);
            return pelicula;
        }

        public bool UpdatePelicula(Pelicula pelicula)
        {
            pelicula.FechaCreacion = DateTime.Now;
            context.peliculas.Update(pelicula);
            return Save();
        }

        //Métodos para buscar peliculas por categoria y buscar pelicula por nombre.
        public ICollection<Pelicula> SearchPelicula(string nombre)
        {
            IQueryable<Pelicula> query = context.peliculas;
            if (!string.IsNullOrEmpty(nombre)) 
            {
                query = query.Where(e => e.Nombre.Contains(nombre) || e.Descripcion.Contains(nombre));
            }
            return query.ToList();
        }

        public ICollection<Pelicula> GetPeliculaByCategory(int catId)
        {
            return context.peliculas.Include(ca => ca.Categoria).Where(ca => ca.categoriaId == catId).ToList();
        }

        public bool Save()
        {
            return context.SaveChanges() >= 0 ? true : false;
        }
    }
}
