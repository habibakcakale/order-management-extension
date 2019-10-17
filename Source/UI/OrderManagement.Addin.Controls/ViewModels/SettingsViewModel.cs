namespace OrderManagement.Addin.Controls.ViewModels {
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using System.Data.SqlClient;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using System.Xml.Linq;
    using System.Xml.XPath;
    using Models.Configuration;
    using PropertyChanged;
    using Utilities;
    using Views.Pickers;

    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [AddINotifyPropertyChangedInterface]
    public class SettingsViewModel {
        private readonly SettingsPersister settingPersister;
        private readonly ProjectSelectorView projectSelector;

        public SolutionConfiguration SolutionConfiguration { get; set; }

        public ICommand SaveCommand { get; set; }
        public ICommand GetConnectionStringCommand { get; set; }
        private static readonly string[] ConfigFiles = { "app.config", "web.config" };

        [ImportingConstructor]
        public SettingsViewModel(SettingsPersister settingPersister,
            SolutionConfiguration solutionConfiguration,
            ProjectSelectorView projectSelector
            ) {
            this.settingPersister = settingPersister;
            this.projectSelector = projectSelector;

            this.SolutionConfiguration = solutionConfiguration;
            this.SaveCommand = new DelegateCommand<SolutionConfiguration>(Save);
            this.GetConnectionStringCommand = new DelegateCommand<object>(GetConnectionString);
        }

        //TODO: Make it async Task
        private async void Save(SolutionConfiguration solutionConfiguration) {
            await Task.Run(() => { settingPersister.SaveProject(solutionConfiguration); });

        }


        private void GetConnectionString(object obj)
        {
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
            var element = document.Descendants().FirstOrDefault(item => {
                var keyAttr = item.Attribute("key");

                return string.Equals(item.Name.LocalName, "add", StringComparison.InvariantCultureIgnoreCase)
                       && keyAttr != null
                       && keyAttr.Value.EndsWith("MasterConnectionString", StringComparison.InvariantCultureIgnoreCase);
            });

            //TODO: Should get connection string from Window
            //var element = document.XPathSelectElement("//connectionStrings/add[@name='VeriBranchDataEntitiesBase']");
            var connectionStringAttribute = element?.Attribute("value");
            if (connectionStringAttribute != null && !string.IsNullOrWhiteSpace(connectionStringAttribute.Value))
            {
                var builder = new SqlConnectionStringBuilder(connectionStringAttribute.Value);
                SolutionConfiguration.InitialCatalog = builder.InitialCatalog;
                SolutionConfiguration.Password = builder.Password;
                SolutionConfiguration.UserId = builder.UserID;
                SolutionConfiguration.DataSource = builder.DataSource;
            }
        }
    }
}