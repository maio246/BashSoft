namespace BashSoft.IO.Commands
{
    using Contracts;

    public class ReadDatabaseCommand : Command
    {
        public ReadDatabaseCommand(string input, string[] data, IContentComparer judge, IDatabase repository, IDirectoryManager inputOutputManager)
            : base(input, data, judge, repository, inputOutputManager)
        {
        }

        public override void Execute()
        {
            string fileName = Data[1];
            this.Repository.LoadData(fileName);

        }
    }
}
