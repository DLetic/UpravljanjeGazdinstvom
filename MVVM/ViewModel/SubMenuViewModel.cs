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

        public AllInvoicesViewModel allInvoicesVM { get; set; }

        public RelayCommand allInvoicesCommand { get; set; }

        public AddProductViewModel AddProductVM { get; set; }

        public RelayCommand AddProductCommand { get; set; }

        public RelayCommand CustomerCommand { get; set; }

        public CustomersViewModel customersVM { get; set; }

        private object _currentSubView;
        private object _saveState;

        public object CurrentSubView
        {
            get { return _currentSubView; }
            set { _currentSubView = value; OnPropertyChanged(); }
        }
        public object SaveState
        {
            get { return _saveState; }
            set { _saveState = value; OnPropertyChanged(); }
        }
        public SubMenuViewModel()
        {
            if (SaveState == null)
            {
                NewInvoiceVM = new NewInvoiceViewModel();
            }

          
            CurrentSubView = NewInvoiceVM;

            allInvoicesVM = new AllInvoicesViewModel();

            UserControl1VM = new UserControl1();

            AddProductVM = new AddProductViewModel();

            customersVM = new CustomersViewModel();

            NewInvoiceCommand = new RelayCommand(o =>
            {
                CurrentSubView = NewInvoiceVM;
            });

            probaCommand = new RelayCommand(o =>
            {
               CurrentSubView = UserControl1VM;
            });

            allInvoicesCommand = new RelayCommand(o =>
            {
                CurrentSubView = allInvoicesVM;
            });

            AddProductCommand = new RelayCommand(o =>
            {
                SaveState = CurrentSubView;
                CurrentSubView = AddProductVM;
            });

            CustomerCommand = new RelayCommand(o =>
            {
                CurrentSubView = customersVM;
            });


        }
    }
}
