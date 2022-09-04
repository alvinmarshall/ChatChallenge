using App.DTO;

namespace App.Exceptions
{
    public class ChatServiceException : CustomException
    {
        public ChatServiceException(string message) : base(message)
        {
        }

        protected ChatServiceException(ExceptionTypes exceptionTypes, string referenceId, List<ApiError> errors) : base(exceptionTypes, referenceId, errors)
        {
        }

        protected ChatServiceException(string message, ExceptionTypes exceptionTypes) : base(message, exceptionTypes)
        {
        }
    }
}