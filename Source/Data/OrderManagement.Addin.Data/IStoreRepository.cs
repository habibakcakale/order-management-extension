namespace OrderManagement.Addin.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Models;

    public interface IStoreRepository {
      Task<IEnumerable<Store>> GetStores();
    }
}
