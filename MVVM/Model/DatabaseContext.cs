using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gazdinstvo.MVVM.Model
{
    class DatabaseContext 
    {
        public SQLiteDataReader reader;
        public SQLiteDataReader reader1;
       // SQLiteConnection dbConnection = new SQLiteConnection(@"Data Source=C:\Users\cenejac\Downloads\proba.db;");
        public void OpenConnection()
        {
         //   dbConnection.Close();
        }
        public DatabaseContext()
        {
            
         //   dbConnection.Open();
       //     SQLiteCommand command = new SQLiteCommand("SELECT * FROM Buyer", dbConnection);
  //          SQLiteCommand command1 = new SQLiteCommand("SELECT * from orders JOIN customer on orders.customerId = customer.customerId & 2 JOIN item on item.itemId = orders.itemId", dbConnection);

     //       reader = command.ExecuteReader();
        //    reader1 = command1.ExecuteReader();

            
        }


        public List<Customer> getCustomer()
        {
            try
            {
                SQLiteConnection i_dbConnection = new SQLiteConnection(@"Data Source = C:\Users\cenejac\Downloads\proba.db;");
                SQLiteCommand i_dbCommand = new SQLiteCommand("SELECT * from customer", i_dbConnection);
                SQLiteDataAdapter i_dbAdapter = new SQLiteDataAdapter(i_dbCommand);
                DataTable i_dataTable = new DataTable();
                i_dbAdapter.Fill(i_dataTable);
                var Customer = new List<Customer>();
                foreach (DataRow row in i_dataTable.Rows)
                {
                    var obj = new Customer()
                    {
                        customerName = Convert.ToString(row["customerName"]),
                        customerAdress = Convert.ToString(row["customerAdrress"]),
                        customerPIB = Convert.ToInt32(row["customerPIB"])
                    };
                    Customer.Add(obj);
                    i_dbConnection.Close();
                }
                return Customer;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<string> getDbItem(string column, string table,string name)
        {
            try
            {
                SQLiteConnection i_dbConnection = new SQLiteConnection(@"Data Source = C:\Users\cenejac\Downloads\proba.db;");
                SQLiteCommand i_dbCommand = new SQLiteCommand("SELECT " + column + "from " + table, i_dbConnection);
                SQLiteDataAdapter i_dbAdapter = new SQLiteDataAdapter(i_dbCommand);
                DataTable i_dataTable = new DataTable();
                i_dbAdapter.Fill(i_dataTable);
                List<string> itemsDb = new List<string>();
                foreach (DataRow row in i_dataTable.Rows)
                {
                    itemsDb.Add(Convert.ToString(row[name]));
                    i_dbConnection.Close();
                }
                /*   foreach(Proba i in Items)
                   {
                       i.LitemDescription = s;
                   }*/

                //   item.LitemDescription = s;
                // item.itemId = s2;


                return itemsDb;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<Item> getItem()
        {
            try
            {
                SQLiteConnection i_dbConnection = new SQLiteConnection(@"Data Source = C:\Users\cenejac\Downloads\proba.db;");
                SQLiteCommand i_dbCommand = new SQLiteCommand("SELECT * from item", i_dbConnection);
                SQLiteDataAdapter i_dbAdapter = new SQLiteDataAdapter(i_dbCommand);
                DataTable i_dataTable = new DataTable();
                i_dbAdapter.Fill(i_dataTable);
                List<string> itemsDescription = new List<string>();
                var item = new List<Item>();
                foreach (DataRow row in i_dataTable.Rows)
                {
                    var obj = new Item()
                    {

                    LitemPrice = Convert.ToInt32(row["itemPrice"]),
                    LitemDescription = Convert.ToString(row["itemDescription"])

                };
                    item.Add(obj);
                  
                    i_dbConnection.Close();
                }
                /*   foreach(Proba i in Items)
                   {
                       i.LitemDescription = s;
                   }*/
               
             //   item.LitemDescription = s;
               // item.itemId = s2;
                
                
                return item;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<Order> getItems(int order)
        {
            try
            {
                SQLiteConnection i_dbConnection = new SQLiteConnection(@"Data Source = C:\Users\cenejac\Downloads\proba.db;");
                SQLiteCommand i_dbCommand = new SQLiteCommand("SELECT * FROM Orders JOIN Customer ON Orders.customerPIB = Customer.customerPIB JOIN Item ON Orders.itemDescription = Item.itemDescription WHERE orderNumber =" + order, i_dbConnection);
                SQLiteDataAdapter i_dbAdapter = new SQLiteDataAdapter(i_dbCommand);
                DataTable i_dataTable = new DataTable();
                i_dbAdapter.Fill(i_dataTable);
                var Items = new List<Order>();
                int itemNum = 0;
                foreach(DataRow row in i_dataTable.Rows)
                {
                    itemNum++;
                    var obj = new Order()
                    {
                      itemNumber = itemNum,
                      itemDescription = Convert.ToString(row["itemDescription"]),
                      itemPrice = Convert.ToInt32(row["itemPrice"]),
                      itemQuantity = Convert.ToInt32(row["itemQuantity"]),
                      itemTotal = Convert.ToInt32(row["itemTotal"])
                    };
                    Items.Add(obj);
                    i_dbConnection.Close();
                }
                return Items;
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        public int addItem(Order item)
        {
            try
            {
                const string query = "INSERT INTO Orders(itemDescription,itemQuantity,itemTotal,customerPIB,orderNumber) VALUES (@itemDescription,@itemQuantity,@itemTotal,@customerPIB,@orderNumber);";
                var args = new Dictionary<string, object>
                {                
                    {"@itemDescription",item.itemDescription},
                    {"@itemQuantity",item.itemQuantity},
                    {"@customerPIB",item.customerPIB},
                    {"@itemTotal",item.itemTotal},
                    {"@orderNumber",item.orderNumber},
                };
                return ExecuteWrite(query, args);
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        public int ExecuteWrite(string query,Dictionary<string,object> args)
        {
            int numOfRowsAffected;

            using (var connection = new SQLiteConnection(@"Data Source = C:\Users\cenejac\Downloads\proba.db;"))
            {
                connection.Open();
                using (var command = new SQLiteCommand(query, connection))
                {
                    foreach(var pair in args)
                    {
                        command.Parameters.AddWithValue(pair.Key, pair.Value);
                    }
                    numOfRowsAffected = command.ExecuteNonQuery();
                }
                return numOfRowsAffected;
            }
        }


    }
}
