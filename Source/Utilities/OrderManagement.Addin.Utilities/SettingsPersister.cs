namespace OrderManagement.Addin.Utilities {
    using System.ComponentModel.Composition;
    using System.IO;
    using System.Linq;
    using Models.Configuration;

    [Export]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class SettingsPersister {
        private AddinConfiguration settings;

        public AddinConfiguration Settings => settings ?? (settings = ReadConfiguration());

        public AddinConfiguration ReadConfiguration() {
            if (File.Exists(DirectoryHelper.ConfigurationXmlPath)) {
                return Extensions.ReadXml<AddinConfiguration>(DirectoryHelper.ConfigurationXmlPath);
            }
            else {
                var emptySettings = new AddinConfiguration ();
                Extensions.WriteXml(DirectoryHelper.ConfigurationXmlPath, emptySettings);

                return emptySettings;
            }
        }

        public void SaveProject(SolutionConfiguration toSave) {
            var project = this.Settings.Projects.FirstOrDefault(item => item.SolutionName == toSave.SolutionName);
            if (project == null) {
                this.Settings.Projects.Add(toSave);
            }
            else {
                this.Settings.Projects.Remove(project);
                this.Settings.Projects.Add(toSave);
            }

            Extensions.WriteXml(DirectoryHelper.ConfigurationXmlPath, this.Settings);
        }
    }
}