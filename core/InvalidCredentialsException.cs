using System;

namespace trellabit.core
{
    /// <summary>
    /// Thrown when authentication credentials are invalid.
    /// </summary>
    /// <seealso cref="System.Exception" />
    public class InvalidCredentialsException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidCredentialsException"/> class.
        /// </summary>
        public InvalidCredentialsException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidCredentialsException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public InvalidCredentialsException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidCredentialsException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="inner">The inner exception.</param>
        public InvalidCredentialsException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}