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
        public static bool OrderNumberState = false; 
        DatabaseContext databaseContext = new DatabaseContext();
        ObservableCollection<Order> items = new ObservableCollection<Order>();
        List<Farmer> farmer = new List<Farmer>();
        int orderNum = 1;
       
        string dbName;
        MenuWindow menu { get; set; }

        Order order = new Order();

        Customer customer = new Customer();

        int totalPrice = 0;

        public NewInvoiceView()
        {
            InitializeComponent();
            
            GetCustomers();
            dbName = DatabaseContext.DB;
            InvoiceDate.Text = DateTime.Now.ToString("dd.MM.yyyy");
        }

        public class OrderItem
        { 
          public List<Order> Order { get; set; }
          public List<Item> Item { get; set; }
        }
        List<OrderItem> OrderItemes = new List<OrderItem>();
        OrderItem orderItem = new OrderItem();
        public void GetCustomers()
        {
            
            List<OrderItem> OrderItemes = new List<OrderItem>();

            



            List<Order> i = new List<Order>();
            Order item = new Order();



      




           



            item.itemDescription = "PROBA";
            i.Add(item);

            List<string> s = new List<string>();

          
            ItemsDataGrid.DataContext = items;
            

            int orderNumber = 1;
            var intList = databaseContext.getDbItem("*","orders","orderNumber").Select(s => Convert.ToInt32(s)).ToList();

            if(!intList.Any())
            {
                intList.Add(orderNum);
            }else
            {
                orderNum = intList.Max() + 1;
            }


            if (OrderNumberState)
            {
                orderNum = intList.Max();
                ItemsDataGrid.DataContext = databaseContext.getItems(orderNum);
            }

            int PIB = 0;

            InvoiceNumber.Text = Convert.ToString(orderNum);


            //OrderItem.Item = databaseContext.getItems();
            //OrderItem.Proba = databaseContext.getIT();
            

            tbProduct.FilterMode = AutoCompleteFilterMode.ContainsOrdinal;
            tbProduct.ItemsSource = databaseContext.getDbItem("*","item","itemDescription");
            //GridProducts.DataContext = OrderItem;
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
            //ODRADI DA KAD SE PROMENI PROZOR PITA TE DAL DA SACUVAMO OTPREMICU
             Item item = new Item();
             orderItem.Item = databaseContext.getItem();

             //item = (Order)ItemsDataGrid.SelectedItem;
             order.orderNumber = orderNum;
             order.itemDescription = tbProduct.Text;
             order.itemQuantity = Int32.Parse(tbQuantity.Text);
             order.customerPIB = customer.customerPIB;
             item = orderItem.Item.Find(x => x.LitemDescription.Contains(order.itemDescription));
             order.itemPrice = item.LitemPrice;
             order.itemTotal = order.itemPrice * order.itemQuantity;
             totalPrice += order.itemTotal;
             order.date = DateTime.Now.ToString("dd.MM.yyyy");
             databaseContext.addItem(order);
             
             ItemsDataGrid.DataContext = databaseContext.getItems(order.orderNumber);
             items.Add(order);
             
             tbProduct.Text = null;
             OrderNumberState = true;
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
           try
           {

                items.Clear();
             //  this.IsEnabled = false;
               PrintDialog printDialog = new PrintDialog();
               Pages.Invoice invoice = new Pages.Invoice();


             //   Items item = new Items(); Popraviti print zeza


                DatabaseContext databaseContext = new DatabaseContext();

                farmer = databaseContext.getFarmer(dbName);
                invoice.FarmerData.DataContext = farmer;
                invoice.CustomerData.DataContext = customer;
                invoice.ItemsDataGrid.DataContext = databaseContext.getItems(order.orderNumber);
                invoice.InvoiceDate.Text = DateTime.Now.ToString("dd.MM.yyyy");
                invoice.TotalPrice.Text = totalPrice.ToString();
                invoice.InvoiceNumber.Text = InvoiceNumber.Text.ToString();

                if (printDialog.ShowDialog() == true)
               {
                   printDialog.PrintVisual(invoice.PrintPage, "Invoice");
               }
                GridProducts.DataContext = null;
                tbProduct.Text = null;
                tbQuantity.Text = null;
                GetCustomers();
                OrderNumberState = false;
            }
           catch
           {
            //   this.IsEnabled = true;
           }
        }

        private void cmbCustomerSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            //Ovde treba napraviti da se sacuva ako se promeni na drugog kupca


            customer = (Customer)cmbCustomerSelector.SelectedItem;
        }

        private void NewInvoice_ContextMenuClosing(object sender, ContextMenuEventArgs e)
        {
            
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
