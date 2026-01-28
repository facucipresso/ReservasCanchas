using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage.Json;
using ReservasCanchas.BusinessLogic.Exceptions;

namespace ReservasCanchas.BusinessLogic.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next; 
        public ExceptionMiddleware(RequestDelegate next) 
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext) 
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        
        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var problemDetails = new ProblemDetails
            {
                Title = "Ha ocurrido un error al procesar la respuesta",
                Detail = exception.Message,
                Instance = context.Request.Path
            };

            context.Response.ContentType = "application/json";
            switch (exception)
            {
                case NotFoundException:
                    problemDetails.Status = StatusCodes.Status404NotFound;
                    problemDetails.Title = "Recurso no encontrado";
                    break;
                case BadRequestException:
                    problemDetails.Status = StatusCodes.Status400BadRequest;
                    problemDetails.Title = "Solicitud incorrecta";
                    break;
                case ForbiddenException:
                    problemDetails.Status = StatusCodes.Status403Forbidden;
                    problemDetails.Title = "Acceso prohibido";
                    break;
                case ConflictException:
                    problemDetails.Status = StatusCodes.Status409Conflict;
                    problemDetails.Title = "Conflicto en la solicitud";
                    break;
                default:
                    problemDetails.Status = StatusCodes.Status500InternalServerError;
                    problemDetails.Title = "Error interno del servidor";
                    break;
            }
            
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = problemDetails.Status.Value;

            var json = JsonSerializer.Serialize(problemDetails);
            return context.Response.WriteAsync(json);

        }
        /*
        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            // Si la respuesta ya comenzó, NO podemos escribir otro body
            if (context.Response.HasStarted)
            {
                Console.WriteLine("No se puede escribir la respuesta de error porque ya se inició.");
                return Task.CompletedTask;
            }

            var problemDetails = new ProblemDetails
            {
                Title = "Ha ocurrido un error al procesar la respuesta",
                Detail = exception.Message,
                Instance = context.Request.Path
            };

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = exception switch
            {
                NotFoundException => StatusCodes.Status404NotFound,
                BadRequestException => StatusCodes.Status400BadRequest,
                _ => StatusCodes.Status500InternalServerError
            };

            var json = JsonSerializer.Serialize(problemDetails);
            return context.Response.WriteAsync(json);
        }
        */
    }
}
