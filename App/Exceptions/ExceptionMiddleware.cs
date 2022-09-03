using System.Net;
using System.Net.Mime;
using System.Text.Json;
using App.DTO;

namespace App.Exceptions
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            var apiResponse = new ApiResponse<object>();
            try
            {
                await _next(context);
            }
            catch (Exception error)
            {
                var response = context.Response;
                response.ContentType = MediaTypeNames.Application.Json;
                switch (error)
                {
                    case InvalidOperationException e:
                        var errors =
                            new List<ApiError>(new[] { new ApiError() { Message = e.Message } });
                        apiResponse.Errors = errors;
                        apiResponse.Success = false;
                        response.StatusCode = GetExceptionType(ExceptionTypes.ServiceBadRequest);
                        break;
                    case RecordNotFoundException e:
                        _logger.LogInformation("{}", e.ExceptionTypes.ToString());
                        _logger.LogInformation("{}", e.ReferenceId);
                        apiResponse.Errors = e.Errors;
                        apiResponse.Success = false;
                        response.StatusCode = GetExceptionType(e.ExceptionTypes);
                        break;
                    case CustomException e:
                        _logger.LogError("{}", e.ExceptionTypes.ToString());
                        _logger.LogError("{}", e.ReferenceId);
                        apiResponse.Errors = e.Errors;
                        apiResponse.Success = false;
                        response.StatusCode = GetExceptionType(e.ExceptionTypes);
                        break;
                    default:
                        _logger.LogError("Application Encountered and unhandled Error: {Msg}", error.Message);
                        ApiError apiError = new ApiError
                        {
                            Message = "Sorry your request cannot be processed at this moment"
                        };
                        List<ApiError> apiErrors = new List<ApiError> { apiError };
                        apiResponse.Errors = apiErrors;
                        apiResponse.Success = false;
                        response.StatusCode = GetExceptionType(ExceptionTypes.InternalServerError);
                        break;
                }

                var result = JsonSerializer.Serialize(apiResponse);
                await response.WriteAsync(result);
            }
        }

        private static int GetExceptionType(ExceptionTypes exceptionTypes)
        {
            return exceptionTypes switch
            {
                ExceptionTypes.UnAuthorize => (int)HttpStatusCode.Unauthorized,
                ExceptionTypes.RecordNotFound => (int)HttpStatusCode.NotFound,
                ExceptionTypes.ServiceDown => (int)HttpStatusCode.ServiceUnavailable,
                ExceptionTypes.ServiceBadRequest => (int)HttpStatusCode.BadRequest,
                ExceptionTypes.ServiceTimeout => (int)HttpStatusCode.RequestTimeout,
                _ => (int)HttpStatusCode.InternalServerError
            };
        }
    }
}