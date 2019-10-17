namespace OrderManagement.Addin.Controls.ViewModels.Pickers {
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel.Composition;
    using System.Windows.Controls;
    using System.Windows.Input;
    using Analyzers;
    using Microsoft.Internal.VisualStudio.PlatformUI;
    using Models.Analyzers;
    using PropertyChanged;


    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [AddINotifyPropertyChangedInterface]
    public class ProjectSelectorViewModel {
        private readonly IProjectService projectService;

        public ObservableCollection<Project> Projects { get; set; }
        public Project SelectedProject { get; set; }
        public SelectionMode SelectionMode { get; set; }
        public IList<Project> SelectedProjects { get; set; }
        public ICommand ViewLoadedCommand { get; set; }

        [ImportingConstructor]
        public ProjectSelectorViewModel(IProjectService projectService) {
            this.projectService = projectService;
            this.ViewLoadedCommand = new DelegateCommand<object>(ViewLoaded);
        }

        private async void ViewLoaded(object obj) {
            this.Projects = new ObservableCollection<Project>(await projectService.GetProjects());
        }
    }
}