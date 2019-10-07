namespace OrderManagement.Addin.Controls.Views
{
    using System.ComponentModel.Composition;
    using ViewModels;

    /// <summary>
    /// Interaction logic for SettingsWindow.xaml
    /// </summary>

    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public partial class SettingsView
    {
        [ImportingConstructor]
        public SettingsView(SettingsViewModel viewModel)
        {
            InitializeComponent();
            this.DataContext = viewModel;
        }
    }
}
