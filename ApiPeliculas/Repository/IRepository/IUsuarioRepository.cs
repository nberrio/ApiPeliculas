namespace ApiPeliculas.Repository.IRepository
{
    using ApiPeliculas.Models;
    using ApiPeliculas.Models.Dtos;

    public interface IUsuarioRepository
    {
        ICollection<Usuario> GetUsuario();
        Usuario GetUsuarioById(int UsuarioId);
        bool IsUniqueUser(string usuario);
        Task<UsuarioLoguinRespuestaDto> Login(UsuarioLoguinDto UsuarioLoguinDto);
        Task<Usuario> Registro(UsuarioRegistroDto usuarioRegistroDto);
    }
}