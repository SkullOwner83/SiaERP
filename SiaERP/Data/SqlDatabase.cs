﻿using System.Windows;
using MySql.Data.MySqlClient;
using System;

namespace SiaERP.Data
{
    internal abstract class SqlDatabase
	{
		//Define Variables
		private string Server = "localhost";
		private string Database = "SiaDatabase";
		private string User = "root";
		private string Password = "Skull Owner83";
		private readonly string ConnectionString;
		public MySqlConnection Connection;
        public string PrimaryKey;
        public string Table;

		//Constructor method
		public SqlDatabase()
		{
			ConnectionString = $"Server={Server}; Database={Database}; User Id={User}; Password={Password};";
		}

		//Check if can get connection with MySQL and return this connection
		public MySqlConnection GetConnection()
		{
            return new MySqlConnection(ConnectionString);
        }

        //Get the id of the last record in the database
        public int LastId()
        {
            string Query = $"SELECT MAX({PrimaryKey}) From {Table}";
            int LastId = 0;

            using (Connection = GetConnection())
            {
                using (MySqlCommand Command = new MySqlCommand(Query, Connection))
                {
                    Connection.Open();
                    object Result = Command.ExecuteScalar();

                    if (Result != null && Result != DBNull.Value)
                    {
                        LastId = Convert.ToInt32(Result);
                    }
                }

            }

            return LastId;
        }
    }
}
