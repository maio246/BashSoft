namespace BashSoft
{
    using System.Collections.Generic;
    using System;
    using Contracts;


    public class RepositoryFilter :IDataFilter
    {

        public void FilterAndTake(Dictionary<string, double> studentsWithMarks, string wantedFilter, int studentToTake)
        {
            if (wantedFilter == "excellent")
            {
                FilterAndTake(studentsWithMarks, x => x >= 5, studentToTake);
            }
            else if (wantedFilter == "average")
            {
                FilterAndTake(studentsWithMarks, x => x < 5 && x >= 3.5, studentToTake);
            }
            else if (wantedFilter == "poor")
            {
                FilterAndTake(studentsWithMarks, x => x < 3.5, studentToTake);
            }
            else
            {
                OutputWriter.DisplayException(ExceptionMessages.ImvalidStudentFilter);
            }
        }

        public static void PrintStudent(KeyValuePair<string, double> student)
        {
            OutputWriter.WriteMessageOnNewLine($"{student.Key} - {student.Value}");
        }

        private void FilterAndTake(Dictionary<string, double> studentsWithMarks, Predicate<double> givenFilter, int studentsToTake)
        {
            var counterForPrinted = 0;

            foreach (var student in studentsWithMarks)
            {
                if (counterForPrinted == studentsToTake)
                {
                    break;
                }

              if (givenFilter(student.Value))
                {
                    OutputWriter.PrintStudent(new KeyValuePair<string, double>(student.Key, studentsToTake));
                    counterForPrinted++;
                }
            }

        }
    }
}
