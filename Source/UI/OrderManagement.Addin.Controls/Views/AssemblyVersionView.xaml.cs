namespace OrderManagement.Addin.Controls.Views {
    using System.ComponentModel.Composition;
    using ViewModels;

    /// <summary>
    /// Interaction logic for AssemblyVersionView.xaml
    /// </summary>
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public partial class AssemblyVersionView {
        [ImportingConstructor]
        public AssemblyVersionView(AssemblyVersionViewModel viewModel) {
            this.DataContext = viewModel;
            InitializeComponent();
        }
    }
}