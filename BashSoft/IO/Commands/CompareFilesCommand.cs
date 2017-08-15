namespace BashSoft.IO.Commands
{
    using Contracts;

    public class CompareFilesCommand : Command
    {
        public CompareFilesCommand(string input, string[] data, IContentComparer judge, IDatabase repository, IDirectoryManager inputOutputManager)
            : base(input, data, judge, repository, inputOutputManager)
        {
        }

        public override void Execute()
        {
            if (Data.Length == 3)
            {
                var firstPath = Data[1];
                var secondPath = Data[2];

                this.Judge.CompareContent(firstPath, secondPath);
            }

        }
    }
}
