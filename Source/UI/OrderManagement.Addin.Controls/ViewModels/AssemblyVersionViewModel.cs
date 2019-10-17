namespace OrderManagement.Addin.Controls.ViewModels {
    using System.Collections.ObjectModel;
    using System.ComponentModel.Composition;
    using System.Windows.Input;
    using Analyzers;
    using Data;
    using Models;
    using Models.Analyzers;
    using Models.Configuration;
    using PropertyChanged;

    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [AddINotifyPropertyChangedInterface]
    public class AssemblyVersionViewModel {
        private readonly IStoreRepository storeRepository;
        private readonly IProjectService projectService;
        private readonly SolutionConfiguration solutionConfiguration;
        public ICommand ViewLoadedCommand { get; set; }
        public ObservableCollection<Store> Stores { get; set; }
        public ObservableCollection<Project> Projects { get; set; }

        [ImportingConstructor]
        public AssemblyVersionViewModel(IStoreRepository storeRepository,
            IProjectService projectService,
            SolutionConfiguration solutionConfiguration) {
            this.storeRepository = storeRepository;
            this.projectService = projectService;
            this.solutionConfiguration = solutionConfiguration;
            ViewLoadedCommand = new DelegateCommand<object>(ViewLoaded);

        }

        private async void ViewLoaded(object obj) {
            this.Stores = new ObservableCollection<Store>(await storeRepository.GetStores());
            this.Projects = new ObservableCollection<Project>(await projectService.GetProjects());
        }

    }
}