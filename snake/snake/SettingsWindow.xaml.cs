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

namespace snake
{
	/// <summary>
	/// Interaction logic for SettingsWindow.xaml
	/// </summary>
	public partial class SettingsWindow : Window
	{
		public SettingsWindow()
		{
			InitializeComponent();
			textBoxRecursionDepth.Text = Common.RecursionDepth.ToString();
			textBoxTimeToMove.Text = Common.TimeToMovePlayer.ToString();
			textBoxTimeToMoveFoe.Text = Common.TimeToMoveAI.ToString();
		}
#region Events
		private void buttonOk_Click(object sender, RoutedEventArgs e)
		{
			if (CheckForErrors())
			{
				Save();
				this.Close();
			}
		}

		private void buttonCancel_Click(object sender, RoutedEventArgs e)
		{
			this.Close();
		}
#endregion
#region Private
		/// <summary>
		/// Проверяет на ошибки введённые значения
		/// </summary>
		private bool CheckForErrors()
		{
			if (!IsNumeric(textBoxRecursionDepth.Text))
				return false;
			if (!IsNumeric(textBoxTimeToMove.Text))
				return false;
			if (!IsNumeric(textBoxTimeToMoveFoe.Text))
				return false;

			if (int.Parse(textBoxRecursionDepth.Text) > 15) //TODO хардкод
			{
				MessageBox.Show("Слишком большая глубина рекурсии!", "Ошибка!");
				return false;
			}
			return true;
		}
		/// <summary>
		/// Проверяет, состоит ли указанная строка только из цифр
		/// </summary>
		private bool IsNumeric(string value)
		{
			for (int i = 0; i < value.Length; i++)
			{
				if (!char.IsDigit(value[i]))
				{
					MessageBox.Show("Введены некорректные данные!", "Ошибка!");
					return false;
				}
			}
			return true;
		}
		/// <summary>
		/// Сохраняет введённые значения
		/// </summary>
		private void Save()
		{
			Common.RecursionDepth = int.Parse(textBoxRecursionDepth.Text);
			Common.TimeToMovePlayer = int.Parse(textBoxTimeToMove.Text);
			Common.TimeToMoveAI = int.Parse(textBoxTimeToMoveFoe.Text);
		}
#endregion
	}
}
