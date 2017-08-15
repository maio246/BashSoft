namespace BashSoft
{
    using System;
    using System.IO;
    using System.Collections.Generic;
    using Contracts;
    using Exceptions;


    public class IOManager : IDirectoryManager
    {

        public void CreateDirectoryInCurrentFolder(string name)
        {
            string path = Directory.GetCurrentDirectory() + "\\" + name;
            try
            {
                Directory.CreateDirectory(path);
            }
            catch (ArgumentException)
            {
                throw new InvalidFileNameException();
            }
        }

        public void TraverseDirectory(int path)
        {
            OutputWriter.WriteEmptyLine();
            int initialIdentation = SessionData.currentPath.Split('\\').Length;
            var subFolders = new Queue<string>();
            subFolders.Enqueue(SessionData.currentPath);

            while (subFolders.Count != 0)
            {
                var currentPath = subFolders.Dequeue();
                int identation = currentPath.Split('\\').Length - initialIdentation;

                OutputWriter.WriteMessageOnNewLine(string.Format("{0}{1}",
                    new string('-', identation),
                    currentPath));

                if (path - identation < 0)
                {
                    break;
                }
                try
                {
                    foreach (string directoryPath in Directory.GetFiles(currentPath))
                    {

                        int indexOfLastSlash = directoryPath.LastIndexOf("\\");
                        string fileName = directoryPath.Substring(indexOfLastSlash);
                        OutputWriter.WriteMessageOnNewLine(
                            new string('-', indexOfLastSlash) + fileName);
                    }

                    foreach (string directoryPath in Directory.GetDirectories(currentPath))
                    {
                        subFolders.Enqueue(directoryPath);
                    }
                }
                catch (UnauthorizedAccessException)
                {
                    throw new Exception(ExceptionMessages.UnauthorizedAccessExceptionMessage);
                }
            }

        }

        public void ChangeCurrentDirectoryRelative(string relativePath)
        {
            if (relativePath == "..")
            {
                try
                {
                    string currentPath = SessionData.currentPath;
                    int indexOfLastSlash = currentPath.LastIndexOf("\\");
                    string newPath = currentPath.Substring(0, indexOfLastSlash);
                    SessionData.currentPath = newPath;
                }
                catch (ArgumentException)
                {
                    throw new InvalidPathException();
                }
            }
            else
            {
                string currentPath = SessionData.currentPath;
                currentPath += "\\" + relativePath;
                ChangeCurrentDirectoryAbsolute(currentPath);
            }
        }

        public void ChangeCurrentDirectoryAbsolute(string absolutePath)
        {
            if (!Directory.Exists(absolutePath))
            {
                throw new InvalidPathException();
            }
            SessionData.currentPath = absolutePath;
        }
    }
}
