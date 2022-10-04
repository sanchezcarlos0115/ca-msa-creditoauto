
using ExceptionsAp = camsacreditoauto.Domain.Comun.Exceptions;
using System.Net;
using System.Text.Json;
namespace camsacreditoauto.API.Middleware;


public class ErrorHandlerMiddleware
{
    private readonly RequestDelegate _next;

    public ErrorHandlerMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception error)
        {
            var response = context.Response;
            response.ContentType = "application/json";

            response.StatusCode = error switch
            {
                ExceptionsAp.ApiException e => (int)HttpStatusCode.BadRequest,// custom application error
                ExceptionsAp.NotFoundException e => (int)HttpStatusCode.NotFound,
                KeyNotFoundException e => (int)HttpStatusCode.NotFound,// not found error
                _ => (int)HttpStatusCode.InternalServerError,// unhandled error
            };
            var result = JsonSerializer.Serialize(new { message = error?.Message });
            await response.WriteAsync(result);
        }
    } 

}