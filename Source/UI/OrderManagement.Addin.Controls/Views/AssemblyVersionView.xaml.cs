namespace OrderManagement.Addin.Controls.Views
{
    using System.ComponentModel.Composition;

    /// <summary>
    /// Interaction logic for AssemblyVersionView.xaml
    /// </summary>
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public partial class AssemblyVersionView 
    {
        [ImportingConstructor]
        public AssemblyVersionView()
        {
            InitializeComponent();
        }
    }
}
