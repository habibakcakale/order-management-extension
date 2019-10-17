namespace OrderManagement.Addin.Controls.Views.Pickers
{
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using System.Windows.Input;
    using Models.Analyzers;
    using ViewModels.Pickers;

    /// <summary>
    /// Interaction logic for ProjectSelectorView.xaml
    /// </summary>
    [Export]
    public partial class ProjectSelectorView
    {
        private readonly ProjectSelectorViewModel viewModel;
        [ImportingConstructor]
        public ProjectSelectorView(ProjectSelectorViewModel viewModel) {
            this.DataContext = this.viewModel = viewModel;
            InitializeComponent();
        }


        public Project SelectedProject
        {
            get => viewModel.SelectedProject;
            set => viewModel.SelectedProject = value;
        }

        public IList<Project> SelectedProjects => viewModel.SelectedProjects;

        private void Control_OnMouseDoubleClick(object sender, MouseButtonEventArgs e) {
            this.DialogResult = true;
        }
    }
}
