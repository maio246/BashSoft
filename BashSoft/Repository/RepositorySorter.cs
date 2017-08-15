namespace BashSoft
{
    using System.Collections.Generic;
    using System.Linq;
    using Contracts;

    public class RepositorySorter : IDataSorter
    {

        public void OrderAndTake(Dictionary<string, double> studentsMarks, string comparison, int studentsToTake)
        {
            comparison = comparison.ToLower();
            if (comparison == "ascending")
            {
                this.PrintStudents(studentsMarks.OrderBy(s => s.Value)
                    .Take(studentsToTake)
                    .ToDictionary(x => x.Key, x => x.Value));
            }
            else if (comparison == "descending")
            {
                this.PrintStudents(studentsMarks
                    .OrderByDescending(s => s.Value)
                    .Take(studentsToTake)
                    .ToDictionary(x => x.Key, x => x.Value));
            }
            else
            {
                OutputWriter.DisplayException(ExceptionMessages.InvalidComparisonQuery);
            }
        }

        private void PrintStudents(Dictionary<string, double> studentsWithMarks)
        {
            var counterForPrinted = 0;

            foreach (var student in studentsWithMarks)
            {
                OutputWriter.PrintStudent(new KeyValuePair<string, double>(student.Key, student.Value));
                counterForPrinted++;
            }

        }
    }
}
