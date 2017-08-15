namespace BashSoft
{ 
    using System;
    using Contracts;

    public class InputReader : IReader
    {
        private const string endCommand = "quit";
        private IInterpreter interpreter;

        public InputReader(IInterpreter interpreter)
        {
            this.interpreter = interpreter;
        }
        public void StartReadingCommands()
        {
            var input = Console.ReadLine();

            while (input != endCommand)
            {
                this.interpreter.InterpretCommand(input);

                OutputWriter.WriteMessage($"{SessionData.currentPath}> ");
                input = Console.ReadLine();
                input = input.Trim();
            }
        }

    }
}
