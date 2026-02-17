using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gazdinstvo.MVVM.Model
{
    public class CustomerOrder
    {
            public Order O { get; set; }
            public Customer C { get; set; }
    }
}
