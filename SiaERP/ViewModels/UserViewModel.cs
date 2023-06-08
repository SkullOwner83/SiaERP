﻿using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using SiaERP.Data;
using SiaERP.Models;

namespace SiaERP.ViewModels
{
    internal class UserViewModel : ViewModelBase
    {
		private readonly SQLDatabase Database;
		private ObservableCollection<User> listUsers;
		private User currentUser;

		//Starting necesary instances to store the data
		public UserViewModel()
		{
			Database = new SQLDatabase();
			currentUser = new User();
			listUsers = Database.Get();
		}

		public User CurrentUser
		{
			get => currentUser;
			set
			{
				if (currentUser != value)
				{
					currentUser = value;
					OnPropertyChanged(nameof(CurrentUser));
				}
			}
		}

		public ObservableCollection<User> ListUsers
		{
			get => listUsers;
			set
			{
				if (listUsers != value)
				{
					listUsers = value;
					OnPropertyChanged(nameof(ListUsers));
				}
			}
		}

		//Send methods to relay command class like delegates
		public ICommand AddCommand
		{
			get
			{
				return new RelayCommand(AddExecute, AddCanExecute);
			}
		}

		//Add new user in database and refresh list users with new register
		private void AddExecute(Object user)
		{
			CurrentUser.Id = Convert.ToInt32(Guid.NewGuid());
			Database.Add(CurrentUser);
			ListUsers = Database.Get();
		}

		private bool AddCanExecute(Object user)
		{ 
			return true;
		}
    }
}