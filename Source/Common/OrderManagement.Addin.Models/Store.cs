namespace OrderManagement.Addin.Models {
    using System;
    using PropertyChanged;

    [AddINotifyPropertyChangedInterface]
    public class Store {
        public int Id { get; set; }
        public string CompanyName { get; set; }
        public string ConnectionString { get; set; }
        public Guid Guid { get; set; }
    }
}