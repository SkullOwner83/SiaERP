using System;

namespace SiaERP.Models
{
    public class User
    {
		//Define class properties
		private int id;
		private string name;
		private string userName;
		private string password;
		private string accountType;
		private string numberPhone;
		private DateTime lastlogin;

        //Encapsulate properties
        public int Id 
		{ 
			get => id; 
			set
			{ 
				if (id != value)
				{
					id = value;
				}

			}
		}

		public string Name 
		{ 
			get => name; 
			set
			{ 
				if (name != value)
				{
					name = value;
				}
			}
		}

		public string UserName 
		{ 
			get => userName; 
			set => userName = value; 
		}

		public string Password 
		{ 
			get => password; 
			set
			{ 
				if (password != value)
				{
					password = value;
				}
			}
		}

		public string AccountType 
		{ 
			get => accountType; 
			set
			{ 
				if (accountType != value)
				{
					accountType = value;
				}
			}
		}

		public string NumberPhone 
		{
			get => numberPhone; 
			set
			{ 
				if (numberPhone != value)
				{
					numberPhone = value;
				}
			}
		}

        public DateTime LastLogin 
		{ 
			get => lastlogin; 
			set => lastlogin = value; 
		}
    }
}
