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
        public static readonly Guid CommandSet = new Guid("b533aa3e-13ff-44f0-b55d-a2a2394c2035");

        public MenuCommand Command { get; private set; }

        protected CommandBase(int id)
        {
            Command = new MenuCommand(Execute, new CommandID(CommandSet, id));
        }

        protected abstract void Execute(object sender, EventArgs e);
    }
}
