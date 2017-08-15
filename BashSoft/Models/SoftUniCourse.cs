namespace BashSoft.Models
{
    using System.Collections.Generic;
    using Contracts;
    using Exceptions;

    public class SoftUniCourse : ICourse
    {
        private string name;
        private Dictionary<string, IStudent> studentsByName;

        public const int NumberOfTasksOnExam = 5;
        public const int MaxScoresOnExamTask = 100;

        public SoftUniCourse(string name)
        {
            this.name = name;
            this.studentsByName = new Dictionary<string, IStudent>();
        }

        public string Name
        {
            get { return this.name; }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new InvalidStringException();
                }
            }
        }

        public IReadOnlyDictionary<string, IStudent> StudentsByName { get; }

        public void EnrollStudent(IStudent student)
        {
            if (this.studentsByName.ContainsKey(student.UserName))
            {
                throw new StudentAlreadyEnrolledInGivenCourse(student.UserName, this.name);
            }
            this.studentsByName.Add(student.UserName, student);
        }

        public int CompareTo(ICourse other) => this.Name.CompareTo(other.Name);

        public override string ToString() => this.Name;
    }
}