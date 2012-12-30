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
		private eKeyPress _keyPress = eKeyPress.None;//Нажатая клавиша управления
		private eKeyPress _moveDirection;
		private long _frameTime = 0;//Число милисекунд, прошедших с последнего обновления карты уровня
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
			/*AutoResetEvent autoEvent = new AutoResetEvent(false);
			TimeChecker statusChecker = new TimeChecker(Common.TimeToMove);
			TimerCallback tcb = statusChecker.CheckStatus;
			Timer stateTimer = new Timer(tcb, autoEvent, 1000, 250);
			autoEvent.WaitOne(1000, false);*/

			DateTime dt = DateTime.Now;
			long t = dt.Millisecond + dt.Second*1000 + dt.Minute*60*1000 + dt.Hour*60*60*1000;
			//_frameTime = t - _frameTime;
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
					//TODO учитывать набранные очки
					break;
			}

			//_keyPress = eKeyPress.None;//Ждём нажатия для следующего кадра
		}

		private void canvasGame_KeyDown(object sender, KeyEventArgs e)
		{
			switch(e.Key)
			{
				case Key.Up:
					if (_moveDirection != eKeyPress.Down)
						_keyPress = eKeyPress.Up;
					break;
				case Key.Down:
					if (_moveDirection != eKeyPress.Up)
						_keyPress = eKeyPress.Down;
					break;
				case Key.Left:
					if (_moveDirection != eKeyPress.Right)
						_keyPress = eKeyPress.Left;
					break;
				case Key.Right:
					if (_moveDirection != eKeyPress.Left)
						_keyPress = eKeyPress.Right;
					break;
			}
		}
		private void Window_KeyDown(object sender, KeyEventArgs e)
		{
			switch (e.Key)
			{
				case Key.Up:
					if (_moveDirection != eKeyPress.Down)
						_keyPress = eKeyPress.Up;
					break;
				case Key.Down:
					if (_moveDirection != eKeyPress.Up)
						_keyPress = eKeyPress.Down;
					break;
				case Key.Left:
					if (_moveDirection != eKeyPress.Right)
						_keyPress = eKeyPress.Left;
					break;
				case Key.Right:
					if (_moveDirection != eKeyPress.Left)
						_keyPress = eKeyPress.Right;
					break;
			}
		}
#endregion

#region Private	
		private void StartGame()
		{
			_keyPress = eKeyPress.Up;//TODO хардкод	
			_level.Start();//Стартовая карта уровня
			_snake = new Snake(_level.StartSnakeCoords);//Стартовое положение змейки
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
