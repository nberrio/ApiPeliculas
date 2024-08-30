using System.Net;

namespace ApiPeliculas.Models
{
    public class RespuestaApi
    {
        public RespuestaApi()
        {
            ErrrorMessages = new List<string>();
        }

        public HttpStatusCode statusCode { get; set; }
        public bool IsSuccess { get; set; }
        public List<string> ErrrorMessages { get; set; }
        public object Result { get; set; } 
    }
}
