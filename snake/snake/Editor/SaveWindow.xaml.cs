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
using System.Windows.Shapes;
using snake.Game;

namespace snake.Editor
{
	/// <summary>
	/// Interaction logic for SaveWindow.xaml
	/// </summary>
	public partial class SaveWindow : Window
	{
		private Game.Level _level = new Level();
		private eKeyPress _direction;
		public SaveWindow(Game.Level level, eKeyPress direction)
		{
			InitializeComponent();
			_level = level;
			_direction = direction;
		}

		private void butonSave_Click(object sender, RoutedEventArgs e)
		{
			if (textBoxNameLevel.Text == "")
			{
				MessageBox.Show("Не указано название локации!", "Ошибка!");
				return;
			}
			string path = Common.PathLevels;
			if (!System.IO.Directory.Exists(path))
			{
				System.IO.Directory.CreateDirectory(path);
			}
			path += "//" + textBoxNameLevel.Text + ".lvl";
			if (System.IO.File.Exists(path))
			{
				MessageBox.Show("Локация с таким названием уже существует!", "Ошибка!");
				return;
			}
			try
			{
				//System.IO.File.Create(path);
				using (System.IO.StreamWriter file = new System.IO.StreamWriter(path))
				{
					file.WriteLine(((int)_direction).ToString());
					file.WriteLine(_level.LevelPixels.GetLength(0));
					file.WriteLine(_level.LevelPixels.GetLength(1));
					for (int i = 0; i < _level.LevelPixels.GetLength(0); i++)
						for (int j = 0; j < _level.LevelPixels.GetLength(1); j++)
						{
							file.WriteLine(((int)_level.LevelPixels[i,j]).ToString());
						}
				}
			}
			catch (Exception exp)
			{
				MessageBox.Show(exp.Message, "Ошибка!");
				return;
			}
			this.Close();
		}

		private void buttonCancel_Click(object sender, RoutedEventArgs e)
		{
			this.Close();
		}
	}
}
