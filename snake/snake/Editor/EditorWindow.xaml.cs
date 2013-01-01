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
using snake.Graphics;

namespace snake.Editor
{
	/// <summary>
	/// Interaction logic for EditorWindow.xaml
	/// </summary>
	public partial class EditorWindow : Window
	{
		private Game.Level _level = new Level(Common.NumberPixelWidth, Common.NumberPixelHeight);
		private Point? _snakeStartCoord;
		private Point? _snakeFoeStartCoord;
		private eKeyPress _direction = eKeyPress.Up;
		private eKeyPress _foeDirection = eKeyPress.Up;
		private DrawTheScene _drawer;//Рисовалищик

		public EditorWindow()
		{
			InitializeComponent();
			canvasLevelMap.Width = Common.NumberPixelWidth*Common.PixelSize;
			canvasLevelMap.Height = Common.NumberPixelHeight*Common.PixelSize;

			_snakeStartCoord = null;

			textBoxNumberPixelWidth.Text = Common.NumberPixelWidth.ToString();
			textBoxNumberPixelHeight.Text = Common.NumberPixelHeight.ToString();

			comboBoxDirection.Items.Add("Вверх");
			comboBoxDirection.Items.Add("Вправо");
			comboBoxDirection.Items.Add("Вниз");
			comboBoxDirection.Items.Add("Влево");

			comboBoxDirection.SelectedIndex = 0;

			comboBoxFoeDirection.Items.Add("Вверх");
			comboBoxFoeDirection.Items.Add("Вправо");
			comboBoxFoeDirection.Items.Add("Вниз");
			comboBoxFoeDirection.Items.Add("Влево");

			comboBoxFoeDirection.SelectedIndex = 0;

			_level.Init();
			_drawer = new DrawTheScene(Common.NumberPixelWidth, Common.NumberPixelHeight);
			DrawBorderBlocks((bool) checkBoxGenerateBorderBlocks.IsChecked);
			DrawGrid();
		}

		#region Events

		private void canvasLevelMap_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			Point pixel = PointToPixelCoord(e.GetPosition(canvasLevelMap));
			if (radioButtonBlock.IsChecked == true)
			{
				if (_level.LevelPixels[(int) pixel.X, (int) pixel.Y] == ePixelType.Block)
					_level.LevelPixels[(int) pixel.X, (int) pixel.Y] = ePixelType.None;
				else
					_level.LevelPixels[(int) pixel.X, (int) pixel.Y] = ePixelType.Block;
			}
			else if (radioButtonSnakeStart.IsChecked == true)
			{
				if (_snakeStartCoord.HasValue)
				{
					_level.LevelPixels[(int) _snakeStartCoord.Value.X, (int) _snakeStartCoord.Value.Y] = ePixelType.None;
				}
				_level.LevelPixels[(int) pixel.X, (int) pixel.Y] = ePixelType.SnakePart;
				_snakeStartCoord = pixel;
			}
			else if (radioButtonFoeSnakeStart.IsChecked == true)
			{
				if (_snakeFoeStartCoord.HasValue)
				{
					_level.LevelPixels[(int)_snakeFoeStartCoord.Value.X, (int)_snakeFoeStartCoord.Value.Y] = ePixelType.None;
				}
				_level.LevelPixels[(int)pixel.X, (int)pixel.Y] = ePixelType.FoeSnakePart;
				_snakeFoeStartCoord = pixel;
			}
			else if (radioButtonFood.IsChecked == true)
			{
				_level.LevelPixels[(int)pixel.X, (int)pixel.Y] = ePixelType.Food;
			}
			_drawer.Draw(_level, ref canvasLevelMap);
			DrawGrid();
		}

		private void textBoxNumberPixelWidth_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key != Key.Enter) return;
			int width = 0;
			if (textBoxNumberPixelWidth.Text == "") return;
			bool rez = int.TryParse(textBoxNumberPixelWidth.Text, out width);
			if (!rez)
			{
				MessageBox.Show("Некорректрые денные!", "Ошибка!");
				return;
			}
			Common.NumberPixelWidth = width;
			canvasLevelMap.Width = Common.NumberPixelWidth*Common.PixelSize;
			_level.Resize();
			DrawBorderBlocks((bool) checkBoxGenerateBorderBlocks.IsChecked);
			DrawGrid();
		}

		private void textBoxNumberPixelHeight_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key != Key.Enter) return;
			int height = 0;
			if (textBoxNumberPixelHeight.Text == "") return;
			bool rez = int.TryParse(textBoxNumberPixelHeight.Text, out height);
			if (!rez)
			{
				MessageBox.Show("Некорректрые денные!", "Ошибка!");
				return;
			}
			Common.NumberPixelHeight = height;
			canvasLevelMap.Height = Common.NumberPixelHeight*Common.PixelSize;
			_level.Resize();
			DrawBorderBlocks((bool) checkBoxGenerateBorderBlocks.IsChecked);
			DrawGrid();
		}

		private void checkBoxGenerateBorderBlocks_Click(object sender, RoutedEventArgs e)
		{
			DrawBorderBlocks((bool) checkBoxGenerateBorderBlocks.IsChecked);
		}

		private void buttonOpen_Click(object sender, RoutedEventArgs e)
		{
			//TODO
		}

		private void buttonSave_Click(object sender, RoutedEventArgs e)
		{
			if (!CheckForErrors()) return;
			_direction = (eKeyPress)comboBoxDirection.SelectedIndex;
			_foeDirection = (eKeyPress)comboBoxFoeDirection.SelectedIndex;
			SaveWindow saveWindow = new SaveWindow(_level, _direction, _foeDirection);
			saveWindow.Show();
		}

		#endregion

		#region Private

		/// <summary>
		/// Рисует сетку
		/// </summary>
		private void DrawGrid()
		{
			for (int i = 1; i < Common.NumberPixelHeight; i++)
			{
				Line line = new Line();
				line.Stroke = Brushes.RoyalBlue;
				line.X1 = 0;
				line.Y1 = i*Common.PixelSize;
				line.X2 = canvasLevelMap.Width;
				line.Y2 = i*Common.PixelSize;
				canvasLevelMap.Children.Add(line);
			}
			for (int i = 0; i < Common.NumberPixelWidth; i++)
			{
				Line line = new Line();
				line.Stroke = Brushes.RoyalBlue;
				line.X1 = i*Common.PixelSize;
				line.Y1 = 0;
				line.X2 = i*Common.PixelSize;
				line.Y2 = canvasLevelMap.Height;
				canvasLevelMap.Children.Add(line);
			}
		}

		/// <summary>
		/// Определяет в каком пикселе находится указанная точка
		/// </summary>
		private Point PointToPixelCoord(Point point)
		{
			double x = Math.Floor(point.X/(double) Common.PixelSize);
			double y = Math.Floor(point.Y/(double) Common.PixelSize);
			return new Point(x, y);
		}

		/// <summary>
		/// Рисует/стирает блоки по краям
		/// </summary>
		/// <param name="need"></param>
		private void DrawBorderBlocks(bool need)
		{
			ePixelType type;
			if (need)
			{
				type = ePixelType.Block;
			}
			else
			{
				type = ePixelType.None;
			}
			for (int j = 0; j < _level.LevelPixels.GetLength(1); j++)
			{
				_level.LevelPixels[0, j] = type;
				_level.LevelPixels[Common.NumberPixelWidth - 1, j] = type;
			}
			for (int i = 0; i < _level.LevelPixels.GetLength(0); i++)
			{
				_level.LevelPixels[i, 0] = type;
				_level.LevelPixels[i, Common.NumberPixelHeight - 1] = type;
			}
			_drawer.Draw(_level, ref canvasLevelMap);
		}

		/// <summary>
		/// Проверка на ошибки
		/// </summary>
		/// <returns></returns>
		private bool CheckForErrors()
		{
			if (!_snakeStartCoord.HasValue)
			{
				MessageBox.Show("Не указано начальное положение змейки", "Ошибка!");
				return false;
			}
			if (!_snakeFoeStartCoord.HasValue)
			{
				MessageBox.Show("Не указано начальное положение змейки-конкурента", "Ошибка!");
				return false;
			}
			switch (comboBoxDirection.SelectedIndex)
			{
				case 0://Up
					if ((Common.NumberPixelHeight - _snakeStartCoord.Value.Y) < 4)
					{
						MessageBox.Show("Недопустимое начальное положение змейки!", "Ошибка!");
						return false;
					}
					_direction = eKeyPress.Up;
					break;
				case 1://Right
					if (_snakeStartCoord.Value.X < 3)
					{
						MessageBox.Show("Недопустимое начальное положение змейки!", "Ошибка!");
						return false;
					}
					_direction = eKeyPress.Right;
					break;
				case 2://Down
					if (_snakeStartCoord.Value.Y < 3)
					{
						MessageBox.Show("Недопустимое начальное положение змейки!", "Ошибка!");
						return false;
					}
					_direction = eKeyPress.Down;
					break;
				case 3://Left
					if ((Common.NumberPixelWidth - _snakeStartCoord.Value.X) < 4)
					{
						MessageBox.Show("Недопустимое начальное положение змейки!", "Ошибка!");
						return false;
					}
					_direction = eKeyPress.Left;
					break;
			}
			return true;
		}
		#endregion
	}
}
