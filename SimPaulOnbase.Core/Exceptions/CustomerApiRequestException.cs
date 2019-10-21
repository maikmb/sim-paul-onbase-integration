using System;

namespace SimPaulOnbase.Core.Exceptions
{
    public class CustomerApiRequestException : Exception
    {
        public CustomerApiRequestException()
        {
        }

        public CustomerApiRequestException(string message) : base(message)
        {
        }

        public CustomerApiRequestException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
