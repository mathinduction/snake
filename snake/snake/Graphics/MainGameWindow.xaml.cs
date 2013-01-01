﻿using System;
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
		private Game.Level _level;//Уровень
		private eKeyPress _keyPress;//Нажатая клавиша управления
		private eKeyPress _moveDirection;//Направление движения змейки
		private long _frameTime = 0;//Число милисекунд, прошедших с последнего обновления карты уровня
		private bool _holdKey = false;//Флаг удержания клавиши. Нужен для включения ускорения
		private bool _pause = false;//Включена ли пауза
		private int _victoryPoints = 0;//Победные очки
		private DrawTheScene _drawer;//Рисовалищик
		private PixelArt _pixelArt;
#endregion
		public MainGameWindow()
		{
			InitializeComponent();
			CompositionTarget.Rendering += OnFrame;
			canvasGame.Width = Common.NumberPixelWidth * Common.PixelSize;
			canvasGame.Height = Common.NumberPixelHeight * Common.PixelSize;

			_drawer = new DrawTheScene(Common.NumberPixelWidth, Common.NumberPixelHeight);
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
			if (_pause) return;
			
			DateTime dt = DateTime.Now;
			long t = dt.Millisecond + dt.Second*1000 + dt.Minute*60*1000 + dt.Hour*60*60*1000;
			if ((t - _frameTime) < Common.TimeToMove) return;
			_frameTime = t;

			canvasGame.Children.Clear();
			_moveDirection = _keyPress;
			_snake.Move(_moveDirection);//Двигаем змейку
			eSnakeMove sm = _level.Update(_snake.SnakeCoordinates);//Обновляем карту уровня
			_drawer.Draw(_level, ref canvasGame);//Перерисовываем сцену

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
					_victoryPoints++;
					textBlockVPoints.Text = _victoryPoints.ToString();
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
				case Key.Space:
					Pause();
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

		private void Window_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
		{
			Pause(true);
		}
		private void Window_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
		{
			Pause(false);
		}
#endregion

#region Private	
		/// <summary>
		/// Установление начальных параметров игры
		/// </summary>
		private void StartGame()
		{
			_level = new Level(Common.NumberPixelWidth, Common.NumberPixelHeight);
			_level.Start("Level1");//Стартовая карта уровня //TODO хардкод
			_keyPress = _level.SnakeStartDirection;
			_snake = new Snake(_level.StartSnakeCoord, _level.SnakeStartDirection);//Стартовое положение змейки
			_level.Update(_snake.SnakeCoordinates);//Учитываем положение змейки
			_level.GenerateFood();//Генерируем еду
			_drawer.Draw(_level, ref canvasGame);//Рисуем сцену

			DateTime dt = DateTime.Now;
			long t = dt.Millisecond + dt.Second * 1000 + dt.Minute * 60 * 1000 + dt.Hour * 60 * 60 * 1000;
			_frameTime = t;
		}
		/// <summary>
		/// Пауза
		/// </summary>
		private void Pause()
		{
			Pause(!_pause);
		}
		private void Pause(bool pause)
		{
			if (_pause && pause) return;
			_pause = pause;
			if (_pause)
			{
				_pixelArt = new PixelArt(PixelArt.eArts.Pause);
				_pixelArt.Closed += new EventHandler(_pixelArt_Closed);
				_pixelArt.Show();
				this.Visibility = Visibility.Collapsed;
			}
			else
			{	
				
			}
		}

		void _pixelArt_Closed(object sender, EventArgs e)
		{
			if (_pixelArt != null)
				_pixelArt.StopPauseArt();
			this.Visibility = Visibility.Visible;
		}
#endregion
	}
}
