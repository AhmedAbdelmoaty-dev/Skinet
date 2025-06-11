
using Application.Exceptions;

namespace Skinet.Middlewares
{
    public class ErrorHandelingMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch(NotFoundException notFound)
            {
                context.Response.StatusCode = StatusCodes.Status404NotFound;
               await context.Response.WriteAsync(notFound.Message);
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                await context.Response.WriteAsync("Something went wrong ...");
            }
        }
    }
}
