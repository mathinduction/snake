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

namespace snake.Editor
{
	/// <summary>
	/// Interaction logic for EditorWindow.xaml
	/// </summary>
	public partial class EditorWindow : Window
	{
		public EditorWindow()
		{
			InitializeComponent();
			canvasLevelMap.Width = Common.NumberPixelWidth * Common.PixelSize;
			canvasLevelMap.Height = Common.NumberPixelHeight * Common.PixelSize;

			textBoxNumberPixelWidth.Text = Common.NumberPixelWidth.ToString();
			textBoxNumberPixelHeight.Text = Common.NumberPixelHeight.ToString();

			comboBoxDirection.Items.Add("Вверх");
			comboBoxDirection.Items.Add("Вниз");
			comboBoxDirection.Items.Add("Влево");
			comboBoxDirection.Items.Add("Вправо");

			comboBoxDirection.SelectedIndex = 0;
		}

		private void canvasLevelMap_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			
		}

		private void textBoxNumberPixelWidth_TextChanged(object sender, TextChangedEventArgs e)
		{
			int width = 0;
			if (textBoxNumberPixelWidth.Text == "") return;
			bool rez = int.TryParse(textBoxNumberPixelWidth.Text, out width);
			if (!rez)
			{
				MessageBox.Show("Некорректрые денные!", "Ошибка!");
				return;
			}
			Common.NumberPixelWidth = width;
			canvasLevelMap.Width = Common.NumberPixelWidth * Common.PixelSize;
		}

		private void textBoxNumberPixelHeight_TextChanged(object sender, TextChangedEventArgs e)
		{
			int height = 0;
			if (textBoxNumberPixelHeight.Text == "") return;
			bool rez = int.TryParse(textBoxNumberPixelHeight.Text, out height);
			if (!rez)
			{
				MessageBox.Show("Некорректрые денные!", "Ошибка!");
				return;
			}
			Common.NumberPixelHeight = height;
			canvasLevelMap.Height = Common.NumberPixelHeight * Common.PixelSize;
		}
	}
}
