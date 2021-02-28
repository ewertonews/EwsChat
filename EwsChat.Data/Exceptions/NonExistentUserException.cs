using System;
using System.Runtime.Serialization;

namespace EwsChat.Data.Exceptions
{
    [Serializable]
    public class NonExistentUserException : Exception
    {
        public NonExistentUserException()
        {
        }

        public NonExistentUserException(string message) : base(message)
        {
        }

        public NonExistentUserException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected NonExistentUserException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}