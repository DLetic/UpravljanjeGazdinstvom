using Gazdinstvo.MVVM.Model;
using Gazdinstvo.Theme;
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
    /// Interaction logic for Customer.xaml
    /// </summary>
    public partial class Customers : UserControl
    {
        DatabaseContext databaseContext = new DatabaseContext();
        public Customers()
        {
            InitializeComponent();
            List<Customer> customers = new List<Customer>();
            customers = databaseContext.getCustomer();
            CustomersDataGrid.ItemsSource = customers;
            
        }

        private void Button_Click_Update(object sender, RoutedEventArgs e)
        {
            if(string.IsNullOrEmpty(CustomerName.ToString()))
            {
                MessageBox.Show("Niste obelezili kupca kojeg zelite izmeniti");
            }
            else
            {
                Customer customer = new Customer();
                customer.customerName = CustomerName.Text.ToString();
                customer.customerAdress = CustomerAdress.Text.ToString();
                customer.customerPIB = Convert.ToInt32(CustomerPIB.Text.ToString());
                databaseContext.UpdateCustomer(customer);
            }
            
        }

        private void Button_Click_Delete(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(CustomersDataGrid.SelectedItem.ToString()))
            {
                MessageBox.Show("Niste obelezili kupca kojeg zelite obrisati");

            }
            else
            {
                Customer customer = (Customer)CustomersDataGrid.SelectedItem;
                databaseContext.DeleteCustomer(customer);
                CustomersDataGrid.ItemsSource = databaseContext.getCustomer();
            }


        }

        private void AddNewCustomer(object sender, RoutedEventArgs e)
        {
            CustomMessageBox customMessageBox = new CustomMessageBox();
            List<string> customerCheck = new List<string>();
            string[] CustomButtons = new string[] { "Da", "Ne" };
            Customer customer = new Customer();
            int index = 1;
            if (string.IsNullOrEmpty(CustomerName.Text) || string.IsNullOrEmpty(CustomerPIB.Text) || string.IsNullOrEmpty(CustomerAdress.Text))
            {
                MessageBox.Show("Niste uneli poslovno ime, adresu ili matični broj");
            }
            else
            {
                if (CustomerPIB.Text.Length != 9)
                {
                    MessageBox.Show("Matični broj treba da ima 9 cifara");
                }
                else
                {
                    customer.customerName = CustomerName.Text.ToString();
                    customer.customerAdress = CustomerAdress.Text.ToString();
                    customer.customerPIB = Convert.ToInt32(CustomerPIB.Text);
                    customerCheck = databaseContext.getDbItem("*", "customer", "customerName");
                    index = customerCheck.FindIndex(s => s.Contains(customer.customerName, StringComparison.OrdinalIgnoreCase));
                }
               
            }




            if (index == -1)
            {
                databaseContext.addCustomer(customer);
                CustomersDataGrid.ItemsSource = databaseContext.getCustomer();
            }
            else
            {
                if (index != 1)
                {
                    string res = customMessageBox.ShowBox("Ovaj kupac postoji da li zelite da izmenite podatke");
                    if (res == "1")
                    {
                        CustomerName.Focus();
                    }
                    else
                    {
                        CustomerName.Clear();
                        CustomerAdress.Clear();
                        CustomerPIB.Clear();
                    }
                }
            }
        }

        private void CustomersDataGrid_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void CustomersDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (string.IsNullOrEmpty(CustomersDataGrid.SelectedItem.ToString()))
            {
                MessageBox.Show("Obeležili ste prazno polje");

            }
            else
            {
                Customer customer = (Customer)CustomersDataGrid.SelectedItem;
                CustomerName.Text = customer.customerName;
                CustomerAdress.Text = customer.customerAdress;
                CustomerPIB.Text = customer.customerPIB.ToString();

            }
        }
    }
}
