using System.Collections.ObjectModel;
using System.Data;
using System.Net;
using MySql.Data.MySqlClient;
using SiaERP.Models;

namespace SiaERP.Data
{
	internal class SqlUserQuery : SqlDatabase
	{
		//Define variables
		private ObservableCollection<User> ListUsers;

		//Constructor method
		public SqlUserQuery()
		{
			ListUsers = new ObservableCollection<User>();
            PrimaryKey = "idUser";
        }

		//Extract objects from database of users type
		internal ObservableCollection<User> Get()
		{
			string Query = "SELECT * FROM Users ";

			using(Connection = GetConnection())
			{
				using (MySqlCommand Command = new MySqlCommand(Query, Connection))
				{
					Connection.Open();
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
                    Connection.Close();
				}
			}

			return ListUsers;
		}

		//Add register in table of data base
		internal void Add(User Model)
		{
			string Query = "INSERT INTO Users VALUES(@id, @name, @userName, @password, @accountType, @numberPhone);";

			using(Connection = GetConnection())
			{
                Connection.Open();
				MySqlCommand Command = new MySqlCommand(Query, Connection);
				Command.Parameters.AddWithValue("@id", Model.Id);
				Command.Parameters.AddWithValue("@name", Model.Name);
				Command.Parameters.AddWithValue("@userName", Model.UserName);
				Command.Parameters.AddWithValue("@password", Model.Password);
				Command.Parameters.AddWithValue("@accountType", Model.AccountType);
				Command.Parameters.AddWithValue("@numberPhone", Model.NumberPhone);
				Command.ExecuteNonQuery();
                Connection.Close();
			}
		}

		//Delete register in table of data base
		internal void Delete(User Model)
		{
			string Query = "DELETE FROM Users WHERE idUser = @id";

			using(Connection = GetConnection())
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

		//Modify register in table of data base
		internal void Modify(User Model)
		{
			string Query = "UPDATE Users SET Name=@name, UserName=@userName, Password=@password, AccountType=@accountType, NumberPhone=@numberPhone";

			using(Connection = GetConnection())
			{
				using (MySqlCommand Command = new MySqlCommand(Query, Connection))
				{
                    Connection.Open();
                    Command.Parameters.AddWithValue("@id", Model.Id);
					Command.Parameters.AddWithValue("@name", Model.Name);
					Command.Parameters.AddWithValue("@userName", Model.UserName);
					Command.Parameters.AddWithValue("@password", Model.Password);
					Command.Parameters.AddWithValue("@accountType", Model.AccountType);
					Command.Parameters.AddWithValue("@numberPhone", Model.NumberPhone);
					Command.ExecuteNonQuery();
					Connection.Close();
				}
			}
		}

		//Authenticate user credentials
		internal bool AuthenticateUser(NetworkCredential Credential)
		{
			bool ValidUser;
			string Query = "SELECT * FROM Users WHERE UserName=@UserName and Password=@Password";

			using(Connection = GetConnection())
			{
				using (MySqlCommand Command = new MySqlCommand(Query, Connection))
				{
                    Connection.Open();
                    Command.Parameters.Add("@UserName", MySqlDbType.VarChar).Value = Credential.UserName;
					Command.Parameters.Add("@Password", MySqlDbType.VarChar).Value = Credential.Password;
					ValidUser = Command.ExecuteScalar() == null ? false : true;
                    Connection.Close();
				}
			}

			return ValidUser;
		}
	}
}
