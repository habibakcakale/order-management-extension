namespace OrderManagement.Addin.Analyzers {
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Models.Analyzers;

    public interface IProjectService {
        Task<IEnumerable<Project>> GetProjects();
    }
}