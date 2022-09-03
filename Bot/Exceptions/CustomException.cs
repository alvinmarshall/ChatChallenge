

using App.Exceptions;
using Bot.DTO;

namespace Bot.Exceptions
{
    public class CustomException : Exception
    {
        public ExceptionTypes ExceptionTypes { get; }
        public string? ReferenceId { get; }
        public List<ApiError> Errors { get; }

        protected CustomException(string message) : base(message)
        {
            ExceptionTypes = ExceptionTypes.ServiceBadRequest;
            var error = new ApiError
            {
                Message = message
            };

            Errors = new List<ApiError> { error };
        }

        protected CustomException(ExceptionTypes exceptionTypes, string referenceId, List<ApiError> errors)
        {
            ExceptionTypes = exceptionTypes;
            ReferenceId = referenceId;
            Errors = errors;
        }

        protected CustomException(string message, ExceptionTypes exceptionTypes) : base(message)
        {
            ExceptionTypes = exceptionTypes;
            var error = new ApiError
            {
                Message = message
            };
            Errors = new List<ApiError> { error };
        }
    }
}