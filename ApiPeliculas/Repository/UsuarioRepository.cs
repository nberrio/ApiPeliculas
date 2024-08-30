namespace ApiPeliculas.Repository
{
    using ApiPeliculas.Data;
    using ApiPeliculas.Models;
    using ApiPeliculas.Models.Dtos;
    using ApiPeliculas.Repository.IRepository;
    using AutoMapper;
    using Microsoft.IdentityModel.Tokens;
    using System.Collections.Generic;
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Text;
    using System.Threading.Tasks;
    using XSystem.Security.Cryptography;

    public class UsuarioRepository : IUsuarioRepository
    {

        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;
        private string claveSecreta;


        public UsuarioRepository(ApplicationDbContext _bd, IMapper _mapper, IConfiguration config)
        {
            context = _bd;
            mapper = _mapper;
            claveSecreta = config.GetValue<string>("ApiSetting:Secreta");
        }

        public ICollection<Usuario> GetUsuario()
        {
            ICollection<Usuario> Usuarios = context.Usuario.OrderBy(c => c.Nombre).ToList();
            return Usuarios;
        }

        public Usuario GetUsuarioById(int Usuariod)
        {
            Usuario Usuario = context.Usuario.FirstOrDefault(c => c.Id == Usuariod);
            return Usuario;
        }

        public bool IsUniqueUser(string nombre)
        {
            var UsurioBd = context.Usuario.FirstOrDefault(c => c.NombreUsuario.ToLower().Trim() == nombre.ToLower().Trim());
            if (UsurioBd == null)
            {
                return true;
            }
           
             return false;
        }

        public async Task<UsuarioLoguinRespuestaDto> Login(UsuarioLoguinDto UsuarioLoguinDto)
        {
            var passwordEncriptado = obtenermd5(UsuarioLoguinDto.Password);

            var usuario = context.Usuario.FirstOrDefault(
                u => u.NombreUsuario.ToLower() == UsuarioLoguinDto.NombreUsuario.ToLower()
                && u.Password == passwordEncriptado
                );
            //validamos si el usuario no existe con la combinacion de usuario y contraseña correcta
            if (usuario == null)
            {
                return new UsuarioLoguinRespuestaDto()
                {
                    Token = "",
                    Usuario = null
                };
            }
            //JWT
            var manejardorToken = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(claveSecreta);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, usuario.NombreUsuario.ToString()),
                    new Claim(ClaimTypes.Role, usuario.Role.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = manejardorToken.CreateToken(tokenDescriptor);
            UsuarioLoguinRespuestaDto usuarioLoguinRespuestaDto = new UsuarioLoguinRespuestaDto()
            {
                Token = manejardorToken.WriteToken(token),
                Usuario = usuario
            };

            return usuarioLoguinRespuestaDto;
        }

        public async Task<Usuario> Registro(UsuarioRegistroDto usuarioRegistroDto)
        {
            var passwordEncriptado = obtenermd5(usuarioRegistroDto.Password);

            Usuario usuario = new Usuario()
            {
                NombreUsuario = usuarioRegistroDto.NombreUsuario,
                Password = passwordEncriptado,
                Nombre = usuarioRegistroDto.Nombre,
                Role = usuarioRegistroDto.Role,
                FechaCreacion = usuarioRegistroDto.FechaCreacion
            };

            context.Usuario.Add(usuario);
            await context.SaveChangesAsync();
            usuario.Password = passwordEncriptado;

            return usuario;
        }

        //Este metodo encripta un string
        public static string obtenermd5(string valor) 
        {
            MD5CryptoServiceProvider x = new MD5CryptoServiceProvider();
            byte[] data = System.Text.Encoding.UTF8.GetBytes(valor);
            data = x.ComputeHash(data);
            string resp = "";
            for (int i = 0; i < data.Length; i++)
                resp += data[i].ToString("x2").ToLower();
            return resp;
        }
    }
}
