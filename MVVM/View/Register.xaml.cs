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
using System.Windows.Shapes;
using System.Data.SQLite;
using Gazdinstvo.MVVM.Model;

namespace Gazdinstvo.MVVM.View
{
    /// <summary>
    /// Interaction logic for Register.xaml
    /// </summary>
    public partial class Register : Window
    {
        DatabaseContext databaseContext = new DatabaseContext();
        List<Farmer> farmers = new List<Farmer>();
        MenuWindow menuWindow = new MenuWindow();
        public Register()
        {
            InitializeComponent();
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            if (tbPassword.Password != tbPasswordRepeat.Password || string.IsNullOrEmpty(tbPassword.Password))
            {
                MessageBox.Show("Šifre se ne poklapaju");
            }
            else if(string.IsNullOrEmpty(tbAdress.Text))      
            {
                MessageBox.Show("Niste uneli adresu");
            }
            else{
                    Farmer farmer = new Farmer();
                    farmer.farmerName = tbUserName.Text;
                    farmer.farmerPassword = tbPassword.Password.ToString();
                    farmer.farmerAdress = tbAdress.Text;
                    farmer.farmerBPG = Convert.ToInt64(tbBPG.Text);
                    farmer.accountNumber = tbAccount.Text   ;
                    databaseContext.CreateDatabase(farmer.farmerName);
                    databaseContext.addFarmer(farmer);
                    menuWindow.Show();
                    this.Close();
            }         
        }
    }
}
