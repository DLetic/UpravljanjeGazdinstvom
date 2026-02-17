using Gazdinstvo.MVVM.Model;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Gazdinstvo.MVVM.View
{
    /// <summary>
    /// Interaction logic for Finances.xaml
    /// </summary>
    public partial class Finances : UserControl
    {
        DatabaseContext databaseContext = new DatabaseContext();
        PieChart pieChart = new PieChart();
        Customer customer = new Customer();
        List<String> customerTotals = new List<String>();
        public int customerTotal = 10;
        public SeriesCollection PieChartCustomer { get; set; }
        public int proba { get; set; }
        

        public Finances()
        {
            InitializeComponent();
            cmbCustomerSelector.ItemsSource = databaseContext.getCustomer();


            PieChartCustomer = new SeriesCollection
            {
                new PieSeries
                {
                    Title = "Ukupno",
                    Values = new ChartValues<ObservableValue> { new ObservableValue(customerTotal) },
                    DataLabels = true
                },
                new PieSeries
                {
                    Title = "Placeno",
                    Values = new ChartValues<ObservableValue> { new ObservableValue(Convert.ToInt32(tbPayment.Text))},
                    DataLabels = true
                },
            };


            customerTotal = 100;


        }

        private void cmbCustomerSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataContext = null;
            customer = (Customer)cmbCustomerSelector.SelectedItem;
            customerTotals = databaseContext.getCustomerTotal("itemTotal", "Orders", "itemTotal", customer.customerPIB);
            customerTotal = 0;
            foreach (var total in customerTotals)
            {
                customerTotal += Convert.ToInt32(total);
            }

            PieChartCustomer = new SeriesCollection
            {
                new PieSeries
                {
                    Title = "Ukupno",
                    Values = new ChartValues<ObservableValue> { new ObservableValue(customerTotal) },
                    DataLabels = true
                },
                new PieSeries
                {
                    Title = "Placeno",
                    Values = new ChartValues<ObservableValue> { new ObservableValue(Convert.ToInt32(tbPayment.Text))},
                    DataLabels = true
                },
            };
            DataContext = this;
        }

        private void PieChart_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
           
        }


        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, e);
        }
    }
}
