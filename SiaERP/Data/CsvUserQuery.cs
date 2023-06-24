using System.Collections.ObjectModel;
using SiaERP.Models;
using System.IO;
using System;
using System.Net;

namespace SiaERP.Data
{
    class CsvUserQuery
    {
		private ObservableCollection<User> ListUsers;

		public CsvUserQuery() 
		{
			ListUsers = new ObservableCollection<User>();
		}

		//Extract objects from database of users type
		internal ObservableCollection<User> Get()
		{
			//Load data if database file exists
			if (File.Exists("UsersData.db"))
			{
				using(StreamReader LoadData = new StreamReader("Database.db"))
				{
					string[] Values = new string[6]; //6 = user columns
					User UserReader = new User();
					int Index = 0;

					//Create one cicle for each read line in file
					while(LoadData.EndOfStream != true)
					{
						string? Line = LoadData.ReadLine(); //Data.DecryptMD5(LoadData.ReadLine());

						foreach (char Digit in Line)
						{
							//Read each character and concatenate them into their property
							if (Digit == ',')
							{
								Index++;
								continue;
							}

							Values[Index] += Digit;
						}
						
						//Assign read values to the object and add in listusers
						UserReader.Id = Convert.ToInt32(Values[0]);
						UserReader.Name = Values[1];
						UserReader.UserName = Values[2];
						UserReader.Password = Values[3];
						UserReader.AccountType = Values[4];
						UserReader.NumberPhone = Values[5];
						ListUsers.Add(UserReader);
					}

				}
			}

			return ListUsers;
		}

		//Authenticate user credentials
		internal bool AuthenticateUser(NetworkCredential Credential)
		{
			bool ValidUser = false;
			
			//Load data if database file exists
			if (File.Exists("UsersData.db"))
			{
				using(StreamReader LoadData = new StreamReader("UsersData.db"))
				{
					string[] Values = new string[6];
					User UserReader = new User();
					int Index = 0;

					//Create one cicle for each read line in file
					while(LoadData.EndOfStream != true)
					{
						string? Line = LoadData.ReadLine();

						foreach (char Digit in Line)
						{
							//Read each character and concatenate them into their property
							if (Digit == ',')
							{
								Index++;
								continue;
							}

							Values[Index] += Digit;
						}

						UserReader.UserName = Values[2];
						UserReader.Password = Values[3];

						if (UserReader.UserName == Credential.UserName && UserReader.Password == Credential.Password)
						{
							ValidUser = true;
							break;
						}
					}
				}
			}

			return ValidUser;
		}
    }
}
