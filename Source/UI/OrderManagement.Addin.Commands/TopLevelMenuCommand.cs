namespace OrderManagement.Addin.Commands {
    using System;
    using System.ComponentModel.Composition;
    using EnvDTE;
    using Microsoft.VisualStudio.Shell;

    [Export(typeof(CommandBase))]
    public class TopLevelMenuCommand : CommandBase
    {
        private readonly DTE dte;

        [ImportingConstructor]
        public TopLevelMenuCommand(SVsServiceProvider sp) : base(0x100)
        {

            dte = sp.GetService(typeof(DTE)) as DTE;
            Command.BeforeQueryStatus += Command_BeforeQueryStatus;
        }

        private void Command_BeforeQueryStatus(object sender, EventArgs e)
        {
#if DEBUG
            Command.Visible = true;

#else
            Command.Visible = dte.Solution.IsOpen;
                              //&& !string.IsNullOrEmpty(dte.Solution.FileName)
                              //&& string.Equals(Path.GetFileNameWithoutExtension(dte.Solution.FileName), "SolutionName", StringComparison.InvariantCultureIgnoreCase);

#endif
        }

        protected override void Execute(object sender, EventArgs e) { }
    }
}