using Gazdinstvo.MVVM.Model;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for AllInvoices.xaml
    /// </summary>
    public partial class AllInvoices : UserControl
    {
        DatabaseContext databaseContext = new DatabaseContext();

        List<CustomerOrder> customerOrders = new List<CustomerOrder>();
        CustomerOrder customerOrder = new CustomerOrder();
        List<Order> order = new List<Order>();

        
        public AllInvoices()
        {
            InitializeComponent();



            customerOrders = databaseContext.getOrders();


            OrdersDataGrid.ItemsSource = customerOrders.Distinct().ToList();

            tbProductSearch.FilterMode = AutoCompleteFilterMode.ContainsOrdinal;
            tbProductSearch.ItemsSource = databaseContext.getDbItem("*", "item", "itemDescription");
           


        }

        private void OrdersDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (string.IsNullOrEmpty(OrdersDataGrid.SelectedItem.ToString()))
            {
                MessageBox.Show("Niste obelezili otpremnicu");

            }
            else
            {
                Window window = new Window();
                window.Content = InvoiceData();      
                window.Show();
               
                
            }
        }

        private void tbProductSearch_LostFocus(object sender, RoutedEventArgs e)
        {
            List<string[,]> list = new List<string[,]>();
            List<CustomerOrder> d = new List<CustomerOrder>();

            string select = tbProductSearch.SelectedItem.ToString();


            list = databaseContext.getItemOrderNumber(select);

            int i, j;
            int num = 0;

            foreach (var item in list)
            {
                num = Convert.ToInt32(item[0, 0]);
                d.Add(customerOrders.Find(x => x.O.orderNumber == num));
            }

            OrdersDataGrid.ItemsSource = d;
            
           

            

        }

        private void PrintInvoice_Click(object sender, RoutedEventArgs e)
        {
            PrintDialog printDialog = new PrintDialog();

            if (printDialog.ShowDialog() == true)
            {
                printDialog.PrintVisual(InvoiceData().PrintPage, "Invoice");
            }
        }

        private Pages.Invoice InvoiceData()
        {
            Pages.Invoice invoice = new Pages.Invoice();
            int total = 0;
            customerOrder = (CustomerOrder)OrdersDataGrid.SelectedItem;
            order = databaseContext.getItems(customerOrder.O.orderNumber);
            invoice.ItemsDataGrid.DataContext = order;
            invoice.CustomerData.DataContext = customerOrder.C;

            order.ForEach(itemTotal =>
            {
                total += itemTotal.itemTotal;
            });
            string dbName = DatabaseContext.DB;
            invoice.FarmerData.DataContext = databaseContext.getFarmer(dbName);
            invoice.TotalPrice.Text = total.ToString();
            invoice.InvoiceDate.Text = customerOrder.O.date;
            invoice.InvoiceNumber.Text = customerOrder.O.orderNumber.ToString();
            return invoice;
        }

        private void ViewInvoice_Click(object sender, RoutedEventArgs e)
        {
            Window window = new Window();
            window.Content = InvoiceData();
            window.Show();
        }
    }
}
