namespace BashSoft.IO.Commands
{
    using Contracts;
   
    public class TraverseFoldersCommand : Command
    {
        public TraverseFoldersCommand(string input, string[] data, IContentComparer judge, IDatabase repository, IDirectoryManager inputOutputManager)
            : base(input, data, judge, repository, inputOutputManager)
        {
        }

        public override void Execute()
        {
            if (this.Data.Length == 1)
            {
                this.InputOutputManager.TraverseDirectory(0);
            }
            else if (Data.Length == 2)
            {
                int depth;
                bool hasParsed = int.TryParse(Data[1], out depth);

                if (hasParsed)
                {
                    InputOutputManager.TraverseDirectory(depth);
                }
                else
                {
                    OutputWriter.DisplayException(ExceptionMessages.UnableToParseNumber);
                }
            }

        }
    }
}
