using System;
using System.Collections.Generic;
using System.IO;
using SiaERP.Models;

namespace SiaERP.Data
{
    class CSVDatabase
    {
		public List<Service> Get()
		{
			List<Service> ListServices = new List<Service>();

			//Load data if database file exists
			if (File.Exists("Database.dll"))
			{
				using(StreamReader LoadData = new StreamReader("Database.dll"))
				{
					while(LoadData.EndOfStream != true)
					{
						//Create one instance for each line reading and save into service list
						Service ServiceLoading = new Service();
						string? Line = LoadData.ReadLine(); //Data.DecryptMD5(LoadData.ReadLine());
						string Folio = "", Customer = "", Status = "";
						int Index = 0;
						
						if (Line != null)
						{
							foreach (char Digit in Line)
							{
								//Read each character and concatenate them into their property
								if (Digit == ',')
								{
									Index++;
									continue;
								}

								switch (Index)
								{
									case 0: Folio += Digit; break;
									case 1: Customer += Digit; break;
									case 2: ServiceLoading.Name += Digit; break;
									case 3: ServiceLoading.AdmissionDate += Digit; break;
									case 4: ServiceLoading.DeliveryDate += Digit; break;
									case 5: Status += Digit; break;
									case 6: ServiceLoading.Diagnostic += Digit; break;
								}
							}
						}

						ServiceLoading.Folio = Convert.ToInt32(Folio);
						ServiceLoading.Customer = Convert.ToInt32(Customer);
						ServiceLoading.Status = Status;
						ListServices.Add(ServiceLoading);
					}
				}

				//Refresh data in listview with instances in service list
				//Service.Load(ServicesListView, ServicesList);
			}

			return ListServices;
		}

		public void Delete(Service Model)
		{
			//Search the database to delte the register
			if (File.Exists("Database.dll"))
			{
				//Create one stream to read the database and another to write a temporary database
				using(StreamReader DeleteData = new StreamReader("Database.dll"))
				{
					using(StreamWriter WriteData = new StreamWriter("DatabaseTemp.dll"))
					{
						int DeleteIndex = 0;
						int Index = 0;

						while(DeleteData.EndOfStream != true)
						{
							String? Line = DeleteData.ReadLine();

							//Write all lines of original database to the temporary one, skipping the delete index
							if (Index != DeleteIndex)
							{
								WriteData.WriteLine(Line);
							}

							Index++;
						}
					}
				}
			}

			//Delete the original database and rename the temporary like the new 
			File.Delete("Database.dll");
			File.Move("DatabaseTemp.dll", "Database.dll");
		}
    }
}
