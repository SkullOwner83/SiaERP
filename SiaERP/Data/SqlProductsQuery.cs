using MySql.Data.MySqlClient;
using SiaERP.Models;
using SiaERP.Resources.Utilities;
using System;
using System.Collections.ObjectModel;
using System.Windows.Media.Imaging;

namespace SiaERP.Data
{
    internal class SqlProductsQuery : SqlDatabase
    {
        //Define variables
        private ObservableCollection<Product> ListProducts;

        //Constructor method
        public SqlProductsQuery()
        {
            ListProducts = new ObservableCollection<Product>();
            PrimaryKey = "idProduct";
        }

        //Extract objects from database
        internal ObservableCollection<Product> Read(string Filter = "")
        {
            string Query = "SELECT * FROM Products ";

            //Display results matching the filter
            if (Filter != string.Empty && Filter != null)
            {
                Query += $"WHERE idCustomer LIKE '%{Filter}%' OR Name LIKE '%{Filter}%' OR PhoneNumber LIKE '%{Filter}%' OR DATE_FORMAT(RegisterDate, '%d/%m/%Y') LIKE '%{Filter}%'";
            }

            using (Connection = GetConnection())
            {
                using (MySqlCommand Command = new MySqlCommand(Query, Connection))
                {
                    Connection.Open();
                    MySqlDataReader Reader = Command.ExecuteReader();

                    while (Reader.Read())
                    {
                        byte[] ImageData = Reader["Image"] is DBNull ? null : (byte[])Reader["Image"];
                        BitmapImage CustomerImage = Function.BytesToBitmapImage(ImageData);

                        //Create one instance with data obtained of conexion and save in list users
                        ListProducts.Add(new Product()
                        {
                            Id = (int)Reader["idProduct"],
                            Type = (int)Reader["Type"],
                            Category = (int)Reader["Category"],
                            Supplier = (int)Reader["Supplier"],
                            Name = (string)Reader["Name"],
                            Brand = Reader["Brand"] is DBNull ? null : (string)Reader["Brand"],
                            Description = Reader["Description"] is DBNull ? null : (string)Reader["Description"],
                            Code = Reader["Code"] is DBNull ? null : (string)Reader["Code"],
                            BuyPrice = (decimal)Reader["BuyPrice"],
                            SalePrice = (decimal)Reader["SalePrice"],
                            Stock = (int)Reader["Stock"],
                            MinStock = (int)Reader["MinStock"],
                            Image = CustomerImage
                        });
                    }

                    Reader.Close();
                    Connection.Close();
                }
            }

            return ListProducts;
        }

        //Add register in table of data base
        internal void Create(Product Model)
        {
            string Query = "INSERT INTO Products VALUES(@id, @type, @category, @supplier, @name, @brand, @description, @code, @buyprice, @saleprice, @stock, @minstock, @image);";
            byte[] ImageData = Function.BitmapImageToBytes(Model.Image);

            using (Connection = GetConnection())
            {
                using (MySqlCommand Command = new MySqlCommand(Query, Connection))
                {
                    //Add properties of parameter model to the query string
                    Connection.Open();
                    Command.Parameters.AddWithValue("@id", Model.Id);
                    Command.Parameters.AddWithValue("@type", Model.Type);
                    Command.Parameters.AddWithValue("@category", Model.Category);
                    Command.Parameters.AddWithValue("@supplier", Model.Supplier);
                    Command.Parameters.AddWithValue("@name", Model.Name);
                    Command.Parameters.AddWithValue("@brand", Model.Brand);
                    Command.Parameters.AddWithValue("@description", Model.Description);
                    Command.Parameters.AddWithValue("@code", Model.Code);
                    Command.Parameters.AddWithValue("@buyprice", Model.BuyPrice);
                    Command.Parameters.AddWithValue("@saleprice", Model.SalePrice);
                    Command.Parameters.AddWithValue("@stock", Model.Stock);
                    Command.Parameters.AddWithValue("@minstock", Model.MinStock);
                    Command.Parameters.AddWithValue("@image", ImageData);
                    Command.ExecuteNonQuery();
                    Connection.Close();
                }
            }
        }

        //Modify register in table of data base
        internal void Update(Product Model)
        {
            string Query = "UPDATE Products SET Type=@type, Category=@category, Supplier=@supplier, Name=@name, Brand=@brand, Description=@description, Code=@code, BuyPrice=@buyprice, SalePrice=@saleprice, Stock=@stock, MinStock=@minstock, Image=@image WHERE idProduct = @id";
            byte[] ImageData = Function.BitmapImageToBytes(Model.Image);

            using (Connection = GetConnection())
            {
                using (MySqlCommand Command = new MySqlCommand(Query, Connection))
                {
                    Connection.Open();
                    Command.Parameters.AddWithValue("@id", Model.Id);
                    Command.Parameters.AddWithValue("@type", Model.Type);
                    Command.Parameters.AddWithValue("@category", Model.Category);
                    Command.Parameters.AddWithValue("@supplier", Model.Supplier);
                    Command.Parameters.AddWithValue("@name", Model.Name);
                    Command.Parameters.AddWithValue("@brand", Model.Brand);
                    Command.Parameters.AddWithValue("@description", Model.Description);
                    Command.Parameters.AddWithValue("@code", Model.Code);
                    Command.Parameters.AddWithValue("@buyprice", Model.BuyPrice);
                    Command.Parameters.AddWithValue("@saleprice", Model.SalePrice);
                    Command.Parameters.AddWithValue("@stock", Model.Stock);
                    Command.Parameters.AddWithValue("@minstock", Model.MinStock);
                    Command.Parameters.AddWithValue("@image", ImageData);
                    Command.ExecuteNonQuery();
                    Connection.Close();
                }
            }
        }

        //Delete register in table of data base
        internal void Delete(Product Model)
        {
            string Query = "DELETE FROM Products WHERE idProduct = @id";

            using (Connection = GetConnection())
            {
                using (MySqlCommand Command = new MySqlCommand(Query, Connection))
                {
                    Connection.Open();
                    Command.Parameters.AddWithValue("@id", Model.Id);
                    Command.ExecuteNonQuery();
                    Connection.Close();
                }
            }
        }        
    }
}
