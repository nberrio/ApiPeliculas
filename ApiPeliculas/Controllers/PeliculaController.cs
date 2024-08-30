using ApiPeliculas.Models.Dtos;
using ApiPeliculas.Models;
using ApiPeliculas.Repository.IRepository;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace ApiPeliculas.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class PeliculaController : ControllerBase
    {
        private readonly IPeliculaRepository _pelRepo;
        private readonly IMapper _mapper;

        public PeliculaController(IPeliculaRepository ctRepo, IMapper mapper)
        {
            _pelRepo = ctRepo;
            _mapper = mapper;
        }

        [AllowAnonymous]
        [HttpGet("Getpeliculas")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Getpeliculas()
        {
            var listapeliculas = _pelRepo.GetPelicula();
            var listapeliculasDto = _mapper.Map<List<PeliculaDto>>(listapeliculas);
            return Ok(listapeliculasDto);
        }

        [AllowAnonymous]
        [HttpGet("GetPeliculaById")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetpeliculasById(int CategoriId)
        {
            Pelicula peliculasId = _pelRepo.GetPeliculaById(CategoriId);
            if (peliculasId == null)
            {
                return NotFound();
            }

            var Respuesta = _mapper.Map<PeliculaDto>(peliculasId);
            return Ok(Respuesta);
        }

        [Authorize(Roles = "admin")]
        [HttpPost("Createpeliculas")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult peliculasCreate([FromBody] PeliculaDto CrearpeliculasDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (CrearpeliculasDto == null)
            {

                return BadRequest(ModelState);
            }
            if (_pelRepo.GetExists(CrearpeliculasDto.Nombre))
            {
                ModelState.AddModelError("", "La peliculas ya Existe");
                return StatusCode(404, ModelState);
            }
            var nuevapeliculas = _mapper.Map<Pelicula>(CrearpeliculasDto);
            if (!_pelRepo.CreatePelicula(nuevapeliculas))
            {
                ModelState.AddModelError("", $"No fue posible crear la peliculas{nuevapeliculas.Nombre}");
                return StatusCode(404, ModelState);
            }

            return Ok(StatusCode(200));
        }

        [Authorize(Roles = "admin")]
        [HttpPatch("Updatepeliculas")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult peliculasUpdate([FromBody] PeliculaDto UPdatepeliculas)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (UPdatepeliculas == null)
            {

                return BadRequest(ModelState);
            }
            if (!_pelRepo.GetExists(UPdatepeliculas.Id))
            {
                ModelState.AddModelError("", "La peliculas no Existe");
                return StatusCode(404, ModelState);
            }
            var nuevapeliculas = _mapper.Map<Pelicula>(UPdatepeliculas);
            if (!_pelRepo.UpdatePelicula(nuevapeliculas))
            {
                return BadRequest();
            }
            return Ok();
        }

        [Authorize(Roles = "admin")]
        [HttpDelete("Deletepeliculas")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Deletepeliculas(int peliculasId)
        {
            if (!_pelRepo.GetExists(peliculasId))
            {
                ModelState.AddModelError("", "La peliculas No Existe");
                return StatusCode(402, ModelState);
            }
            Pelicula peliculas = new Pelicula
            {
                Id = peliculasId
            };
            bool listaCateogira = _pelRepo.DeletePelicula(peliculas);

            if (!listaCateogira)
            {
                return BadRequest();
            }
            return Ok(listaCateogira);
        }

        [AllowAnonymous]
        [HttpGet("GetPeliculaByName")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetpeliculasByName(string NombrePelicula)
        {
            var peliculasName = _pelRepo.SearchPelicula(NombrePelicula);
            if (peliculasName == null)
            {
                return NotFound();
            }

            var Respuesta = _mapper.Map<PeliculaDto>(peliculasName);
            return Ok(Respuesta);
        }

        [AllowAnonymous]
        [HttpGet("GetPeliculaByCategory")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetPeliculaByCategory(int CategoriaId)
        {
            var peliculas = _pelRepo.GetPeliculaByCategory(CategoriaId);
            if (peliculas == null)
            {
                return NotFound();
            }

            var Respuesta = _mapper.Map<PeliculaDto>(peliculas);
            return Ok(Respuesta);
        }
    }
}
