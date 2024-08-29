using Microsoft.AspNetCore.Http;
using System.Text.Json;
using URLShortener.Application.Exceptions;
using URLShortener.Application.ViewModels;

namespace URLShortener.Application;

public class GlobalExceptionHandler(RequestDelegate next)
{
    public async Task Invoke(HttpContext context)
    {
        var result = new ResultViewModel();

        try
        {
            await next(context);
        }
        catch (BadRequestException ex)
        {
            result.BadRequest(ex.Error, ex.Errors);
            await SetContext(context, result, StatusCodes.Status400BadRequest);
        }
        catch (NotFoundException ex)
        {
            result.NotFound(ex.Error);
            await SetContext(context, result, StatusCodes.Status404NotFound);
        }
        catch (ConflictException ex)
        {
            result.Conflict(ex.Error);
            await SetContext(context, result, StatusCodes.Status409Conflict);
        }
        catch (Exception ex)
        {
            result.InternalServerError(ex.Message);
            await SetContext(context, result, StatusCodes.Status500InternalServerError);
        }
    }

    private static async Task SetContext(HttpContext context, ResultViewModel result, int statusCode)
    {
        context.Response.StatusCode = statusCode;
        context.Response.ContentType = "application/json";
        await context.Response.WriteAsync(JsonSerializer.Serialize(result));
    }
}

