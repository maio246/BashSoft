namespace BashSoft.Exceptions
{
    using System;

    public class InvalidCommandException : Exception
    {

        public InvalidCommandException(string message)
        {
            this.DisplayInvalidCommandMessage(message);
        }

        public void DisplayInvalidCommandMessage(string message)
        {
            OutputWriter.DisplayException($"The command '{message}' is invalid");
        }
    }
}
