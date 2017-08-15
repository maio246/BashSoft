namespace BashSoft.Exceptions
{
    using System;

    public class InvalidStringException : Exception
    {
        private const string InvalidStringExceptions = "The value of the variable CANNOT be null or empty!";

        public InvalidStringException() : base (InvalidStringExceptions)
        {
        }
        public InvalidStringException(string message) : base (message)
        { }
    }
}
