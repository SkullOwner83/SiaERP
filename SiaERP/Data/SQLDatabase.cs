using System.Collections.ObjectModel;
using SiaERP.Models;
using System.Data.SqlClient;

namespace SiaERP.Data
{
    internal class SQLDatabase
    {
		private readonly string connection;
		public string Connection => connection;

		public SQLDatabase()
		{
			connection = @"Server=localhost; Database=SiaDatavase; Trusted_Connection=true;";
		}

		//Extract objects from database of users type
		internal ObservableCollection<User> Get()
		{
			//Create object list from save registers and create sql query
			ObservableCollection<User> ListUsers = new ObservableCollection<User>();
			string Query = "SELECT * FROM Users";

			//Create new conection with sql
			using(SqlConnection CurrentConnection = new SqlConnection(Connection))
			{
				CurrentConnection.Open();
				SqlCommand Command = new SqlCommand(Query, CurrentConnection);
				SqlDataReader Reader = Command.ExecuteReader();

				while(Reader.Read())
				{
					//Create one instance with data obtained of conexion and save in list users
					ListUsers.Add(new User()
					{
						Id = (int)Reader["idUser"],
						Name = (string)Reader["Name"],
						UserName = (string)Reader["UserName"],
						Password = (string)Reader["Password"],
						AccountType = (string)Reader["AccounType"],
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

			using(SqlConnection CurrentConnection = new SqlConnection(Connection))
			{
				CurrentConnection.Open();
				SqlCommand Command = new SqlCommand(Query, CurrentConnection);
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

			using(SqlConnection CurrentConnection = new SqlConnection(Connection))
			{
				CurrentConnection.Open();
				SqlCommand Command = new SqlCommand(Query, CurrentConnection);
				Command.Parameters.AddWithValue("@id", Model.Id);
				Command.ExecuteNonQuery();
				CurrentConnection.Close();
			}
		}

		//Modify register in table of data base
		internal void Modify(User Model)
		{
			string Query = "UPDATE Users SET Name=@name, UserName=@userName, Password=@password, AccountType=@accountType, NumberPhone=@numberPhone";

			using(SqlConnection CurrentConnection = new SqlConnection(Connection))
			{
				CurrentConnection.Open();
				SqlCommand Command = new SqlCommand(Query, CurrentConnection);
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
