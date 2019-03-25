namespace Zadatak.Exceptions
{
    /// <summary>
    /// Format of exception in response
    /// </summary>
    public class MyException
    {
        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        /// <value>
        /// The message.
        /// </value>
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets the exception.
        /// </summary>
        /// <value>
        /// The exception.
        /// </value>
        public string Exception { get; set; }

        /// <summary>
        /// Gets or sets the stack trace.
        /// </summary>
        /// <value>
        /// The stack trace.
        /// </value>
        public string StackTrace { get; set; }
        
    }
}
