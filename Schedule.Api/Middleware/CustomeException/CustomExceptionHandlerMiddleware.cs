using System.Net;
using FluentValidation;
using Newtonsoft.Json;
using Schedule.Core.Common.Exceptions;

namespace Schedule.Api.Middleware.CustomException;

public sealed class CustomExceptionHandlerMiddleware(RequestDelegate next)
{
    public async Task Invoke(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var code = HttpStatusCode.InternalServerError;
        var result = "Неизвестная ошибка.";
        
        switch (exception)
        {
            case UnauthorizedAccessException:
                code = HttpStatusCode.Unauthorized;
                result = "Не авторизован.";
                break;
            case NotAccessException:
                code = HttpStatusCode.Forbidden;
                result = "Нет доступа.";
                break;
            case ValidationException:
                code = HttpStatusCode.BadRequest;
                result = "Некорректный запрос.";
                break;
            case NotFoundException:
                code = HttpStatusCode.NotFound;
                result = "Не найдено.";
                break;
            case IncorrectAuthorizationDataException:
                code = HttpStatusCode.BadRequest;
                result = "Неверный логин или пароль.";
                break;
            case AlreadyExistsException alreadyExistsException:
                code = HttpStatusCode.Conflict;
                result = $"{alreadyExistsException.Value} уже существует, или ранее был удален.\n" +
                         $"Вы можете восстановить его на вкладке 'Удаленные'.";
                break;
            case DeletedException:
                code = HttpStatusCode.NotFound;
                result = "Это содержимое было удалено.";
                break;
            case ScheduleException scheduleException:
                code = HttpStatusCode.Conflict;
                result = scheduleException.Message;
                break;
        }

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)code;

        return context.Response.WriteAsync( 
            JsonConvert.SerializeObject(new
            {
                error = result
            }));
    }
}