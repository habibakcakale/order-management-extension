namespace OrderManagement.Addin.Controls.ViewModels.Pickers
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel.Composition;
    using System.Windows.Controls;
    using Microsoft.Internal.VisualStudio.PlatformUI;
    using PropertyChanged;


    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [AddINotifyPropertyChangedInterface]
   public class ProjectSelectorViewModel
    {
        public ObservableCollection<object> Projects { get; set; }
        public object SelectedProject { get; set; }
        public SelectionMode SelectionMode { get; set; }
        public IList<object> SelectedProjects { get; set; }
        [ImportingConstructor]
        public ProjectSelectorViewModel() {
            this.Projects = new AsyncInitializedCollection<object>();
        }

    }
}
