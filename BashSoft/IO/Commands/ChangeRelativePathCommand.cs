namespace BashSoft.IO.Commands
{
    using Contracts;

    public class ChangeRelativePathCommand : Command
    {
        public ChangeRelativePathCommand(string input, string[] data, IContentComparer judge, IDatabase repository, IDirectoryManager inputOutputManager) 
            : base(input, data, judge, repository, inputOutputManager)
        {
        }

        public override void Execute()
        {
            var relPath = Data[1];
            this.InputOutputManager.ChangeCurrentDirectoryRelative(relPath);

        }
    }
}
