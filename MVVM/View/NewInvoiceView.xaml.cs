using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using Gazdinstvo.MVVM.Model;

namespace Gazdinstvo.MVVM.View
{
    /// <summary>
    /// Interaction logic for NewInvoiceView.xaml
    /// </summary>
    public partial class NewInvoiceView : UserControl
    {
        DatabaseContext databaseContext = new DatabaseContext();
        int last = 1;

        Customer customer = new Customer();

        public NewInvoiceView()
        {
            InitializeComponent();
            
            GetCustomers();
        }

        public class ProbaClass
        { 
          public List<Order> Item { get; set; }
          public List<Item> Proba { get; set; }
        }
        List<ProbaClass> probaClasses = new List<ProbaClass>();
        ProbaClass probaClass = new ProbaClass();
        public void GetCustomers()
        {
            List<ProbaClass> probaClasses = new List<ProbaClass>();

            



            List<Order> i = new List<Order>();
            Order item = new Order();
           

            
            
     





           

            item.itemDescription = "PROBA";
            i.Add(item);

            List<string> s = new List<string>();

            List<Item> items = new List<Item>();

            

            int orderNumber = 0;
            var intList = databaseContext.getDbItem("*","orders","orderNumber").Select(s => Convert.ToInt32(s)).ToList();

            if(intList == null)
            {
                intList.Add(last);
            }else
            {
                last = intList.Max() + 1;
            }

            int PIB = 0;
            

            

            //probaClass.Item = databaseContext.getItems();
            //probaClass.Proba = databaseContext.getIT();

            tbProduct.FilterMode = AutoCompleteFilterMode.ContainsOrdinal;
            tbProduct.ItemsSource = databaseContext.getDbItem("*","item","itemDescription");
            //GridProducts.DataContext = probaClass;
            //autopick.DataContext = databaseContext.getIT();
            


            //comboBox selector for customer

            cmbCustomerSelector.ItemsSource = databaseContext.getCustomer();


            /*  GridProducts.DataContext = new
              {
                  Proba = databaseContext.getIT(),
                  Item = databaseContext.getItems(),
              };
            */

            /*   while (databaseContext.reader.Read())
               {
                   Customer data = new Customer();

                   data.customerId = databaseContext.reader.GetInt32(0);
                   data.customerName = databaseContext.reader.GetString(1);
                   data.customerAdress = databaseContext.reader.GetString(2);
                   data.customerPIB = databaseContext.reader.GetInt32(3);

                   customers.Add(data);
               }*/

            /*   Items items = new Items();
               items.itemDescription = "proba";
               items.itemNumber = 1;
               items.itemPrice = 100;
               items.itemQuantity = 100;
               items.itemTotal = 100000;

               ObservableCollection<Items> it = new ObservableCollection<Items>();
               it.Add(items);

               ItemsDataGrid.DataContext = it;*/

            // ItemsDataGrid.DataContext = databaseContext.reader1;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Order order = new Order();
            Item item = new Item();
            probaClass.Proba = databaseContext.getItem();
            
            // item = (Order)ItemsDataGrid.SelectedItem;
            order.orderNumber = last;
            order.itemDescription = tbProduct.Text;
            order.itemQuantity = Int32.Parse(tbQuantity.Text);
            order.customerPIB = customer.customerPIB;
            item = probaClass.Proba.Find(x => x.LitemDescription.Contains(order.itemDescription));
            order.itemPrice = item.LitemPrice;
            order.itemTotal = order.itemPrice * order.itemQuantity;

            databaseContext.addItem(order);
            GridProducts.DataContext = databaseContext.getItems(order.orderNumber);
        }

        private void ItemsDataGrid_AddingNewItem(object sender, AddingNewItemEventArgs e)
        {
            List<Item> proba = new List<Item>();
            Item p = new Item();
            

         //   p.Pr = s;
            proba.Add(p);
            proba.Add(p);
           // GridProducts.DataContext = proba;
           // ItemsDataGrid.ItemsSource = databaseContext.getItems();
        }

        private void Print(object sender, RoutedEventArgs e)
        {

        }

        private void cmbCustomerSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {




            customer = (Customer)cmbCustomerSelector.SelectedItem;
        }

        /*   private void tb_SelectionChanged(object sender, SelectionChangedEventArgs e)
           {

               Order item = new Order();
               item.itemQuantity = 1000;
               item.itemPrice = 10;
               item.itemDescription = "proba";
            //   item.itemDescription = tb.Text;
               databaseContext.addItem(item);
           }*/
    }
}
