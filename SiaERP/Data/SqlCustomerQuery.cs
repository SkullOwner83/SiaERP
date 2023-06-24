using Models;
using MySql.Data.MySqlClient;
using SiaERP.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

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
        internal ObservableCollection<Customer> Get()
        {
            string Query = "SELECT * FROM Customers ";

            using (MySqlConnection ConnectionOpended = Connection.GetConnection())
            {
                MySqlCommand Command = new MySqlCommand(Query, ConnectionOpended);
                MySqlDataReader Reader = Command.ExecuteReader();

                while (Reader.Read())
                {
                    //Create one instance with data obtained of conexion and save in list users
                    ListCustomers.Add(new Customer()
                    {
                        Id = (int)Reader["idCustomer"],
                        Type = (string)Reader["Type"],
                        Name = (string)Reader["Name"],
                        RFC = Reader["RFC"] is DBNull ? null : (string)Reader["RFC"],
                        PhoneNumber = Reader["PhoneNumber"] is DBNull ? null : (string)Reader["PhoneNumber"],
                        Email = Reader["Email"] is DBNull ? null : (string)Reader["Email"],
                        Adress = Reader["Adress"] is DBNull ? null : (string)Reader["Adress"],
                        City = Reader["City"] is DBNull ? null : (string)Reader["City"],
                        State = Reader["State"] is DBNull ? null : (string)Reader["State"],
                        Country = Reader["Country"] is DBNull ? null : (string)Reader["Country"],
                        PostalCode = Reader["PostalCode"] is DBNull ? null : (string)Reader["PostalCode"],
                        TaxRegime = Reader["TaxRegime"] is DBNull ? null : (string)Reader["TaxRegime"]
                    });
                }

                Reader.Close();
                ConnectionOpended.Close();
            }

            return ListCustomers;
        }

        //Add register in table of data base
        internal void Add(Customer Model)
        {
            string Query = "INSERT INTO Customers VALUES(@id, @type, @name, @rfc, @phonenumber, @email, @adress, @city, @state, @country, @postalcode, @taxregime;";

            using (MySqlConnection ConnectionOpended = Connection.GetConnection())
            {
                ConnectionOpended.Open();
                MySqlCommand Command = new MySqlCommand(Query, ConnectionOpended);
                Command.Parameters.AddWithValue("@id", Model.Id);
                Command.Parameters.AddWithValue("@rfc", Model.Name);
                Command.Parameters.AddWithValue("@phonenumber", Model.PhoneNumber);
                Command.Parameters.AddWithValue("@email", Model.Email);
                Command.Parameters.AddWithValue("@adress", Model.Adress);
                Command.Parameters.AddWithValue("@city", Model.City);
                Command.Parameters.AddWithValue("@state", Model.State);
                Command.Parameters.AddWithValue("@country", Model.Country);
                Command.Parameters.AddWithValue("@postalcode", Model.PostalCode);
                Command.Parameters.AddWithValue("@taxregime", Model.TaxRegime);
                //Command.Parameters.AddWithValue("@registerdate", Model.RegisterDate);
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

        //Modify register in table of data base
        internal void Modify(Customer Model)
        {
            string Query = "UPDATE Customers SET Type=@type, Name=@name, RFC=@rfc, PhoneNumber=@phonenumber, Email=@email, Adress=@adress, City=@city, State=@state, Country=@country, PostalCode=@postalcode, TaxRegime=@taxregime;";

            using (MySqlConnection ConnectionOpended = Connection.GetConnection())
            {
                MySqlCommand Command = new MySqlCommand(Query, ConnectionOpended);
                Command.Parameters.AddWithValue("@id", Model.Id);
                Command.Parameters.AddWithValue("@rfc", Model.Name);
                Command.Parameters.AddWithValue("@phonenumber", Model.PhoneNumber);
                Command.Parameters.AddWithValue("@email", Model.Email);
                Command.Parameters.AddWithValue("@adress", Model.Adress);
                Command.Parameters.AddWithValue("@city", Model.City);
                Command.Parameters.AddWithValue("@state", Model.State);
                Command.Parameters.AddWithValue("@country", Model.Country);
                Command.Parameters.AddWithValue("@postalcode", Model.PostalCode);
                Command.Parameters.AddWithValue("@taxregime", Model.TaxRegime);
                //Command.Parameters.AddWithValue("@registerdate", Model.RegisterDate);
                Command.ExecuteNonQuery();
                ConnectionOpended.Close();
            }
        }
    }
}
