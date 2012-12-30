using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using snake.Graphics;

namespace snake
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
		}

		private void buttonStart_Click(object sender, RoutedEventArgs e)
		{
			Graphics.MainGameWindow gameWindow = new MainGameWindow();
			gameWindow.Closing += new System.ComponentModel.CancelEventHandler(gameWindow_Closing);
			gameWindow.Show();
			this.Visibility = Visibility.Collapsed;
		}

		void gameWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			this.Visibility = Visibility.Visible;
		}
	}
}
