namespace BashSoft.Exceptions
{
    public class StudentAlreadyEnrolledInGivenCourse : DuplicateEntryInStructureException
    {
        private const string DuplicateEntry = "The {0} already exists in {1}";

        public StudentAlreadyEnrolledInGivenCourse(string message) : base(message)
        {
        }

        public StudentAlreadyEnrolledInGivenCourse(string entry, string structure) : base (string.Format(DuplicateEntry, entry, structure))
        {
        }
    }
}
