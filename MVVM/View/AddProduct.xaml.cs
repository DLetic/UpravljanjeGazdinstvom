using Gazdinstvo.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Gazdinstvo.Theme;
using System.Linq;
namespace Gazdinstvo.MVVM.View
{
    /// <summary>
    /// Interaction logic for AddProduct.xaml
    /// </summary>
    public partial class AddProduct : UserControl
    {
        DatabaseContext databaseContext = new DatabaseContext();
        
        public AddProduct()
        {
            InitializeComponent();
           
            ProductsDataGrid.SelectionMode = DataGridSelectionMode.Extended ;
            ProductsDataGrid.DataContext = GetProducts();
           // ProductsDataGrid.SelectionMode = (DataGridSelectionMode)DataGridSelectionUnit.Cell;
        }

        private void AddNewProduct(object sender, RoutedEventArgs e)
        {

            CustomMessageBox customMessageBox = new CustomMessageBox();
            List<string> itemsCheck = new List<string>();
            string[] CustomButtons = new string[] { "Da", "Ne" };
            Order item = new Order();
            int index = 1;
            if (string.IsNullOrEmpty(ImeProizvoda.Text) || string.IsNullOrEmpty(CenaProizvoda.Text))
            {
                MessageBox.Show("Niste uneli ime proizvoda ili cenu");
            }
            else
            {
                item.itemDescription = FirstCharToUpper(ImeProizvoda.Text.ToString());
                item.itemPrice = Convert.ToInt32(CenaProizvoda.Text);
                itemsCheck = databaseContext.getDbItem("*", "item", "itemDescription");
                index = itemsCheck.FindIndex(s => s.Contains(item.itemDescription, StringComparison.OrdinalIgnoreCase));
            }




            if (index == -1)
            {
             databaseContext.addProduct(item);
             ProductsDataGrid.DataContext = GetProducts();
            }
            else
            {
                if(index != 1)
                {
                    string res = customMessageBox.ShowBox("Ovaj proizvod postoji da li zelite da ga izmenite");
                    if (res == "1")
                    {
                        ImeProizvoda.Focus();
                    }
                    else
                    {
                        ImeProizvoda.Clear();
                        CenaProizvoda.Clear();
                    }
                }                                
            }

        }

        public static string FirstCharToUpper(string str)
        {
            if (str != "")
            {
                return str?.First().ToString().ToUpper() + str?.Substring(1).ToLower();
            }else
                return null;
        }

        private void Button_Click_Delete(object sender, RoutedEventArgs e)
        {
            if(string.IsNullOrEmpty(ProductsDataGrid.SelectedItem.ToString()))
            {
                MessageBox.Show("Niste obelezili proizvod koji zelite obrisati");

            }else
            {
                Item item = (Item)ProductsDataGrid.SelectedItem;
                databaseContext.DeleteProduct(item);
                ProductsDataGrid.DataContext = GetProducts();
            }
            

        }

        private void ProductsDataGrid_MouseRightButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            

        }

        private List<Item> GetProducts()
        {
            List<Item> item = databaseContext.getItem();
            return item;
        }

        private void Button_Click_Update(object sender, RoutedEventArgs e)
        {
            Item item = new Item();
            item.LitemDescription = ImeProizvoda.Text.ToString();
            item.LitemPrice = Convert.ToInt32(CenaProizvoda.Text.ToString());
            databaseContext.UpdateProduct(item);
            ProductsDataGrid.DataContext = GetProducts();
        }

        private void ProductsDataGrid_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (string.IsNullOrEmpty(ProductsDataGrid.SelectedItem.ToString()))
            {
                MessageBox.Show("Niste obelezili proizvod koji zelite promeniti");

            }
            else
            {
                Item item = (Item)ProductsDataGrid.SelectedItem;
                ImeProizvoda.Text = item.LitemDescription.ToString();
                CenaProizvoda.Text = item.LitemPrice.ToString();
            }
        }
    }
}
