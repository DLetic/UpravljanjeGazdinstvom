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
using Gazdinstvo.MVVM.View;
namespace Gazdinstvo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public SQLiteDataReader reader;
        DatabaseContext databaseContext = new DatabaseContext();
        List<Farmer> farmers = new List<Farmer>();
       
        Register register = new Register();
        
        public MainWindow()
        {
            InitializeComponent();
        }



        private void Button_Click_Login(object sender, RoutedEventArgs e)
        {

            farmers = databaseContext.getFarmer(tbUserName.Text);
            int checkUser,checkPass = 1;
            checkUser = farmers.FindIndex(s => s.farmerName == tbUserName.Text);
            checkPass = farmers.FindIndex(s => s.farmerPassword == tbPassword.Password);
            
            if (checkUser == 0 && checkPass == 0)
            {
                MenuWindow menuWindow = new MenuWindow();
                menuWindow.PGName.Text = tbUserName.Text;
                menuWindow.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Pogrešno korisničko ime ili šifra");
            }
            
           
            
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

        private void Button_Click_Register(object sender, RoutedEventArgs e)
        {

            //    DatabaseContext database = new DatabaseContext();
            // database.CreateDatabase();
            register.Show();
            this.Close();
            
        }

        private void Button_Close_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();

        }

        private void themeToggle_Click(object sender, RoutedEventArgs e)
        {

        }
    }


    
}
