﻿using System;

namespace trellabit.core
{
    /// <summary>
    /// Thrown when the user options fail validation.
    /// </summary>
    /// <seealso cref="System.Exception" />
    class InvalidUserOptionsException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidUserOptionsException"/> class.
        /// </summary>
        public InvalidUserOptionsException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidUserOptionsException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public InvalidUserOptionsException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidUserOptionsException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="inner">The inner exception.</param>
        public InvalidUserOptionsException(string message, Exception inner)
            : base(message, inner)
        {
        }

    }
}
