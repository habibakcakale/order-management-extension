using System;
using System.ComponentModel.Composition;
using System.Globalization;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;

namespace OrderManagement.Addin.Commands {
    using Controls.Views;
    using Microsoft.VisualStudio.ComponentModelHost;

    [Export(typeof(CommandBase))]
    public sealed class ChangeAssemblyVersion : CommandBase {
        private readonly IComponentModel componentModel;

        /// <summary>
        /// Command ID.
        /// </summary>
        public const int CommandId = 0x202;


        /// <summary>
        /// Initializes a new instance of the <see cref="ChangeAssemblyVersion"/> class.
        /// Adds our command handlers for menu (commands must exist in the command table file)
        /// </summary>
        /// <param name="provider">Owner package, not null.</param>
        [ImportingConstructor]
        public ChangeAssemblyVersion([Import(typeof(SVsServiceProvider))] IServiceProvider provider)
            : this((IComponentModel) provider.GetService(typeof(SComponentModel))) { }

        public ChangeAssemblyVersion(IComponentModel componentModel) : base(CommandId) {
            this.componentModel = componentModel;
        }

        /// <summary>
        /// This function is the callback used to execute the command when the menu item is clicked.
        /// See the constructor to see how the menu item is associated with this function using
        /// OleMenuCommandService service and MenuCommand class.
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="e">Event args.</param>
        protected override void Execute(object sender, EventArgs e) {

            var view = this.componentModel.GetService<AssemblyVersionView>();
            view.ShowModal();
        }
    }
}