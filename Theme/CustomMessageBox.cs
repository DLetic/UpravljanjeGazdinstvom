using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Gazdinstvo.Theme
{
    public partial class CustomMessageBox : Form
    {
        public CustomMessageBox()
        {
            InitializeComponent();
        }

        static CustomMessageBox newMessageBox;
        static string Button_ID;

        public string ShowBox(string txtMessage)
        {
            newMessageBox = new CustomMessageBox();
            newMessageBox.label2.Text = txtMessage;
            newMessageBox.ShowDialog();
            return Button_ID;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Button_ID = "1";
            newMessageBox.Dispose();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Button_ID = "2";
            newMessageBox.Dispose();
        }


    }
}
