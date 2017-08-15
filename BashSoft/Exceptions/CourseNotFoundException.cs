namespace BashSoft.Exceptions
{
    using System;

    public class CourseNotFoundException : Exception
    {
        private const string CourseNotFound = "This course was not found.";

        public CourseNotFoundException() : base(CourseNotFound)
        { }

        public CourseNotFoundException(string message) : base(message)
        { }

    }
}
