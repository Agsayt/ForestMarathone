using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForestMarathone.ViewModels
{
    public class AuthViewModel : INotifyPropertyChanged
    {
        public string UserName { get => "admin";}

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
