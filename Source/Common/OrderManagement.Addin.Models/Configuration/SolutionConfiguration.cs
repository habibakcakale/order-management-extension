namespace OrderManagement.Addin.Models.Configuration
{
    using System.ComponentModel;
    using System.Data.SqlClient;
    using PropertyChanged;

    [AddINotifyPropertyChangedInterface]
    public class SolutionConfiguration
    {
        private const string Database = "Database";

        public string SolutionName { get; set; }

        [Description("Connection string of System Integrator database.")]
        [Browsable(false)]
        public string MasterConnectionString
        {
            get {
                var builder = new SqlConnectionStringBuilder {
                    DataSource = this.DataSource,
                    InitialCatalog = this.InitialCatalog,
                    UserID = this.UserId,
                    Password = this.Password,
                    MultipleActiveResultSets = true
                };
                //try integrated security.
                if (string.IsNullOrEmpty(this.UserId))
                    builder.IntegratedSecurity = true;
                return builder.ToString();
            }
        }

        [Category(Database)]
        public string InitialCatalog { get; set; }
        [Category(Database)]
        public string UserId { get; set; }
        [Category(Database)]
        public string Password { get; set; }
        [Category(Database)]
        public string DataSource { get; set; }
    }
}