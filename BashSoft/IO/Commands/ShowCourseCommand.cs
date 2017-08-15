namespace BashSoft.IO.Commands
{
    using Contracts;
    using Exceptions;

    public class ShowCourseCommand : Command
    {
        public ShowCourseCommand(string input, string[] data, IContentComparer judge, IDatabase repository, IDirectoryManager inputOutputManager)
            : base(input, data, judge, repository, inputOutputManager)
        {
        }

        public override void Execute()
        {
            if (Data.Length == 2)
            {
                string courseName = Data[1];
                this.Repository.GetAllStudentsFromCourse(courseName);
            }
            else if (Data.Length == 3)
            {
                string courseName = Data[1];
                string studentName = Data[2];
                this.Repository.GetStudentScoresFromCourse(courseName, studentName);
            }
            else
            {
                throw new InvalidCommandException(Input);
            }

        }
    }
}
