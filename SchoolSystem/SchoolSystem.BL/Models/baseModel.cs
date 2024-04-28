using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SchoolSystem.BL.Models
{
    public abstract record baseModel : INotifyPropertyChanged, IModel
    {
        public Guid Id { get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}