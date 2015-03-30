﻿namespace BundleTransformer.Core
{
	using System;

	/// <summary>
	/// The exception that is thrown when a specified value is null or empty
	/// </summary>
	public sealed class EmptyValueException : Exception
	{
		/// <summary>
		/// Initializes a new instance of the <code>BundleTransformer.Core.EmptyValueException</code> class
		/// with a specified error message
		/// </summary>
		/// <param name="message">The message that describes the error</param>
		public EmptyValueException(string message)
			: base(message)
		{ }

		/// <summary>
		/// Initializes a new instance of the <code>BundleTransformer.Core.EmptyValueException</code> class
		/// with a specified error message and a reference to the inner exception that is the cause of this exception
		/// </summary>
		/// <param name="message">The error message that explains the reason for the exception</param>
		/// <param name="innerException">The exception that is the cause of the current exception</param>
		public EmptyValueException(string message, Exception innerException)
			: base(message, innerException)
		{ }
	}
}