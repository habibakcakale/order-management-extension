namespace OrderManagement.Addin.Controls.Views {
    using System.ComponentModel.Composition;
    using ViewModels;

    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public partial class SettingsView {
        [ImportingConstructor]
        public SettingsView(SettingsViewModel viewModel) {
            InitializeComponent();
            this.DataContext = viewModel;
        }
#if DEBUG
        private SettingsView() : base(null) { }
    }
#endif

}