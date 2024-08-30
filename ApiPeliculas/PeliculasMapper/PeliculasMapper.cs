namespace ApiPeliculas.PeliculasMapper
{
    using ApiPeliculas.Models;
    using ApiPeliculas.Models.Dtos;
    using AutoMapper;

    public class PeliculasMapper : Profile
    {
        public PeliculasMapper()
        {
            CreateMap<Categoria, CategoriaDto>().ReverseMap();
            CreateMap<Categoria, CrearCategoriaDto>().ReverseMap();
            CreateMap<Categoria, GetCategoriaDto>().ReverseMap();
            CreateMap<Pelicula, PeliculaDto>().ReverseMap();
        }
    }
}
