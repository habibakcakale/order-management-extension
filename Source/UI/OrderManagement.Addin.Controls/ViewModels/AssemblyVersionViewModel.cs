namespace OrderManagement.Addin.Controls.ViewModels
{
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using System.Windows.Input;
    using Data;
    using Models;
    using Models.Configuration;
    using PropertyChanged;

    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [AddINotifyPropertyChangedInterface]
    public class AssemblyVersionViewModel
    {
        private readonly IStoreRepository storeRepository;
        private readonly SolutionConfiguration solutionConfiguration;
        public ICommand ViewLoadedCommand { get; set; }
        public IEnumerable<Store> Stores { get; set; }

        [ImportingConstructor]
        public AssemblyVersionViewModel(IStoreRepository storeRepository, SolutionConfiguration solutionConfiguration) {
            this.storeRepository = storeRepository;
            this.solutionConfiguration = solutionConfiguration;
            ViewLoadedCommand = new DelegateCommand<object>(ViewLoaded);

        }

        private async void ViewLoaded(object obj) {
            this.Stores =  await storeRepository.GetStores();
        }

    }
}
