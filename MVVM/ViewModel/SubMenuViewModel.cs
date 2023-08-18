using Gazdinstvo.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gazdinstvo.MVVM.ViewModel
{
    class SubMenuViewModel : ObservableObject
    {
        public RelayCommand NewInvoiceCommand { get; set; }
        public NewInvoiceViewModel NewInvoiceVM { get; set; }

        public RelayCommand probaCommand { get; set; }

        public UserControl1 UserControl1VM { get; set; }

        private object _currentSubView;

        public object CurrentSubView
        {
            get { return _currentSubView; }
            set { _currentSubView = value; OnPropertyChanged(); }
        }
        public SubMenuViewModel()
        {
            NewInvoiceVM = new NewInvoiceViewModel();
            CurrentSubView = NewInvoiceVM;

            UserControl1VM = new UserControl1();

            NewInvoiceCommand = new RelayCommand(o =>
            {
                CurrentSubView = NewInvoiceVM;
            });

            probaCommand = new RelayCommand(o =>
            {
                CurrentSubView = UserControl1VM;
            });
        }
    }
}
