namespace OrderManagement.Addin.Controls.ViewModels {
    using System;
    using Microsoft.VisualStudio.PlatformUI;

    class DelegateCommand<T> : DelegateCommand
    {
        public DelegateCommand(Action<T> execute) : base((obj) => execute((T)obj)) { }
        public DelegateCommand(Action<T> execute, Predicate<T> canExecute) : base((value => execute((T)value)), value => canExecute((T)value)) { }
    }
}