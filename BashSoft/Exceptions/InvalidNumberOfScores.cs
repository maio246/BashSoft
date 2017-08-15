namespace BashSoft.Exceptions
{
    using System;

    public class InvalidNumberOfScores :Exception
    {
        private const string InvalidScores = "This is not a valid number for scores.";

        public InvalidNumberOfScores() : base(InvalidScores)
        { }
    }
}
