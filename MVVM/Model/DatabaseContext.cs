using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gazdinstvo.MVVM.Model
{
    class DatabaseContext 
    {
        public static string DB;
        static string databasePath ;
        string wanted_path = Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory()));
        // SQLiteConnection dbConnection = new SQLiteConnection(@"Data Source=C:\Users\cenejac\Downloads\proba.db;"); = @"Data Source = D:\Diplomski\Diplomski\Gazdinstvo\bin\Debug\net5.0-windows\DataBase.db;"
        public void OpenConnection()
        {
         //   dbConnection.Close();
        }
        public DatabaseContext()
        {
            
         //   command.ExecuteNonQuery();

            //   dbConnection.Open();
            //     SQLiteCommand command = new SQLiteCommand("SELECT * FROM Buyer", dbConnection);
            //          SQLiteCommand command1 = new SQLiteCommand("SELECT * from orders JOIN customer on orders.customerId = customer.customerId & 2 JOIN item on item.itemId = orders.itemId", dbConnection);

            //       reader = command.ExecuteReader();
            //    reader1 = command1.ExecuteReader();


        }

        public string CreateDatabase(string DataBaseName)
        {
            DB = DataBaseName;
            databasePath = @"Data Source =" + wanted_path + "\\Debug\\net5.0-windows\\" + DataBaseName + ".db";
            string message;
            try
            {
                SQLiteConnection.CreateFile(DataBaseName);
                string connectionString = "Data Source=" +DataBaseName + ".db;";
                SQLiteConnection i_dbConnection = new SQLiteConnection(connectionString);
                i_dbConnection.Open();
                string sql = "CREATE TABLE Customer ( customerName  varchar(20),customerAdress varchar(30),customerPIB integer(9) primary key not null)";
                SQLiteCommand command = new SQLiteCommand(sql, i_dbConnection);
                command.ExecuteNonQuery();
                sql = "CREATE TABLE Item ( itemDescription varchar(20) primary key not null unique,itemPrice integer(6))";
                command = new SQLiteCommand(sql, i_dbConnection);
                command.ExecuteNonQuery();
                sql = "CREATE TABLE Orders ( orderId INTEGER PRIMARY KEY NOT NULL UNIQUE,itemDescription varchar(30),itemQuantity integer(6),itemTotal integer(10),customerPIB integer(9),orderNumber integer(6),date varchar(10))";
                command = new SQLiteCommand(sql, i_dbConnection);
                command.ExecuteNonQuery();
                sql = "CREATE TABLE Farmer (farmerBPG integer(12) PRIMARY KEY NOT NULL UNIQUE,farmerName varchar(30),farmerAdress varchar(30),accountNumber varchar(20) NOT NULL,farmerPassword varchar(16))";
                command = new SQLiteCommand(sql, i_dbConnection);
                command.ExecuteNonQuery();
                message = "Baza kreirana";
            }catch (Exception e)
            {
                throw e;
            }
            return message;
        }


        public List<Customer> getCustomer()
        {
            try
            {
                SQLiteConnection i_dbConnection = new SQLiteConnection(databasePath);
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
                        customerAdress = Convert.ToString(row["customerAdress"]),
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

        public List<Farmer> getFarmer(string DataBaseName)
        {
            DB = DataBaseName;
            databasePath = @"Data Source =" + wanted_path + "\\Debug\\net5.0-windows\\" + DataBaseName + ".db";
            try
            {
                SQLiteConnection i_dbConnection = new SQLiteConnection(databasePath);
                SQLiteCommand i_dbCommand = new SQLiteCommand("SELECT * from farmer", i_dbConnection);
                SQLiteDataAdapter i_dbAdapter = new SQLiteDataAdapter(i_dbCommand);
                DataTable i_dataTable = new DataTable();
                i_dbAdapter.Fill(i_dataTable);
                var Farmer = new List<Farmer>();
                if (i_dataTable.Equals(null))
                {
                    return null;
                }
                foreach (DataRow row in i_dataTable.Rows)
                {
                    var obj = new Farmer()
                    {
                        farmerName = Convert.ToString(row["farmerName"]),
                        farmerAdress = Convert.ToString(row["farmerAdress"]),
                        farmerBPG = Convert.ToInt64(row["farmerBPG"]),
                        farmerPassword = Convert.ToString(row["farmerPassword"]),
                        accountNumber = Convert.ToString(row["accountNumber"])
                    };
                    Farmer.Add(obj);
                    i_dbConnection.Close();
                }
                return Farmer;
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
                SQLiteConnection i_dbConnection = new SQLiteConnection(databasePath);
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

        public List<string> getCustomerTotal(string column, string table, string name,int pib)
        {
            try
            {
                SQLiteConnection i_dbConnection = new SQLiteConnection(databasePath);
                SQLiteCommand i_dbCommand = new SQLiteCommand("SELECT " + column + " from " + table + " WHERE customerPIB=" + pib, i_dbConnection);
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
                SQLiteConnection i_dbConnection = new SQLiteConnection(databasePath);
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
                SQLiteConnection i_dbConnection = new SQLiteConnection(databasePath);
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

        public List<string[,]> getItemOrderNumber(string product)
        {
       

            try
            {
                SQLiteConnection i_dbConnection = new SQLiteConnection(databasePath);
                SQLiteCommand i_dbCommand = new SQLiteCommand("SELECT itemQuantity, orderNumber FROM Orders WHERE itemDescription ==" + "'" + product +  "'" ,i_dbConnection);
                SQLiteDataAdapter i_dbAdapter = new SQLiteDataAdapter(i_dbCommand);
                DataTable i_dataTable = new DataTable();
                i_dbAdapter.Fill(i_dataTable);
                List<string[,]> list = new List<string[,]>();
                int itemNum = 0;
                foreach (DataRow row in i_dataTable.Rows)
                {

                    string itemQuantity = Convert.ToString(row["itemQuantity"]);
                    string itemDescripton = Convert.ToString(row["orderNumber"]);

                    
                    string[ , ] s = { { itemDescripton }, { itemQuantity } };
                  
                    list.Add(s);
                    i_dbConnection.Close();
                }
                return list;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<CustomerOrder> getOrders()
        {
            try
            {
                SQLiteConnection i_dbConnection = new SQLiteConnection(databasePath);
                SQLiteCommand i_dbCommand = new SQLiteCommand("SELECT DISTINCT orderNumber,date,customerName,Orders.customerPIB,customerAdress,itemTotal FROM Orders JOIN Customer WHERE Orders.customerPIB == Customer.customerPIB GROUP BY orderNumber", i_dbConnection);
                SQLiteDataAdapter i_dbAdapter = new SQLiteDataAdapter(i_dbCommand);
                DataTable i_dataTable = new DataTable();
                i_dbAdapter.Fill(i_dataTable);
                var orders = new List<CustomerOrder>();
                int itemNum = 0;
                foreach (DataRow row in i_dataTable.Rows)
                {
                    var ob = new Customer()
                    {
                        customerName = Convert.ToString(row["customerName"]),
                        customerAdress = Convert.ToString(row["customerAdress"]),
                        customerPIB = Convert.ToInt32(row["customerPIB"])
                  
                    };
                        
                    var obj = new Order()
                    {
                       date = Convert.ToString(row["date"]),
                       orderNumber = Convert.ToInt32(row["orderNumber"]),
                       itemTotal = Convert.ToInt32(row["itemTotal"])                     
                    };
                    

                    CustomerOrder co = new CustomerOrder();

                    co.C = ob;
                    co.O = obj;

                    orders.Add(co);
                    i_dbConnection.Close();
                }
                return orders;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public int addItem(Order item)
        {
            try
            {
                const string query = "INSERT INTO Orders(itemDescription,itemQuantity,itemTotal,customerPIB,orderNumber,date) VALUES (@itemDescription,@itemQuantity,@itemTotal,@customerPIB,@orderNumber,@date);";
                var args = new Dictionary<string, object>
                {                
                    {"@itemDescription",item.itemDescription},
                    {"@itemQuantity",item.itemQuantity},
                    {"@customerPIB",item.customerPIB},
                    {"@itemTotal",item.itemTotal},
                    {"@orderNumber",item.orderNumber},
                    {"@date",item.date },
                };
                return ExecuteWrite(query, args);
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        public int addFarmer(Farmer farmer)
        {
            try
            {
                const string query = "INSERT INTO Farmer(farmerBPG,farmerName,farmerAdress,accountNumber,farmerPassword) VALUES (@farmerBPG,@farmerName,@farmerAdress,@accountNumber,@farmerPassword);";
                var args = new Dictionary<string, object>
                {
                    {"@farmerBPG",farmer.farmerBPG},
                    {"@farmerName",farmer.farmerName},
                    {"@farmerAdress",farmer.farmerAdress},
                    {"@accountNumber",farmer.accountNumber},
                    {"@farmerPassword",farmer.farmerPassword}
                };
                return ExecuteWrite(query, args);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public int addCustomer(Customer customer)
        {
            try
            {
                const string query = "INSERT INTO Customer(customerName,customerAdress,customerPIB) VALUES (@customerName,@customerAdress,@customerPIB);";
                var args = new Dictionary<string, object>
                {
                    {"@customerName",customer.customerName},
                    {"@customerAdress",customer.customerAdress},
                    {"@customerPIB",customer.customerPIB},
                };
                return ExecuteWrite(query, args);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public int addProduct(Order item)
        {
            try
            {
                const string query = "INSERT INTO Item(itemDescription,itemPrice) VALUES (@itemDescription,@itemPrice);";
                var args = new Dictionary<string, object>
                {
                    {"@itemDescription",item.itemDescription},
                    {"@itemPrice",item.itemPrice},
                };
                return ExecuteWrite(query, args);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public int ExecuteWrite(string query,Dictionary<string,object> args)
        {
            int numOfRowsAffected;

            using (var connection = new SQLiteConnection(databasePath))
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

        public void DeleteProduct(Item item)
        {
            const string query = "Delete from item where itemDescription = @itemDescription";
            var args = new Dictionary<string, object>
            {
                {"@itemDescription",item.LitemDescription},
            };
            ExecuteWrite(query, args);
        }

        public void DeleteCustomer(Customer customer)
        {
            const string query = "Delete from customer where customerPIB = @customerPIB";
            var args = new Dictionary<string, object>
            {
                {"@customerPIB",customer.customerPIB},
            };
            ExecuteWrite(query, args);
        }

        public void UpdateCustomer(Customer customer)
        {
            const string query = "UPDATE customer SET customerName = @customerName, customerAdress = @customerAdress WHERE customerPIB = @customerPIB";

            var args = new Dictionary<string, object>
            {
                {"@customerName", customer.customerName},
                {"@customerAdress", customer.customerAdress},
                {"@customerPIB", customer.customerPIB},
            };

            ExecuteWrite(query, args);
        }

        public void UpdateProduct(Item item)
        {

            const string query = "UPDATE item SET itemDescription = @itemDescription,itemPrice = @itemPrice where itemDescription = @itemDescription";
            var args = new Dictionary<string, object>
            {
                {"@itemDescription",item.LitemDescription},
                {"@itemPrice",item.LitemPrice},
            };
            ExecuteWrite(query, args);
        }
    }
}
