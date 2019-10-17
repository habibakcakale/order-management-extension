namespace OrderManagement.Addin.Controls.Views.Pickers
{
    using System.Collections.Generic;
    using System.Windows.Input;
    using ViewModels.Pickers;

/// <summary>
    /// Interaction logic for ProjectSelectorView.xaml
    /// </summary>
    public partial class ProjectSelectorView
    {
        private readonly ProjectSelectorViewModel viewModel;

        public ProjectSelectorView(ProjectSelectorViewModel viewModel) {
            this.viewModel = viewModel;
            InitializeComponent();
        }


        public object SelectedProject
        {
            get => ((ProjectSelectorViewModel)this.DataContext).SelectedProject;
            set => ((ProjectSelectorViewModel)this.DataContext).SelectedProject = value;
        }

        public IList<object> SelectedProjects => ((ProjectSelectorViewModel)this.DataContext).SelectedProjects;

        private void Control_OnMouseDoubleClick(object sender, MouseButtonEventArgs e) {
            this.DialogResult = true;
        }
    }
}
