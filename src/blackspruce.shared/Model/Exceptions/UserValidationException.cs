#nullable enable

using System;

namespace BlackSpruce.Exceptions
{
    public class UserValidationException : Exception
    {
        public UserValidationException(string? message)
            : base(message)
        {

        }
    }
}
