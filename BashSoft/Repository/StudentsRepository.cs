namespace BashSoft
{
    using System;
    using System.Linq;
    using Models;
    using System.IO;
    using System.Collections.Generic;
    using System.Text.RegularExpressions;
    using Contracts;
    using DataStructures;

    public class StudentsRepository : IDatabase
    {

        private Dictionary<string, ICourse> courses;
        private Dictionary<string, IStudent> students;

        private bool IsDataInitialised;

        private IDataFilter filter;
        private IDataSorter sorter;

        public StudentsRepository(IDataSorter sorter, IDataFilter filter)
        {
            this.filter = filter;
            this.sorter = sorter;
        }

        public void LoadData(string fileName)
        {
            if (this.IsDataInitialised)
            {
                throw new Exception(ExceptionMessages.DataAlreadyInitialisedException);
            }
            this.students = new Dictionary<string, IStudent>();
            this.courses = new Dictionary<string, ICourse>();

            OutputWriter.WriteMessageOnNewLine("Reading data...");
            ReadData(fileName);
        }

        public void UnloadData()
        {
            if (!this.IsDataInitialised)
            {
                throw new ArgumentException(ExceptionMessages.DataNotinitializedExceptionMessage);
            }

            this.courses = null;
            this.students = null;
            this.IsDataInitialised = false;
        }

        private void ReadData(string fileName)
        {
            var path = SessionData.currentPath + "\\" + fileName;

            if (File.Exists(path))
            {
                var pattern = @"([A-Z][a-zA-Z#\++]*_[A-Z][a-z]{2}_\d{4})\s+([A-Za-z]+\d{2}_\d{2,4})\s([\s0-9]+)";
                Regex rgx = new Regex(pattern);

                var allInputLines = File.ReadAllLines(path);

                for (int line = 0; line < allInputLines.Length; line++)
                {
                    if (!string.IsNullOrEmpty(allInputLines[line]) && rgx.IsMatch(allInputLines[line]))
                    {
                        Match currentMatch = rgx.Match(allInputLines[line]);
                        var scoresStr = currentMatch.Groups[3].Value;

                        try
                        {
                            var scores = scoresStr.Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries)
                                .Select(int.Parse).ToArray();

                            var currCourse = currentMatch.Groups[1].Value;
                            var currStudent = currentMatch.Groups[2].Value;

                            if (scores.Any(x => x > 100 || x < 0))
                            {
                                OutputWriter.DisplayException(ExceptionMessages.InvalidScore);
                            }
                            if (scores.Length > SoftUniCourse.NumberOfTasksOnExam)
                            {
                                OutputWriter.DisplayException(ExceptionMessages.InvalidNumberOrScores);
                            }
                            if (!this.students.ContainsKey(currStudent))
                            {
                                this.students.Add(currStudent, new SoftUniStudent(currStudent));
                            }
                            if (!this.courses.ContainsKey(currStudent))
                            {
                                this.courses.Add(currStudent, new SoftUniCourse(currStudent));
                            }

                            ICourse course = this.courses[currCourse];
                            IStudent student = this.students[currStudent];

                            student.EnrollInCourse(course);
                            student.SetMarkOnCourse(currCourse, scores);

                            course.EnrollStudent(student);
                        }
                        catch (FormatException fex)
                        {
                            OutputWriter.DisplayException(fex.Message + $"at line : {line}");
                        }
                    }
                }
            }
            else
            {
                throw new Exception(ExceptionMessages.InvalidPath);
            }
            IsDataInitialised = true;
            OutputWriter.WriteMessageOnNewLine("Data read!");
        }

        private bool IsQueryForCoursePossible(string courseName)
        {
            if (this.courses.ContainsKey(courseName))
            {
                return true;
            }
            else OutputWriter.DisplayException(ExceptionMessages.DataAlreadyInitialisedException);
            return false;
        }

        private bool IsQueryForStudentPossible(string courseName, string studentName)
        {
            if (this.IsQueryForCoursePossible(courseName) &&
                this.courses[courseName].StudentsByName.ContainsKey(studentName))
            {
                return true;
            }
            else
            {
                OutputWriter.DisplayException(ExceptionMessages.InexistingCourseInDataBase);
                return false;
            }

        }

        public void GetStudentScoresFromCourse(string course, string studentName)
        {
            if (IsQueryForStudentPossible(course, studentName))
            {
                OutputWriter.PrintStudent(
                    new KeyValuePair<string, double>(studentName, 
                    this.courses[course]
                    .StudentsByName[studentName]
                    .MarksByCourseName[course]));
            }
        }

        public void GetAllStudentsFromCourse(string course)
        {
            if (IsQueryForCoursePossible(course))
            {
                OutputWriter.WriteMessageOnNewLine($"{course}");
                foreach (var studentMarksEntry in this.courses[course].StudentsByName)
                {
                    this.GetStudentScoresFromCourse(course, studentMarksEntry.Key);
                }
            }
        }

        public ISimpleOrderedBag<ICourse> GetAllCoursesSorted(IComparer<ICourse> cmp)
        {
            SimpleSortedList<ICourse> sortedStudents = new SimpleSortedList<ICourse>(cmp);
            sortedStudents.AddAll(this.courses.Values);
            return sortedStudents;
        }

        public ISimpleOrderedBag<IStudent> GetAllStudentsSorted(IComparer<IStudent> cmp)
        {
            SimpleSortedList<IStudent> sortedStudents = new SimpleSortedList<IStudent>(cmp);
            sortedStudents.AddAll(this.students.Values);
            return sortedStudents;
        }

        public void FilterAndTake(string courseName, string givenFilter, int? studentsToTake = null)
        {
            if (this.IsQueryForCoursePossible(courseName))
            {
                if (studentsToTake == null)
                {
                    studentsToTake = this.courses[courseName].StudentsByName.Count;
                }

                Dictionary<string, double> marks = this.courses[courseName].StudentsByName
                    .ToDictionary(s => s.Key, s => s.Value.MarksByCourseName[courseName]);

                this.filter.FilterAndTake(marks, givenFilter, studentsToTake.Value);
            }
        }

        public void OrderAndTake(string courseName, string comparison, int? studentsToTake = null)
        {
            if (IsQueryForCoursePossible(courseName))
            {
                if (studentsToTake == null)
                {
                    studentsToTake = this.courses[courseName].StudentsByName.Count;
                }

                Dictionary<string, double> marks = 
                    this.courses[courseName].StudentsByName
                    .ToDictionary(s => s.Key, s => s.Value.MarksByCourseName[courseName]);

                this.sorter.OrderAndTake(marks , comparison, studentsToTake.Value);
            }
        }

    }
}
