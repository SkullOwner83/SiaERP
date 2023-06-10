﻿using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Runtime.InteropServices;
using System.Windows.Interop;
using SiaERP.ViewModels;

namespace SiaERP.Views
{
	public partial class Main : Window
	{
		private GridLength MenuMinSize = new GridLength(48);
		private GridLength MenuMaxSize = new GridLength(192);

		//Constructor Method
		public Main()
		{
			InitializeComponent();
			this.DataContext = new MainViewModel();
			this.MaxWidth = SystemParameters.MaximizedPrimaryScreenWidth;
			this.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
		}

		//Import windows drag native library
		[DllImport("user32.dll")]
		public static extern IntPtr SendMessage(IntPtr hWnd, int wMsg, int wParam, int lParam);

		//Window movement and behavior control
		private void MtdDragWindow(object sender, MouseButtonEventArgs e)
		{
			//Move window when clicking on header
			if (e.ClickCount == 1)
			{
				if (e.LeftButton == MouseButtonState.Pressed)
				{
					WindowInteropHelper helper = new WindowInteropHelper(this);
					SendMessage(helper.Handle, 161, 2, 0);
				}
			}
			//Maximized or restore window when double clicking on header
			else if (e.ClickCount == 2)
			{
				MtdWindowMaximize();
			}
		}

		//Button cliked function controls
		public void MtdButtonClicked(object sender, RoutedEventArgs e)
		{
			switch(((Button)sender).Name)
			{
				//Window behavior buttons control
				case "BtnWindowMinimize": WindowState = WindowState.Minimized; break;
				case "BtnWindowMaximize": MtdWindowMaximize(); break;
				case "BtnWindowClose": Application.Current.Shutdown(); break;

				case "BtnExpandPanel":
					if (PnlMenu.Width == MenuMinSize)
					{
						PnlMenuHeader.Height = new GridLength(126);
						PnlMenu.Width = MenuMaxSize;
					}
					else
					{
						PnlMenuHeader.Height = new GridLength(48);
						PnlMenu.Width = MenuMinSize;
					}
				break;

				case "BtnLogOut": Application.Current.Shutdown(); break;
			}
		}

		//Interchanged between maximize and normal window state
		public void MtdWindowMaximize()
		{
			if (WindowState == WindowState.Maximized)
			{
				WindowState = WindowState.Normal;
			}
			else if (WindowState == WindowState.Normal)
			{
				WindowState = WindowState.Maximized;
			}
		}
	}
}
