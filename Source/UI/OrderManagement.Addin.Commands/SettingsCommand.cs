namespace OrderManagement.Addin.Commands {
    using System;
    using System.ComponentModel.Composition;
    using Controls.Views;
    using Microsoft.VisualStudio.ComponentModelHost;
    using Microsoft.VisualStudio.Shell;

    [Export(typeof(CommandBase))]
    public sealed class SettingsCommand : CommandBase {
        private readonly IComponentModel componentModel;
        private const int CommandId = 0x201;

        [ImportingConstructor]
        public SettingsCommand([Import(typeof(SVsServiceProvider))] IServiceProvider provider)
            : this((IComponentModel) provider.GetService(typeof(SComponentModel))) { }

        public SettingsCommand(IComponentModel componentModel) : base(CommandId) {
            this.componentModel = componentModel;
        }

        protected override void Execute(object sender, EventArgs e) {
            var view = this.componentModel.GetService<SettingsView>();
            view.ShowModal();

        }
    }
}