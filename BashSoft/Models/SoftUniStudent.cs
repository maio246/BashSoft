namespace BashSoft.Models
{
    using Contracts;
    using Exceptions;
    using System.Collections.Generic;
    using System.Linq;

    public class SoftUniStudent : IStudent
    {
        private string userName;
        private Dictionary<string, ICourse> enrolledCourses;
        private Dictionary<string, double> marksByCourseName;

        public SoftUniStudent(string userName)
        {
            this.UserName = userName;
            this.enrolledCourses = new Dictionary<string, ICourse>();
            this.marksByCourseName = new Dictionary<string, double>();
        }

        public IReadOnlyDictionary<string, ICourse> EnrolledCourses
        {
            get { return enrolledCourses; }
        }

        public IReadOnlyDictionary<string, double> MarksByCourseName
        {
            get { return marksByCourseName; }
        }

        public string UserName
        {
            get { return this.userName; }
            private set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new InvalidStringException();
                }
                this.userName = value;
            }
        }

        public void EnrollInCourse(ICourse course)
        {
            if (this.enrolledCourses.ContainsKey(course.Name))
            {
                throw new DuplicateEntryInStructureException(this.userName, course.Name);
            }

            this.enrolledCourses.Add(course.Name, course);
        }

        public void SetMarkOnCourse(string courseName, params int[] scores)
        {
            if (!this.enrolledCourses.ContainsKey(courseName))
            {
                throw new StudentNotEnrolledInCourse(this.userName, courseName);
            }
            if (scores.Length > SoftUniCourse.NumberOfTasksOnExam)
            {
                throw new InvalidNumberOfScores();
            }
            this.marksByCourseName.Add(courseName, CalculateMark(scores));
        }

        private double CalculateMark(int[] scores)
        {
            double percentageOfSolvedExam = scores.Sum() /
                                            (double)(SoftUniCourse.NumberOfTasksOnExam * SoftUniCourse.MaxScoresOnExamTask);
            double mark = percentageOfSolvedExam * 4 + 2;

            return mark;
        }

        public int CompareTo(IStudent other) => this.UserName.CompareTo(other.UserName);

        public override string ToString() => this.UserName;
    }
}
