using System.Collections.ObjectModel;
using MySql.Data.MySqlClient;
using SiaERP.Models;

namespace SiaERP.Data
{
	internal class SQLUserQuery
	{
		//Define variables
		private SqlDatabaseConnection Connection;
		private ObservableCollection<User> ListUsers;

		//Constructor method
		public SQLUserQuery()
		{
			Connection = new SqlDatabaseConnection();
			ListUsers = new ObservableCollection<User>();
		}

		//Extract objects from database of users type
		internal ObservableCollection<User> Get()
		{
			string Query = "SELECT * FROM Users ";

			using(MySqlConnection ConnectionOpended = Connection.GetConnection())
			{
				MySqlCommand Command = new MySqlCommand(Query, ConnectionOpended);
				MySqlDataReader Reader = Command.ExecuteReader();

				while(Reader.Read())
				{
					//Create one instance with data obtained of conexion and save in list users
					ListUsers.Add(new User()
					{
						Id = (int)Reader["idUser"],
						Name = (string)Reader["Name"],
						UserName = (string)Reader["UserName"],
						Password = (string)Reader["Password"],
						AccountType = (string)Reader["AccountType"],
						NumberPhone = (string)Reader["NumberPhone"]
					});
				}

				Reader.Close();
				ConnectionOpended.Close();
			}

			return ListUsers;
		}

		//Add register in table of data base
		internal void Add(User Model)
		{
			string Query = "INSERT INTO Users VALUES(@id, @name, @userName, @password, @accountType, @numberPhone);";

			using(MySqlConnection ConnectionOpended = Connection.GetConnection())
			{
				ConnectionOpended.Open();
				MySqlCommand Command = new MySqlCommand(Query, ConnectionOpended);
				Command.Parameters.AddWithValue("@id", Model.Id);
				Command.Parameters.AddWithValue("@name", Model.Name);
				Command.Parameters.AddWithValue("@userName", Model.UserName);
				Command.Parameters.AddWithValue("@password", Model.Password);
				Command.Parameters.AddWithValue("@accountType", Model.AccountType);
				Command.Parameters.AddWithValue("@numberPhone", Model.NumberPhone);
				Command.ExecuteNonQuery();
				ConnectionOpended.Close();
			}
		}

		//Delete register in table of data base
		internal void Delete(User Model)
		{
			string Query = "DELETE FROM Users WHERE idUser = @id";

			using(MySqlConnection ConnectionOpended = Connection.GetConnection())
			{
				ConnectionOpended.Open();
				MySqlCommand Command = new MySqlCommand(Query, ConnectionOpended);
				Command.Parameters.AddWithValue("@id", Model.Id);
				Command.ExecuteNonQuery();
				ConnectionOpended.Close();
			}
		}

		//Modify register in table of data base
		internal void Modify(User Model)
		{
			string Query = "UPDATE Users SET Name=@name, UserName=@userName, Password=@password, AccountType=@accountType, NumberPhone=@numberPhone";

			using(MySqlConnection ConnectionOpended = Connection.GetConnection())
			{
				ConnectionOpended.Open();
				MySqlCommand Command = new MySqlCommand(Query, ConnectionOpended);
				Command.Parameters.AddWithValue("@id", Model.Id);
				Command.Parameters.AddWithValue("@name", Model.Name);
				Command.Parameters.AddWithValue("@userName", Model.UserName);
				Command.Parameters.AddWithValue("@password", Model.Password);
				Command.Parameters.AddWithValue("@accountType", Model.AccountType);
				Command.Parameters.AddWithValue("@numberPhone", Model.NumberPhone);
				Command.ExecuteNonQuery();
				ConnectionOpended.Close();
			}
		}
	}
}
