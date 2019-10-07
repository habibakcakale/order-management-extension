namespace OrderManagement.Addin.Models.Configuration {
    using System.ComponentModel;

    public class SolutionConfiguration {
        public string SolutionName { get; set; }
        [Description("Connection string of System Integrator database.")]
        public string MasterConnectionString { get; set; }
    }
}