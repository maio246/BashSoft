namespace BashSoft.Exceptions
{
    using System;

    public class StudentNotEnrolledInCourse : Exception
    {
        private const string DuplicateEntry = "The {0} does not exists in {1}";

        public StudentNotEnrolledInCourse(string message) : base(message)
            {
        }

        public StudentNotEnrolledInCourse(string entry, string structure) : base(string.Format(DuplicateEntry,
            entry, structure))
        {
        }

    }
}
