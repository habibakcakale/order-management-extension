using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace OrderManagement.Addin.Controls.Views.Pickers
{
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


        public Project SelectedProject
        {
            get { return ((ProjectSelectorViewModel)this.DataContext).SelectedProject; }
            set { ((ProjectSelectorViewModel)this.DataContext).SelectedProject = value; }
        }

        public IList<Project> SelectedProjects => ((ProjectSelectorViewModel)this.DataContext).SelectedProjects;

        private void Control_OnMouseDoubleClick(object sender, MouseButtonEventArgs e) {
            this.DialogResult = true;
        }
    }
}
