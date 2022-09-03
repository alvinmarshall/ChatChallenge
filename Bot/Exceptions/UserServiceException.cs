
using Bot.DTO;
using Bot.Exceptions;

namespace App.Exceptions
{
    public class UserServiceException : CustomException
    {
        protected UserServiceException(string message) : base(message)
        {
        }

        protected UserServiceException(ExceptionTypes exceptionTypes, string referenceId, List<ApiError> errors) : base(exceptionTypes, referenceId, errors)
        {
        }

        public UserServiceException(string message, ExceptionTypes exceptionTypes) : base(message, exceptionTypes)
        {
        }
    }
}