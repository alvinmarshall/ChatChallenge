using Bot.DTO;

namespace Bot.Exceptions
{
    public class RecordNotFoundException : CustomException
    {
        public RecordNotFoundException(string message) : base(message, ExceptionTypes.RecordNotFound)
        {
        }

        public RecordNotFoundException(string message, ExceptionTypes exceptionTypes) : base(message, exceptionTypes)
        {
        }

        public RecordNotFoundException(ExceptionTypes exceptionTypes, string referenceId, List<ApiError> errors) : base(
            exceptionTypes, referenceId, errors)
        {
        }
    }
}