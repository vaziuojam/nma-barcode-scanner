using System.Collections.Generic;
using Microsoft.Data.Sqlite;

namespace BarcodeScanner
{
    public class DatabaseProductCatalog : IProductCatalog
    {
        private const string ConnectionString = "DataSource=products.db";
        
            
        public static void CreateDatabase()
        {
            using (var connection = new SqliteConnection(ConnectionString))
            {
                connection.Open();

                using (var command = new SqliteCommand("CREATE TABLE Products (Barcode PRIMARY KEY, Name, Price)", connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }

        public void SaveProducts(IEnumerable<Product> products)
        {
            using (var connection = new SqliteConnection(ConnectionString))
            {
                connection.Open();

                foreach (var product in products)
                {
                    using (var command = new SqliteCommand("INSERT INTO Products(Barcode, Name, Price) VALUES(@barcode, @name, @price)", connection))
                    {
                        command.Parameters.AddWithValue("@barcode", product.Barcode);
                        command.Parameters.AddWithValue("@name", product.Name);
                        command.Parameters.AddWithValue("@price", product.Price);
                        command.ExecuteNonQuery();
                    }
                }
            }
        }

        public Product FindByBarcode(string barcode)
        {
            using (var connection = new SqliteConnection(ConnectionString))
            {
                connection.Open();
                using (var command = new SqliteCommand("SELECT Barcode, Name, Price FROM Products WHERE Barcode = @barcode", connection))
                {
                    command.Parameters.AddWithValue("@barcode", barcode);
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Product
                            {
                                Barcode = reader.GetString(0),
                                Name = reader.GetString(1),
                                Price = reader.GetDecimal(2)
                            };
                        }

                        return null;
                    }
                }
            }
        }
    }
}