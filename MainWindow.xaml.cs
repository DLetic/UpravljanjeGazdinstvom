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
using System.Data;
using System.Collections.ObjectModel;
using System.Data.SQLite;
using Gazdinstvo.MVVM.Model;

namespace Gazdinstvo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public SQLiteDataReader reader;
        public MainWindow()
        {
            InitializeComponent();
        }



        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MenuWindow menuWindow = new MenuWindow();
            
            menuWindow.Show();
            this.Close();
            
           /* try
            {
                
                
                this.IsEnabled = false;
                PrintDialog printDialog = new PrintDialog();
                Pages.Invoice invoice = new Pages.Invoice();
               
                Items item = new Items();
                ObservableCollection<Items> items = new ObservableCollection<Items>();
                
                item.itemNumber = 1;
                item.itemDescription = "jedandvatricetiripet";
                item.itemQuantity = 5;
                item.itemPrice = 100;
                item.itemTotal = item.itemQuantity * item.itemPrice;
               
                items.Add(item);

                DatabaseContext databaseContext = new DatabaseContext();

                

                invoice.ItemsDataGrid.DataContext = databaseContext.getItems();
              


                for (int i = 1; i < 7; i++)
                {
                    string itemNumber = "ItemNumber";
                    itemNumber += i.ToString();
                    
                    
                }


                if (printDialog.ShowDialog() == true)
                {
                    printDialog.PrintVisual(invoice.PrintPage, "Invoice");
                }
            }
            catch
            {
                this.IsEnabled = true;
            }*/
        }
    }

        public class Items
        {
            public int itemNumber { get; set; }
            public string itemDescription { get; set; }
            public int itemQuantity { get; set; }

            public int itemPrice { get; set; }

            public int itemTotal { get; set; }
        }
    
}
