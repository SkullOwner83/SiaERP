using System.Collections.ObjectModel;
using MySql.Data.MySqlClient;
using SiaERP.Models;

namespace SiaERP.Data
{
    internal class SQLDatabase
	{
		private string Server = "localhost";
		private string Database = "SiaDatabase";
		private string User = "SkullOwner";
		private string Password = "Skull Owner83";
		public readonly string Connection;


		public SQLDatabase()
		{
			//connection = @"Server=localhost; Database=SiaDatabase; Trusted_Connection=true;";
			Connection = $"Server={Server}; Database={Database}; User Id={User}; Password={Password};";
		}

		//Extract objects from database of users type
		internal ObservableCollection<User> Get()
		{
			//Create object list from save registers and create sql query
			ObservableCollection<User> ListUsers = new ObservableCollection<User>();
			string Query = "SELECT * FROM Users";

			//Create new conection with sql
			using(MySqlConnection CurrentConnection = new MySqlConnection(Connection))
			{
				CurrentConnection.Open();
				MySqlCommand Command = new MySqlCommand(Query, CurrentConnection);
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
				CurrentConnection.Close();
			}

			return ListUsers;
		}

		//Add register in table of data base
		internal void Add(User Model)
		{
			string Query = "INSERT INTO Users VALUES(@id, @name, @userName, @password, @accountType, @numberPhone);";

			using(MySqlConnection CurrentConnection = new MySqlConnection(Connection))
			{
				CurrentConnection.Open();
				MySqlCommand Command = new MySqlCommand(Query, CurrentConnection);
				Command.Parameters.AddWithValue("@id", Model.Id);
				Command.Parameters.AddWithValue("@name", Model.Name);
				Command.Parameters.AddWithValue("@userName", Model.UserName);
				Command.Parameters.AddWithValue("@password", Model.Password);
				Command.Parameters.AddWithValue("@accountType", Model.AccountType);
				Command.Parameters.AddWithValue("@numberPhone", Model.NumberPhone);
				Command.ExecuteNonQuery();
				CurrentConnection.Close();
			}
		}

		//Delete register in table of data base
		internal void Delete(User Model)
		{
			string Query = "DELETE FROM Users WHERE idUser = @id";

			using(MySqlConnection CurrentConnection = new MySqlConnection(Connection))
			{
				CurrentConnection.Open();
				MySqlCommand Command = new MySqlCommand(Query, CurrentConnection);
				Command.Parameters.AddWithValue("@id", Model.Id);
				Command.ExecuteNonQuery();
				CurrentConnection.Close();
			}
		}

		//Modify register in table of data base
		internal void Modify(User Model)
		{
			string Query = "UPDATE Users SET Name=@name, UserName=@userName, Password=@password, AccountType=@accountType, NumberPhone=@numberPhone";

			using(MySqlConnection CurrentConnection = new MySqlConnection(Connection))
			{
				CurrentConnection.Open();
				MySqlCommand Command = new MySqlCommand(Query, CurrentConnection);
				Command.Parameters.AddWithValue("@id", Model.Id);
				Command.Parameters.AddWithValue("@name", Model.Name);
				Command.Parameters.AddWithValue("@userName", Model.UserName);
				Command.Parameters.AddWithValue("@password", Model.Password);
				Command.Parameters.AddWithValue("@accountType", Model.AccountType);
				Command.Parameters.AddWithValue("@numberPhone", Model.NumberPhone);
				Command.ExecuteNonQuery();
				CurrentConnection.Close();
			}
		}
	}
}
