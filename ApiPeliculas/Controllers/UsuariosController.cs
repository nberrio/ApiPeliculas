using ApiPeliculas.Models;
using ApiPeliculas.Models.Dtos;
using ApiPeliculas.Repository.IRepository;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiPeliculas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {

        private readonly IUsuarioRepository _usRepo;
        private readonly IMapper _mapper;
        protected RespuestaApi _respuestaApi;

        public UsuariosController(IUsuarioRepository ctRepo, IMapper mapper)
        {
            _usRepo = ctRepo;
            _mapper = mapper;
            this._respuestaApi = new();
        }

        [Authorize(Roles = "admin")]
        [HttpGet("GetUsuarios")]
        [ResponseCache(CacheProfileName = "porDefecto30segundos")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetUsuarios()
        {
            var listaUsuario = _usRepo.GetUsuario();
            var listaUsuarioDto = _mapper.Map<List<UsuarioDto>>(listaUsuario);
            return Ok(listaUsuarioDto);
        }

        [Authorize(Roles = "admin")]
        [HttpGet("GetUsuarioById")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetUsuariosById(int usuarioId)
        {
            Usuario UsuarioId = _usRepo.GetUsuarioById(usuarioId);
            if (UsuarioId == null)
            {
                return NotFound();
            }

            var Respuesta = _mapper.Map<UsuarioDto>(UsuarioId);
            return Ok(Respuesta);
        }

        [Authorize(Roles = "admin")]
        [HttpGet("GetUsuarioByName")]
        [ResponseCache(CacheProfileName = "porDefecto30segundos")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetUsuarioByName(string usuarioName)
        {
            if (!_usRepo.IsUniqueUser(usuarioName))
            {
                return NotFound();
            }
            else 
            {
                return Ok();
            }
        }

        [HttpPost("CreateUsuario")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UsuarioCreate([FromBody] UsuarioRegistroDto UsuarioRegistroDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (UsuarioRegistroDto == null)
            {
                return BadRequest(ModelState);
            }
            if (!_usRepo.IsUniqueUser(UsuarioRegistroDto.Nombre))
            {
                _respuestaApi.statusCode = System.Net.HttpStatusCode.BadRequest;
                _respuestaApi.IsSuccess = false;
                _respuestaApi.ErrrorMessages.Add("El nombre de usuario ya existe");

                return BadRequest(_respuestaApi);
            }
            Usuario respuesta = await _usRepo.Registro(UsuarioRegistroDto);
            if (respuesta == null)
            {
                _respuestaApi.statusCode = System.Net.HttpStatusCode.BadRequest;
                _respuestaApi.IsSuccess = false;
                _respuestaApi.ErrrorMessages.Add("Error en el registro");

                return BadRequest(_respuestaApi);
            }
            _respuestaApi.statusCode = System.Net.HttpStatusCode.OK;
            _respuestaApi.IsSuccess = true;
           
            return Ok(_respuestaApi);
        }

        [HttpPost("LoginUsuario")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> LoginCreate([FromBody] UsuarioLoguinDto UsuarioLoguinDto)
        {
            UsuarioLoguinRespuestaDto respuesta = await _usRepo.Login(UsuarioLoguinDto);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (UsuarioLoguinDto == null)
            {
                return BadRequest(ModelState);
            }
            if (respuesta.Usuario == null || string.IsNullOrEmpty(respuesta.Token))
            {
                _respuestaApi.statusCode = System.Net.HttpStatusCode.BadRequest;
                _respuestaApi.IsSuccess = false;
                _respuestaApi.ErrrorMessages.Add("El nombre de usuario o contraseña son incorrectos");

                return BadRequest(_respuestaApi);
            }
            _respuestaApi.statusCode = System.Net.HttpStatusCode.OK;
            _respuestaApi.IsSuccess = true;
            _respuestaApi.Result = respuesta;
            return Ok(_respuestaApi);
        }
    }
}
