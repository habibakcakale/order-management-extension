namespace OrderManagement.Addin.Analyzers {
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.VisualStudio.LanguageServices;
    using Models.Analyzers;

    [Export(typeof(IProjectService))]
    public class RoslynProjectService : IProjectService {
        private readonly VisualStudioWorkspace workspace;

        [ImportingConstructor]
        public RoslynProjectService(VisualStudioWorkspace workspace) {
            this.workspace = workspace;
        }

        public Task<IEnumerable<Project>> GetProjects() {
            var projects = workspace.CurrentSolution.Projects.Select(item => new Project() {
                FilePath = item.FilePath,
                Name = item.Name
            });
            return Task.FromResult(projects);
        }
    }
}