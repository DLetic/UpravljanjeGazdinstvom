using Gazdinstvo.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gazdinstvo.MVVM.ViewModel
{
    class MainViewModel : ObservableObject
    {
        public RelayCommand SubMenuCommand { get; set; }
        public RelayCommand NewInvoiceCommand { get; set; }
        public SubMenuViewModel SubMenuVM { get; set; }
        public NewInvoiceViewModel NewInvoiceVM { get; set; }

        private object _currentView;
    

        public object CurrentView
        {
            get { return _currentView; }
            set { _currentView = value; OnPropertyChanged(); }
        }

     
        public MainViewModel()
        {
            SubMenuVM = new SubMenuViewModel();
            NewInvoiceVM = new NewInvoiceViewModel();
          CurrentView = SubMenuVM;

            SubMenuCommand = new RelayCommand(o =>
            {
                CurrentView = SubMenuVM;
            });

            NewInvoiceCommand = new RelayCommand(o =>
            {
                CurrentView = NewInvoiceVM;
            });

        }
    }
}
