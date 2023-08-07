using MySql.Data.MySqlClient;
using SiaERP.Models;
using System;
using System.Collections.ObjectModel;

namespace SiaERP.Data
{
    internal class SqlServiceQuery : SqlDatabase
    {
        //Define variables
        private ObservableCollection<Service> ListServices;

        //Constructor method
        public SqlServiceQuery()
        {
            ListServices = new ObservableCollection<Service>();
            PrimaryKey = "idService";
            Table = "Services";
        }

        //Extract objects from database
        internal ObservableCollection<Service> Read(string Filter = "")
        {
            string Query = "SELECT s.*, c.Name FROM Services s " +
                           "JOIN Customers c ON s.idCustomer = c.idCustomer";
   
            using (Connection = GetConnection())
            {
                using (MySqlCommand Command = new MySqlCommand(Query, Connection))
                {
                    Connection.Open();
                    MySqlDataReader Reader = Command.ExecuteReader();

                    while (Reader.Read())
                    {
                        //Create one instance with data obtained of conexion and save in list users
                        ListServices.Add(new Service()
                        {
                            Id = (int)Reader["idService"],
                            IdCustomer = (int)Reader["idCustomer"],
                            CustomerName = (string)Reader["Name"],
                            AdmissionDate = (DateTime)Reader["AdmissionDate"],
                            DeliveryDate = (DateTime)Reader["DeliveryDate"],
                            Status = (int)Reader["Status"],
                            Diagnostic = Reader["Diagnostic"] is DBNull ? null : (string)Reader["Diagnostic"]
                        });
                    }

                    Reader.Close();
                    Connection.Close();
                }
            }

            return ListServices;
        }

        //Add register in table of data base
        internal void Create(Service Model)
        {
            string Query = "INSERT INTO Services VALUES(@id, @customer, @admissiondate, @deliverydate, @status, @diagnostic);";

            using (Connection = GetConnection())
            {
                using (MySqlCommand Command = new MySqlCommand(Query, Connection))
                {
                    //Add properties of parameter model to the query string
                    Connection.Open();
                    Command.Parameters.AddWithValue("@id", Model.Id);
                    Command.Parameters.AddWithValue("@customer", Model.IdCustomer);
                    Command.Parameters.AddWithValue("@admissiondate", Model.AdmissionDate);
                    Command.Parameters.AddWithValue("@deliverydate", Model.DeliveryDate);
                    Command.Parameters.AddWithValue("@status", Model.Status);
                    Command.Parameters.AddWithValue("@diagnostic", Model.Diagnostic);
                    Command.ExecuteNonQuery();
                    Connection.Close();
                }
            }
        }

        //Modify register in table of data base
        internal void Update(Service Model)
        {
            string Query = "UPDATE Services SET idCustomer=@idcustomer, AdmissionDate=@admissiondate, DeliveryDate=@deliverydate, Status=@status, Diagnostic=@diagnostic WHERE idService = @id";

            using (Connection = GetConnection())
            {
                using (MySqlCommand Command = new MySqlCommand(Query, Connection))
                {
                    //Add properties of parameter model to the query string
                    Connection.Open();
                    Command.Parameters.AddWithValue("@id", Model.Id);
                    Command.Parameters.AddWithValue("@idcustomer", Model.IdCustomer);
                    Command.Parameters.AddWithValue("@admissiondate", Model.AdmissionDate);
                    Command.Parameters.AddWithValue("@deliverydate", Model.DeliveryDate);
                    Command.Parameters.AddWithValue("@status", Model.Status);
                    Command.Parameters.AddWithValue("@diagnostic", Model.Diagnostic);
                    Command.ExecuteNonQuery();
                    Connection.Close();
                }
            }
        }

        //Delete register in table of data base
        internal void Delete(Service Model)
        {
            string Query = "DELETE FROM Services WHERE idService = @id";

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
