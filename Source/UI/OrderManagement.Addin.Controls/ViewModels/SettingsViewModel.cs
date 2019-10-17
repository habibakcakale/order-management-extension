namespace OrderManagement.Addin.Controls.ViewModels {
    using System.ComponentModel.Composition;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using Models.Configuration;
    using Utilities;

    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class SettingsViewModel : BaseViewModel {
        private readonly SettingsPersister settingPersister;
        private SolutionConfiguration solution;
        [Import]
        public SolutionConfiguration Solution {
            get => solution;
            set {
                solution = value;
                OnPropertyChanged();
            }
        }

        public ICommand SaveCommand { get; set; }

        [ImportingConstructor]
        public SettingsViewModel(SettingsPersister settingPersister) {
            this.settingPersister = settingPersister;
            this.SaveCommand = new DelegateCommand<SolutionConfiguration>(Save);
        }

        //TODO: Make it async Task
        private async void Save(SolutionConfiguration configuration) {
            await Task.Run(() => { settingPersister.SaveProject(configuration); });

        }
    }
}