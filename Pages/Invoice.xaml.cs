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

namespace Gazdinstvo.Pages
{
    /// <summary>
    /// Interaction logic for Invoice.xaml
    /// </summary>
    public partial class Invoice : Page
    {

        //  private MainWindow _mainWindow;

        public Invoice()
        {
            InitializeComponent();

       //     _mainWindow = mainWindow;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
         //       _mainWindow.IsEnabled = false;
                PrintDialog printDialog = new PrintDialog();
                if(printDialog.ShowDialog()==true)
                {
                    printDialog.PrintVisual(PrintPage,"Invoice");
                }
            }
            catch
            {
       //         _mainWindow.IsEnabled = true;
            }
        }
    }
}
