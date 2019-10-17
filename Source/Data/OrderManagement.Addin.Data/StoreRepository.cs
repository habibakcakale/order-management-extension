namespace OrderManagement.Addin.Data {
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using System.Data.SqlClient;
    using System.Threading.Tasks;
    using Dapper;
    using Models;
    using Models.Configuration;

    [Export(typeof(IStoreRepository))]
    public class StoreRepository : IStoreRepository {
        private readonly SolutionConfiguration configuration;

        [ImportingConstructor]
        public StoreRepository(SolutionConfiguration configuration) {
            this.configuration = configuration;
        }

        public async Task<IEnumerable<Store>> GetStores() {
            var connection = new SqlConnection(configuration.MasterConnectionString);
            var stores = await connection.QueryAsync<Store>("SELECT ID, CompanyName, ConnectionString, Guid FROM Stores");
            return stores;
        }
    }
}