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

namespace snake.Graphics
{
	/// <summary>
	/// Interaction logic for MainGameWindow.xaml
	/// </summary>
	public partial class MainGameWindow : Window
	{
#region Private
		private Game.Snake _snake = new Snake();//Змейка
		private Game.Level _level = new Level();//Уровень
		private eKeyPress _keyPress = eKeyPress.None;//Нажатая клавиша управления
#endregion
		public MainGameWindow()
		{
			InitializeComponent();
			CompositionTarget.Rendering += OnFrame;
			this.Height = Common.NumberPixelHeight*Common.PixelSize;
			this.Width = Common.NumberPixelWidth*Common.PixelSize;
		}

#region Events
		/// <summary>
		/// Смена кадра
		/// </summary>
		private void OnFrame(object sender, EventArgs e)
		{
			_snake.Move(_keyPress);//Двигаем змейку
			_level.Update(_snake.SnakeCoordinates);//Обновляем карту уровня
			DrawTheScene.Draw(_level, canvasGame);//Перерисовываем сцену

			//TODO учитывать состояние игры: съели еду, врезались...

			_keyPress = eKeyPress.None;//Ждём нажатия для следующего кадра
		}

		private void canvasGame_KeyDown(object sender, KeyEventArgs e)
		{
			switch(e.Key)
			{
				case Key.Up:
					_keyPress = eKeyPress.Up;
					break;
				case Key.Down:
					_keyPress = eKeyPress.Down;
					break;
				case Key.Left:
					_keyPress = eKeyPress.Left;
					break;
				case Key.Right:
					_keyPress = eKeyPress.Right;
					break;
			}
		}
#endregion

#region Private	
		private void StartGame()
		{
			_level.Start();//Стартовая карта уровня
			_snake.ToTheStart();//Стартовое положение змейки
			_level.Update(_snake.SnakeCoordinates);//Учитываем положение змейки
			_level.GenerateFood();//Генерируем еду
			DrawTheScene.Draw(_level, canvasGame);//Рисуем сцену
		}
#endregion
	}
}
