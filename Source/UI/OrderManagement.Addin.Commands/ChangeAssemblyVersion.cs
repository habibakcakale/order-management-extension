using System;
using System.ComponentModel.Composition;
using System.Globalization;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;

namespace OrderManagement.Addin.Commands
{
    [Export(typeof(CommandBase))]
    public sealed class ChangeAssemblyVersion : CommandBase
    {
        private readonly IServiceProvider provider;

        /// <summary>
        /// Command ID.
        /// </summary>
        public const int CommandId = 0x0100;


        /// <summary>
        /// Initializes a new instance of the <see cref="ChangeAssemblyVersion"/> class.
        /// Adds our command handlers for menu (commands must exist in the command table file)
        /// </summary>
        /// <param name="provider">Owner package, not null.</param>
        [ImportingConstructor]
        public ChangeAssemblyVersion(
            [Import(typeof(SVsServiceProvider))] IServiceProvider provider
            ) : base(CommandId)
        {
            this.provider = provider ?? throw new ArgumentNullException(nameof(provider));
        }

        /// <summary>
        /// This function is the callback used to execute the command when the menu item is clicked.
        /// See the constructor to see how the menu item is associated with this function using
        /// OleMenuCommandService service and MenuCommand class.
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="e">Event args.</param>
        protected override void Execute(object sender, EventArgs e)
        {
            ////var selector = provider.GetService(typeof(ProjectSelector)) as ProjectSelector;
            ////selector?.ShowModal();
            ThreadHelper.ThrowIfNotOnUIThread();
            string message = string.Format(CultureInfo.CurrentCulture, "Inside {0}.MenuItemCallback()",
                this.GetType().FullName);
            string title = "ChangeAssemblyVersion";

            // Show a message box to prove we were here
            VsShellUtilities.ShowMessageBox(
                this.provider,
                message,
                title,
                OLEMSGICON.OLEMSGICON_INFO,
                OLEMSGBUTTON.OLEMSGBUTTON_OK,
                OLEMSGDEFBUTTON.OLEMSGDEFBUTTON_FIRST);
        }
    }
}