using System.Collections.Generic;

namespace BashSoft.Contracts
{
    public interface IRequester
    {
        void GetStudentScoresFromCourse(string course, string studentName);
        void GetAllStudentsFromCourse(string course);

        ISimpleOrderedBag<ICourse> GetAllCoursesSorted(IComparer<ICourse> cmp);
        ISimpleOrderedBag<IStudent> GetAllStudentsSorted(IComparer<IStudent> cmp);

    }
}
