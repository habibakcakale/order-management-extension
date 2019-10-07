namespace OrderManagement.Addin.Models.Configuration {
    using System.Collections.Generic;

    public class AddinConfiguration {
        public List<SolutionConfiguration> Projects { get; set; }

        public AddinConfiguration() {
            this.Projects = new List<SolutionConfiguration>();
        }
    }
}