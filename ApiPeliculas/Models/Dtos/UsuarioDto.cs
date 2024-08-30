namespace ApiPeliculas.Models.Dtos
{
    public class UsuarioDto
    {
        public int Id { get; set; }
        public string NombreUsuario { get; set; }
        public string Nombre { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public DateTime FechaCreacion { get; set; }
    }

    public class UsuarioRegistroDto 
    {
        public string NombreUsuario { get; set; }
        public string Nombre { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public DateTime FechaCreacion { get; set; }
    }

    public class UsuarioLoguinDto
    {
        public string NombreUsuario { get; set; }
        public string Password { get; set; }
    }

    public class UsuarioLoguinRespuestaDto
    {
        public Usuario Usuario { get; set; }
        public string Token { get; set; }
    }
}
