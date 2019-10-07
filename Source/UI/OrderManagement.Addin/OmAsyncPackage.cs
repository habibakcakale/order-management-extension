namespace OrderManagement.Addin {
    using System;
    using System.ComponentModel.Composition;
    using System.ComponentModel.Design;
    using System.Diagnostics.CodeAnalysis;
    using System.IO;
    using System.Linq;
    using System.Runtime.InteropServices;
    using System.Threading;
    using EnvDTE;
    using Microsoft.VisualStudio.Shell;
    using Microsoft.VisualStudio.ComponentModelHost;
    using Microsoft.VisualStudio.Shell.Interop;
    using OrderManagement.Addin.Commands;
    using Utilities;
    using SolutionConfiguration = Models.Configuration.SolutionConfiguration;

    /// <summary>
    /// This is the class that implements the package exposed by this assembly.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The minimum requirement for a class to be considered a valid package for Visual Studio
    /// is to implement the IVsPackage interface and register itself with the shell.
    /// This package uses the helper classes defined inside the Managed Package Framework (MPF)
    /// to do it: it derives from the Package class that provides the implementation of the
    /// IVsPackage interface and uses the registration attributes defined in the framework to
    /// register itself and its components with the shell. These attributes tell the pkgdef creation
    /// utility what data to put into .pkgdef file.
    /// </para>
    /// <para>
    /// To get loaded into VS, the package must be referred by &lt;Asset Type="Microsoft.VisualStudio.VsPackage" ...&gt; in .vsixmanifest file.
    /// </para>
    /// </remarks>
    [PackageRegistration(UseManagedResourcesOnly = true, AllowsBackgroundLoading = true)]
    [InstalledProductRegistration("#110", "#112", "1.0", IconResourceID = 400)] // Info on this package for Help/About
    [Guid(PackageGuidString)]
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly",
        Justification = "pkgdef, VS and vsixmanifest are valid VS terms")]
    [ProvideMenuResource("Menus.ctmenu", 1)]
    [ProvideAutoLoad(UIContextGuids80.NoSolution, PackageAutoLoadFlags.BackgroundLoad)]
    [ProvideAutoLoad(UIContextGuids80.SolutionExists, PackageAutoLoadFlags.BackgroundLoad)]
    public sealed class OmAsyncPackage : AsyncPackage {
        /// <summary>
        /// OmAsyncPackage GUID string.
        /// </summary>
        public const string PackageGuidString = "5dc77444-f14b-4404-b464-3267c030f59b";

        [Export]
        public SolutionConfiguration CurrentSolutionSettings {
            get {
                ThreadHelper.ThrowIfNotOnUIThread();
                if (!(GetGlobalService(typeof(DTE)) is DTE dte) || !dte.Solution.IsOpen)
                    return null;
                var solutionName = dte.Solution.FileName;
                var componentModel = (IComponentModel) GetGlobalService(typeof(SComponentModel));
                var settings = componentModel.GetService<SettingsPersister>();

                var project = settings.Settings.Projects.FirstOrDefault(item => solutionName.StartsWith(item.SolutionName));
                return project ?? new SolutionConfiguration();
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OmAsyncPackage"/> class.
        /// </summary>
        public OmAsyncPackage() {
            // Inside this method you can place any initialization code that does not require
            // any Visual Studio service because at this point the package object is created but
            // not sited yet inside Visual Studio environment. The place to do all the other
            // initialization is the Initialize method.
        }

        #region Package Members

        /// <summary>
        /// Initialization of the package; this method is called right after the package is sited, so this is the place
        /// where you can put all the initialization code that rely on services provided by VisualStudio.
        /// </summary>
        /// <param name="cancellationToken">A cancellation token to monitor for initialization cancellation, which can occur when VS is shutting down.</param>
        /// <param name="progress">A provider for progress updates.</param>
        /// <returns>A task representing the async work of package initialization, or an already completed task if there is none. Do not return null from this method.</returns>
        protected override async System.Threading.Tasks.Task InitializeAsync(CancellationToken cancellationToken, IProgress<ServiceProgressData> progress) {
            await JoinableTaskFactory.SwitchToMainThreadAsync(cancellationToken);

            var componentModel = await GetServiceAsync(typeof(SComponentModel)) as IComponentModel;
            if (componentModel == null) throw new ArgumentNullException(nameof(componentModel));
            if (await GetServiceAsync(typeof(IMenuCommandService)) is OleMenuCommandService commandService) {
                var commands = componentModel.GetExtensions<CommandBase>();
                foreach (var command in commands) {
                    commandService.AddCommand(command.Command);
                }
            }

            await SubscribeSolutionEventsAsync();
        }

        private async System.Threading.Tasks.Task SubscribeSolutionEventsAsync() {
            await JoinableTaskFactory.SwitchToMainThreadAsync(DisposalToken);
            var extensibility = (IVsExtensibility) await GetServiceAsync(typeof(IVsExtensibility));
            if (extensibility != null) {
                var app = extensibility.GetGlobalsObject(null).DTE;
                app.Events.SolutionEvents.Opened += Solution_Opened;
                //app.Events.SolutionEvents.BeforeClosing += Solution_BeforeClosing;
            }
        }

        private void Solution_Opened() {
            
        }

        #endregion
    }
}