using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Addin.Commands
{
    public abstract class CommandBase
    {
        /// <summary>
        /// Command set of Menu
        /// </summary>
        public static readonly Guid CommandSet = new Guid("55a0754a-c67f-46e4-8cff-ab81f9dc0971");

        public MenuCommand Command { get; private set; }

        protected CommandBase(int id)
        {
            Command = new MenuCommand(Execute, new CommandID(CommandSet, id));
        }

        protected abstract void Execute(object sender, EventArgs e);
    }
}
