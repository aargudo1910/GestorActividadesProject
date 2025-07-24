using System.Net;

namespace GestorActividades.Middleware
{
    public class ApiRespuesta<T>
    {
        public bool Exito { get; set; }
        public HttpStatusCode Codigo { get; set; }
        public string? Mensaje { get; set; }
        public T? Datos { get; set; }

        public static ApiRespuesta<T> Ok(T datos, string? mensaje = null) =>
        new() { Exito = true, Codigo = HttpStatusCode.OK, Datos = datos, Mensaje = mensaje };

        public static ApiRespuesta<T> Fail(string mensaje, HttpStatusCode codigo = HttpStatusCode.BadRequest) =>
            new() { Exito = false, Codigo = codigo, Mensaje = mensaje };
    }
}
