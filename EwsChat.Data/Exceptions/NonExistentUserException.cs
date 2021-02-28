using System;

namespace EwsChat.Data.Exceptions
{
    [Serializable]
    public class NonExistentUserException : Exception
    {
        public NonExistentUserException(string message) : base(message)
        {
        }
    }
}