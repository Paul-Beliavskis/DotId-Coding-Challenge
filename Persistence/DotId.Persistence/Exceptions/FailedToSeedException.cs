using System;

namespace DotId.Persistence.Exceptions
{
    public class FailedToSeedException : Exception
    {
        public FailedToSeedException(string message, Exception exception) : base(message, exception)
        {

        }
    }
}
