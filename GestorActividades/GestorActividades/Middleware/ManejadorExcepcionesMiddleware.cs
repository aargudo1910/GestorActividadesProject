using GestorActividades.Excepciones;
using GestorActividades.Excepciones.Base;
using System.Net;
using System.Text.Json;

public class ManejadorExcepcionesMiddleware
{
    private readonly RequestDelegate _next;

    public ManejadorExcepcionesMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (ExcepcionNoEncontrado ex)
        {
            await ManejarAsync(context, ex.Message, HttpStatusCode.NotFound);
        }
        catch (ExcepcionConflicto ex)
        {
            await ManejarAsync(context, ex.Message, HttpStatusCode.Conflict);
        }
        catch (ExcepcionValidacion ex)
        {
            await ManejarAsync(context, ex.Message, HttpStatusCode.BadRequest);
        }
        catch (ExcepcionNegocio ex)
        {
            await ManejarAsync(context, ex.Message, HttpStatusCode.BadRequest);
        }
        catch (Exception)
        {
            await ManejarAsync(context, "Error interno del servidor", HttpStatusCode.InternalServerError);
        }
    }


    private Task ManejarAsync(HttpContext context, string mensaje, HttpStatusCode status)
    {
        context.Response.StatusCode = (int)status;
        context.Response.ContentType = "application/json";

        var respuesta = new
        {
            exito = false,
            mensaje
        };

        return context.Response.WriteAsync(JsonSerializer.Serialize(respuesta));
    }
}
