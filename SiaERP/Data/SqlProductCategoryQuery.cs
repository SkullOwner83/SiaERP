using MySql.Data.MySqlClient;
using System;
using SiaERP.Models;
using System.Collections.ObjectModel;

namespace SiaERP.Data
{
	internal class SqlProductCategoryQuery : SqlDatabase
	{
		//Define variables
		private ObservableCollection<ProductCategory> ListCategory;

		//Constructor method
		public SqlProductCategoryQuery()
		{
			ListCategory = new ObservableCollection<ProductCategory>();
            PrimaryKey = "idCategory";
        }

		//Extract objects from database
		internal ObservableCollection<ProductCategory> Read(string Filter = "")
		{
			string Query = "SELECT * FROM ProductCategory ";

			using (Connection = GetConnection())
			{
				using (MySqlCommand Command = new MySqlCommand(Query, Connection))
				{
					Connection.Open();
					MySqlDataReader Reader = Command.ExecuteReader();

					while (Reader.Read())
					{
                        ListCategory.Add(new ProductCategory()
						{
							Id = (int)Reader["idCategory"],
							CategoryName = (string)Reader["CategoryName"]
						});
					}

					Reader.Close();
					Connection.Close();
				}
			}

			return ListCategory;
		}

		//Add register in table of data base
		internal void Create(ProductCategory Model)
		{
			string Query = "INSERT INTO ProductCategory VALUES(@id, @categoryname);";

			using (Connection = GetConnection())
			{
				using (MySqlCommand Command = new MySqlCommand(Query, Connection))
				{
					//Add properties of parameter model to the query string
					Connection.Open();
					Command.Parameters.AddWithValue("@id", Model.Id);
					Command.Parameters.AddWithValue("@categoryname", Model.CategoryName);
					Command.ExecuteNonQuery();
					Connection.Close();
				}
			}
		}

		//Modify register in table of data base
		internal void Update(ProductCategory Model)
		{
			string Query = "UPDATE ProductCategory SET @categoryname WHERE idProduct = @id";

			using (Connection = GetConnection())
			{
				using (MySqlCommand Command = new MySqlCommand(Query, Connection))
				{
					Connection.Open();
					Command.Parameters.AddWithValue("@id", Model.Id);
					Command.Parameters.AddWithValue("@categoryname", Model.CategoryName);
					Command.ExecuteNonQuery();
					Connection.Close();
				}
			}
		}

		//Delete register in table of data base
		internal void Delete(ProductCategory Model)
		{
			string Query = "DELETE FROM ProductCategory WHERE idCategory = @id";

			using (Connection = GetConnection())
			{
				using (MySqlCommand Command = new MySqlCommand(Query, Connection))
				{
					Connection.Open();
					Command.Parameters.AddWithValue("@id", Model.Id);
					Command.Parameters.AddWithValue("@categoryname", Model.CategoryName);
					Command.ExecuteNonQuery();
					Connection.Close();
				}
			}
		}

		//Get the id of the last record in the database
		internal int LastId()
		{
			string Query = "SELECT MAX(idProductCategory) From Products";
			int LastId = 0;

			using (Connection = GetConnection())
			{
				using (MySqlCommand Command = new MySqlCommand(Query, Connection))
				{
					Connection.Open();
					object Resullt = Command.ExecuteScalar();

					if (Resullt != null && Resullt != DBNull.Value)
					{
						LastId = Convert.ToInt32(Resullt);
					}
				}

			}

			return LastId;
		}
	}
}
