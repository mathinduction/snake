using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using snake.Game;

namespace snake.Graphics
{
	/// <summary>
	/// Interaction logic for MainGameWindow.xaml
	/// </summary>
	public partial class MainGameWindow : Window
	{
#region Private
		private Game.Snake _snake;//Змейка
		private Game.Level _level = new Level();//Уровень
		private eKeyPress _keyPress;//Нажатая клавиша управления
		private eKeyPress _moveDirection;//Направление движения змейки
		private long _frameTime = 0;//Число милисекунд, прошедших с последнего обновления карты уровня
		private bool _holdKey = false;//Флаг удержания клавиши. Нужен для включения ускорения
#endregion
		public MainGameWindow()
		{
			InitializeComponent();
			CompositionTarget.Rendering += OnFrame;
			canvasGame.Width = Common.NumberPixelWidth * Common.PixelSize;
			canvasGame.Height = Common.NumberPixelHeight * Common.PixelSize;

			StartGame();
		}

#region Events
		private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			CompositionTarget.Rendering -= OnFrame;
		}
		/// <summary>
		/// Смена кадра
		/// </summary>
		private void OnFrame(object sender, EventArgs e)
		{
			DateTime dt = DateTime.Now;
			long t = dt.Millisecond + dt.Second*1000 + dt.Minute*60*1000 + dt.Hour*60*60*1000;
			if ((t - _frameTime) < Common.TimeToMove) return;
			_frameTime = t;

			canvasGame.Children.Clear();
			_moveDirection = _keyPress;
			_snake.Move(_moveDirection);//Двигаем змейку
			eSnakeMove sm = _level.Update(_snake.SnakeCoordinates);//Обновляем карту уровня
			DrawTheScene.Draw(_level, ref canvasGame);//Перерисовываем сцену

			switch (sm)//Учитываем результат движения змейки
			{
				case eSnakeMove.Normal:
					break;
				case eSnakeMove.Died:
					MessageBox.Show("GameOver!");
					this.Close();
					break;
				case eSnakeMove.Fed:
					_snake.LengthUp();
					_level.GenerateFood();
					//TODO учитывать набранные очки
					break;
			}
		}

		private void Window_KeyDown(object sender, KeyEventArgs e)
		{
			bool good = false;
			switch (e.Key)
			{
				case Key.Up:
					if (_moveDirection != eKeyPress.Down)
					{
						_keyPress = eKeyPress.Up;
						good = true;
					}
					break;
				case Key.Down:
					if (_moveDirection != eKeyPress.Up)
					{
						_keyPress = eKeyPress.Down;
						good = true;
					}
					break;
				case Key.Left:
					if (_moveDirection != eKeyPress.Right)
					{
						_keyPress = eKeyPress.Left;
						good = true;
					}
					break;
				case Key.Right:
					if (_moveDirection != eKeyPress.Left)
					{
						_keyPress = eKeyPress.Right;
						good = true;
					}
					break;
			}
			if (good)
			{
				if (_holdKey)
				{
					Common.HoldKey = true;//Включаем ускорение
				}
				else
				{
					_holdKey = true;
				}
			}
		}
		private void Window_KeyUp(object sender, KeyEventArgs e)
		{
			_holdKey = false;
			Common.HoldKey = false;
		}
#endregion

#region Private	
		private void StartGame()
		{
			_level.Start();//Стартовая карта уровня
			_keyPress = _level.SnakeStartDirection;
			_snake = new Snake(_level.StartSnakeCoord, _level.SnakeStartDirection);//Стартовое положение змейки
			_level.Update(_snake.SnakeCoordinates);//Учитываем положение змейки
			_level.GenerateFood();//Генерируем еду
			DrawTheScene.Draw(_level, ref canvasGame);//Рисуем сцену

			DateTime dt = DateTime.Now;
			long t = dt.Millisecond + dt.Second * 1000 + dt.Minute * 60 * 1000 + dt.Hour * 60 * 60 * 1000;
			_frameTime = t;
		}
#endregion

		
	}
}
