using System;

namespace SimPaulOnbase.Core.Exceptions
{
    public class OnbaseConnectionException : Exception
    {
        public OnbaseConnectionException()
        {
        }

        public OnbaseConnectionException(string message) : base(message)
        {
        }

        public OnbaseConnectionException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
