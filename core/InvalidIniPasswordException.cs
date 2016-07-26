using System;

namespace Trellabit.Core
{
    /// <summary>
    /// Thrown when the password is incorrect for an encrypted Ini file.
    /// </summary>
    /// <seealso cref="System.Exception" />
    public class InvalidIniPasswordException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidIniPasswordException"/> class.
        /// </summary>
        public InvalidIniPasswordException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidIniPasswordException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public InvalidIniPasswordException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidIniPasswordException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="inner">The inner exception.</param>
        public InvalidIniPasswordException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}