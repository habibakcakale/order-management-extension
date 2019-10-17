namespace OrderManagement.Addin.Controls.ViewModels {
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using System.IO;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using System.Xml.Linq;
    using System.Xml.XPath;
    using Models.Configuration;
    using PropertyChanged;
    using Utilities;

    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [AddINotifyPropertyChangedInterface]
    public class SettingsViewModel {
        private readonly SettingsPersister settingPersister;

        public SolutionConfiguration SolutionConfiguration { get; set; }

        public ICommand SaveCommand { get; set; }
        private static readonly string[] ConfigFiles = { "app.config", "web.config" };

        [ImportingConstructor]
        public SettingsViewModel(SettingsPersister settingPersister, SolutionConfiguration solutionConfiguration) {
            this.settingPersister = settingPersister;

            this.SolutionConfiguration = solutionConfiguration;
            this.SaveCommand = new DelegateCommand<SolutionConfiguration>(Save);
        }

        //TODO: Make it async Task
        private async void Save(SolutionConfiguration solutionConfiguration) {
            await Task.Run(() => { settingPersister.SaveProject(solutionConfiguration); });

        }


        private void GetConnectionString(object obj)
        {
            var projectSelector = this.componentModel.GetService<ProjectSelector>();
            var result = projectSelector.ShowDialog();
            if (result != null && result.Value)
            {
                var project = projectSelector.SelectedProject;
                var projectDirectory = Path.GetDirectoryName(project.FilePath);
                foreach (var fileName in ConfigFiles)
                {
                    var configFile = Path.Combine(projectDirectory, fileName);
                    if (File.Exists(configFile))
                    {
                        ReadConfigurationFile(configFile);
                        break;
                    }
                }
            }
        }

        private void ReadConfigurationFile(string configFile)
        {
            var document = XDocument.Load(configFile);
            //TODO: Should get connection string from Window
            var element = document.XPathSelectElement("//connectionStrings/add[@name='VeriBranchDataEntitiesBase']");
            var connectionStringAttribute = element?.Attribute("connectionString");
            if (connectionStringAttribute != null && !string.IsNullOrWhiteSpace(connectionStringAttribute.Value))
            {
                var builder = new EntityConnectionStringBuilder(connectionStringAttribute.Value);
                var pairs = builder.ProviderConnectionString.Split(';').Select(item => {
                    var pair = item.Split('=');
                    return new KeyValuePair<string, string>(pair.First(), pair.Last());
                }).ToList();

                var db = pairs.FirstOrDefault(item => string.Equals(item.Key, "initial catalog"));
                var userId = pairs.FirstOrDefault(item => string.Equals(item.Key, "user id"));
                var password = pairs.FirstOrDefault(item => string.Equals(item.Key, "password"));
                var dataSource = pairs.FirstOrDefault(item => string.Equals(item.Key, "data source"));
                AddInProject.DatabaseName = db.Value;
                AddInProject.Password = password.Value;
                AddInProject.UserName = userId.Value;
                AddInProject.ServerName = dataSource.Value;
            }
        }
    }
}