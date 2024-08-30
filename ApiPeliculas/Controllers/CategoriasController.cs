namespace ApiPeliculas.Controllers
{
    using ApiPeliculas.Models;
    using ApiPeliculas.Models.Dtos;
    using ApiPeliculas.Repository.IRepository;
    using AutoMapper;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

 
    [ApiController]
    [ResponseCache(Duration =20)]
    [Route("api/[controller]")]
    public class CategoriasController : ControllerBase
    {
        private readonly ICategoriaRepository _ctRepo;
        private readonly IMapper _mapper;
        
        public CategoriasController(ICategoriaRepository ctRepo, IMapper mapper)
        {
            _ctRepo = ctRepo;
            _mapper = mapper;
        }

        [AllowAnonymous]
        [HttpGet("Getcategorias")]
        [ResponseCache(Location = ResponseCacheLocation.None, NoStore = false)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetCategorias() 
        {
            var listaCategoria = _ctRepo.GetCategoria();
            var listaCategoriaDto = _mapper.Map<List<CategoriaDto>>(listaCategoria);
            return Ok(listaCategoriaDto);
        }

        [AllowAnonymous]
        [HttpGet("GetCateogiasById")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetCategoriasById(int CategoriId)
        {
            Categoria CategoriaId = _ctRepo.GetCategoriaById(CategoriId);
            if (CategoriaId == null) 
            {
                return NotFound();
            }

            var Respuesta = _mapper.Map<CategoriaDto>(CategoriaId);
            return Ok(Respuesta);
        }

        [Authorize(Roles = "admin")]
        [HttpPost("Createcategorias")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult CategoriasCreate([FromBody] CrearCategoriaDto CrearCategoriaDto)
        {
            if (!ModelState.IsValid) 
            {
                return BadRequest(ModelState);
            }
            if (CrearCategoriaDto == null) 
            {

                return BadRequest(ModelState);
            }
            if (_ctRepo.GetExists(CrearCategoriaDto.Nombre))
            {
                ModelState.AddModelError("", "La categoria ya Existe");
                return StatusCode(404, ModelState);
            }
            var nuevaCategoria = _mapper.Map<Categoria>(CrearCategoriaDto);
            if (!_ctRepo.CreateCategoria(nuevaCategoria)) 
            {
                ModelState.AddModelError("", $"No fue posible crear la categoria{nuevaCategoria.Nombre}");
                return StatusCode(404, ModelState);
            }

            return Ok(StatusCode(200)); 
        }

        [Authorize(Roles = "admin")]
        [HttpPatch("UpdateCategoria")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult CategoriaUpdate([FromBody] GetCategoriaDto UPdateCategoria) 
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (UPdateCategoria == null)
            {

                return BadRequest(ModelState);
            }
            if (!_ctRepo.GetExists(UPdateCategoria.Id))
            {
                ModelState.AddModelError("", "La categoria no Existe");
                return StatusCode(404, ModelState);
            }
            var nuevaCategoria = _mapper.Map<Categoria>(UPdateCategoria);
            bool listaCategoria = _ctRepo.UpdateCategoria(nuevaCategoria);
            if(!listaCategoria) 
            {
                return BadRequest();
            }
            return Ok(listaCategoria);
        }

        [Authorize(Roles = "admin")]
        [HttpDelete("DeleteCategoria")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult DeleteCategoria(int CategoriaId) 
        {
            if (!_ctRepo.GetExists(CategoriaId))
            {
                ModelState.AddModelError("", "La categoria No Existe");
                return StatusCode(402, ModelState);
            }
            Categoria categoria = new Categoria
            {
                Id = CategoriaId
            };
            bool listaCateogira = _ctRepo.DeleteCategoria(categoria);

            if(!listaCateogira) 
            {
                return BadRequest();
            }
            return Ok(listaCateogira);
        }
    }
}
