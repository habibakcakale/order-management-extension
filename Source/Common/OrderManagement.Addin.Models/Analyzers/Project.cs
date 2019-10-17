using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Addin.Models.Analyzers
{
    using PropertyChanged;

    [AddINotifyPropertyChangedInterface]
    public class Project
    {
        public string FilePath { get; set; }
        public string Name { get; set; }
    }
}
