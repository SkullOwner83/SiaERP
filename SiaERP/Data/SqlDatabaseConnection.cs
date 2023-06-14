using System.Windows;
using MySql.Data.MySqlClient;
using System;

namespace SiaERP.Data
{
    internal class SqlDatabaseConnection
	{
		//Define Variables
		private string Server = "localhost";
		private string Database = "SiaDatabases";
		private string User = "SkullOwner";
		private string Password = "Skull Owner83";
		private readonly string ConnectionString;
		private MySqlConnection Connection;

		//Constructor method
		public SqlDatabaseConnection()
		{
			ConnectionString = $"Server={Server}; Database={Database}; User Id={User}; Password={Password};";
			Connection = new MySqlConnection(ConnectionString);
		}

		//Check if can get connection with MySQL and return this connection
		public MySqlConnection? GetConnection()
		{
			try
			{
				if (Connection.State != System.Data.ConnectionState.Open)
				{
					Connection.Open();
				}

				return Connection;
			}
			catch (Exception e)
			{
				//MessageBox.Show(e.ToString());
				return null;
			}
		}
	}
}
