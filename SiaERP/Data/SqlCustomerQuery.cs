using Models;
using MySql.Data.MySqlClient;
using System.Collections.ObjectModel;
using System;
using MySqlX.XDevAPI.Common;
using SiaERP.Resources.Utilities;
using System.Windows.Media.Imaging;

namespace SiaERP.Data
{
    internal class SqlCustomerQuery
    {
        //Define variables
        private SqlDatabaseConnection Connection;
        private ObservableCollection<Customer> ListCustomers;

        //Constructor method
        public SqlCustomerQuery()
        {
            Connection = new SqlDatabaseConnection();
            ListCustomers = new ObservableCollection<Customer>();
        }

        //Extract objects from database of users type
        internal ObservableCollection<Customer> Read(string Filter = "")
        {
            string Query = "SELECT * FROM Customers ";

            //Display results matching the filter
            if (Filter != string.Empty && Filter != null)
            {
                Query += $"WHERE idCustomer LIKE '%{Filter}%' OR Name LIKE '%{Filter}%' OR PhoneNumber LIKE '%{Filter}%' OR DATE_FORMAT(RegisterDate, '%d/%m/%Y') LIKE '%{Filter}%'";
            }

            using (MySqlConnection ConnectionOpended = Connection.GetConnection())
            {
                MySqlCommand Command = new MySqlCommand(Query, ConnectionOpended);
                MySqlDataReader Reader = Command.ExecuteReader();

                while (Reader.Read())
                {
                    byte[] ImageData = Reader["Image"] is DBNull ? null : (byte[])Reader["Image"];
                    BitmapImage CustomerImage = Function.BytesToBitmapImage(ImageData);

                    //Create one instance with data obtained of conexion and save in list users
                    ListCustomers.Add(new Customer()
                    {
                        Id = (int)Reader["idCustomer"],
                        Type = (int)Reader["Type"],
                        Name = (string)Reader["Name"],
                        RFC = Reader["RFC"] is DBNull ? null : (string)Reader["RFC"],
                        PhoneNumber = Reader["PhoneNumber"] is DBNull ? null : (string)Reader["PhoneNumber"],
                        Email = Reader["Email"] is DBNull ? null : (string)Reader["Email"],
                        Adress = Reader["Adress"] is DBNull ? null : (string)Reader["Adress"],
                        City = Reader["City"] is DBNull ? null : (string)Reader["City"],
                        State = Reader["State"] is DBNull ? null : (string)Reader["State"],
                        Country = Reader["Country"] is DBNull ? null : (string)Reader["Country"],
                        PostalCode = Reader["PostalCode"] is DBNull ? null : (string)Reader["PostalCode"],
                        TaxRegime = Reader["TaxRegime"] is DBNull ? null : (string)Reader["TaxRegime"],
                        RegisterDate = (DateTime)Reader["RegisterDate"],
                        Trusted = (bool)Reader["Trusted"],
                        Image = CustomerImage
                    });
                }

                Reader.Close();
                ConnectionOpended.Close();
            }

            return ListCustomers;
        }

        //Add register in table of data base
        internal void Create(Customer Model)
        {
            string Query = "INSERT INTO Customers VALUES(@id, @type, @name, @rfc, @phonenumber, @email, @adress, @city, @state, @country, @postalcode, @taxregime, @registerdate, @trusted, @image);";
            byte[] ImageData = Function.BitmapImageToBytes(Model.Image);

            using (MySqlConnection ConnectionOpended = Connection.GetConnection())
            {
                //Checar porque la conexion no se cierra para evitar usar el if
                if (ConnectionOpended.State != System.Data.ConnectionState.Open)
                {
                    ConnectionOpended.Open();
                }

                MySqlCommand Command = new MySqlCommand(Query, ConnectionOpended);
                Command.Parameters.AddWithValue("@id", Model.Id);
                Command.Parameters.AddWithValue("@type", Model.Type);
                Command.Parameters.AddWithValue("@name", Model.Name);
                Command.Parameters.AddWithValue("@rfc", Model.RFC);
                Command.Parameters.AddWithValue("@phonenumber", Model.PhoneNumber);
                Command.Parameters.AddWithValue("@email", Model.Email);
                Command.Parameters.AddWithValue("@adress", Model.Adress);
                Command.Parameters.AddWithValue("@city", Model.City);
                Command.Parameters.AddWithValue("@state", Model.State);
                Command.Parameters.AddWithValue("@country", Model.Country);
                Command.Parameters.AddWithValue("@postalcode", Model.PostalCode);
                Command.Parameters.AddWithValue("@taxregime", Model.TaxRegime);
                Command.Parameters.AddWithValue("@registerdate", Model.RegisterDate);
                Command.Parameters.AddWithValue("@trusted", Model.Trusted);
                Command.Parameters.AddWithValue("@image", ImageData);
                Command.ExecuteNonQuery();
                ConnectionOpended.Close();
            }
        }

        //Modify register in table of data base
        internal void Update(Customer Model)
        {
            string Query = "UPDATE Customers SET Type=@type, Name=@name, RFC=@rfc, PhoneNumber=@phonenumber, Email=@email, Adress=@adress, City=@city, State=@state, Country=@country, PostalCode=@postalcode, TaxRegime=@taxregime, RegisterDate=@registerdate, Trusted=@trusted, Image=@image WHERE idCustomer = @id";
            byte[] ImageData = Function.BitmapImageToBytes(Model.Image);

            using (MySqlConnection ConnectionOpended = Connection.GetConnection())
            {
                MySqlCommand Command = new MySqlCommand(Query, ConnectionOpended);
                Command.Parameters.AddWithValue("@id", Model.Id);
                Command.Parameters.AddWithValue("@type", Model.Type);
                Command.Parameters.AddWithValue("@name", Model.Name);
                Command.Parameters.AddWithValue("@rfc", Model.RFC);
                Command.Parameters.AddWithValue("@phonenumber", Model.PhoneNumber);
                Command.Parameters.AddWithValue("@email", Model.Email);
                Command.Parameters.AddWithValue("@adress", Model.Adress);
                Command.Parameters.AddWithValue("@city", Model.City);
                Command.Parameters.AddWithValue("@state", Model.State);
                Command.Parameters.AddWithValue("@country", Model.Country);
                Command.Parameters.AddWithValue("@postalcode", Model.PostalCode);
                Command.Parameters.AddWithValue("@taxregime", Model.TaxRegime);
                Command.Parameters.AddWithValue("@registerdate", Model.RegisterDate);
                Command.Parameters.AddWithValue("@trusted", Model.Trusted);
                Command.Parameters.AddWithValue("@image", ImageData);
                Command.ExecuteNonQuery();
                ConnectionOpended.Close();
            }
        }

        //Delete register in table of data base
        internal void Delete(Customer Model)
        {
            string Query = "DELETE FROM Customers WHERE idCustomer = @id";

            using (MySqlConnection ConnectionOpended = Connection.GetConnection())
            {
                MySqlCommand Command = new MySqlCommand(Query, ConnectionOpended);
                Command.Parameters.AddWithValue("@id", Model.Id);
                Command.ExecuteNonQuery();
                ConnectionOpended.Close();
            }
        }

        internal int LastId()
        {
            string Query = "SELECT MAX(idCustomer) From Customers";
            int LastId = 0;

            using (MySqlConnection ConnectionOpended = Connection.GetConnection())
            {
                if (ConnectionOpended.State != System.Data.ConnectionState.Open)
                {
                    ConnectionOpended.Open();
                }

                MySqlCommand Command = new MySqlCommand(Query, ConnectionOpended);
                object Resullt = Command.ExecuteScalar();

                if (Resullt != null && Resullt != DBNull.Value)
                {
                    LastId = Convert.ToInt32(Resullt);
                }

            }

            return LastId;
        }
    }
}
