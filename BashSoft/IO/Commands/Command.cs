﻿namespace BashSoft.IO.Commands
{
    using System;
    using Contracts;
    using Exceptions;

    public abstract class Command : IExecutable
    {
        private string input;
        private string[] data;

        private IContentComparer judge;
        private IDatabase repository;
        private IDirectoryManager inputOutputManager;

        protected Command(string input, string[] data,
            IContentComparer judge, IDatabase repository, IDirectoryManager inputOutputManager)
        {
            this.Input = input;
            this.Data = data;
            this.judge = judge;
            this.repository = repository;
            this.inputOutputManager = inputOutputManager;
        }

        public abstract void Execute();

        public string Input 
        {
            get { return this.input; }
            protected set 
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new InvalidStringException();
                }
                this.input = value;
            }
        }

        public string[] Data
        {
            get { return this.data; }
            protected set
            {
                if (value == null || value.Length == 0)
                {
                    throw new NullReferenceException();
                }
                this.data = value;
            }
        }

        protected IContentComparer Judge
        {
            get { return this.judge; }
        }

        protected IDatabase Repository
        {
            get { return this.repository; }
        }

        protected IDirectoryManager InputOutputManager
        {
            get { return this.inputOutputManager; }
        }
    }
}
