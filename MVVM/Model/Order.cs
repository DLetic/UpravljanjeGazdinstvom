using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gazdinstvo.MVVM.Model
{
    public class Order
    {
            public int orderNumber { get; set; }
            public int itemNumber { get; set; }
            public string itemDescription { get; set; }
            public int itemQuantity { get; set; }
            public int itemPrice { get; set; }
            public int itemTotal { get; set; }
            public int customerPIB { get; set; }

            public string date { get; set; }
    }
}
