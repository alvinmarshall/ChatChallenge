using App.DTO;
using Bot.DTO;
using Microsoft.AspNetCore.Mvc;

namespace Bot.Exceptions;

public static class ExceptionConfiguration
{
    public static ApiResponse<object> HandleValidationExceptionResponse(ActionContext context)
    {
        var problemDetails = new ValidationProblemDetails(context.ModelState);
        var errors = problemDetails.Errors
            .Select(detail =>
                new ApiError() { Message = $"{detail.Value.GetValue(0)}" }
            ).ToList();

        var apiResponse = new ApiResponse<object>()
        {
            Success = false,
            Errors = errors
        };
        return apiResponse;
    }
}