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

        public void GetCustomers()
        {
            List<ProbaClass> probaClasses = new List<ProbaClass>();





            List<Order> i = new List<Order>();
            Order item = new Order();
           

            
            
     





           

            item.itemDescription = "PROBA";
            i.Add(item);

            List<string> s = new List<string>();

            List<Item> items = new List<Item>();
           

        
            ProbaClass probaClass = new ProbaClass();

            probaClass.Item = databaseContext.getItems();
            //probaClass.Proba = databaseContext.getIT();

            tb.FilterMode = AutoCompleteFilterMode.ContainsOrdinal;
            tb.ItemsSource = databaseContext.getIT();
            //GridProducts.DataContext = probaClass;
            //autopick.DataContext = databaseContext.getIT();
            GridProducts.DataContext = databaseContext.getItems();
          
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
           Order item = new Order();
            // item = (Order)ItemsDataGrid.SelectedItem;
            item.itemDescription = tb.Text;
            databaseContext.addItem(item);
        }

        private void ItemsDataGrid_AddingNewItem(object sender, AddingNewItemEventArgs e)
        {
            List<Item> proba = new List<Item>();
            Item p = new Item();
            

         //   p.Pr = s;
            proba.Add(p);
            proba.Add(p);
           // GridProducts.DataContext = proba;
            ItemsDataGrid.ItemsSource = databaseContext.getItems();
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
