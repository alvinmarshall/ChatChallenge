
using App.DTO;

namespace App.Exceptions
{
    public class UserServiceException : CustomException
    {
        public UserServiceException(string message) : base(message)
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