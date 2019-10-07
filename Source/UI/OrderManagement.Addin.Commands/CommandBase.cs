namespace OrderManagement.Addin.Commands
{
    using System;
    using System.ComponentModel.Design;
    using Microsoft.VisualStudio.Shell;

    public abstract class CommandBase
    {
        /// <summary>
        /// Command set of Menu
        /// </summary>
        public static readonly Guid CommandSet = new Guid("55a0754a-c67f-46e4-8cff-ab81f9dc0971");

        public OleMenuCommand Command { get; private set; }

        protected CommandBase(int id)
        {
            Command = new OleMenuCommand(Execute, new CommandID(CommandSet, id));
        }

        protected abstract void Execute(object sender, EventArgs e);
    }
}
