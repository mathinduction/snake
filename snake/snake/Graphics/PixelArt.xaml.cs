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
	/// Interaction logic for PixelArt.xaml
	/// </summary>
	public partial class PixelArt : Window
	{
#region Private
		private int _sizeX;
		private int _sizeY;
		private Game.Level _level = new Level(Common.NumberPixelWidth, Common.NumberPixelHeight);
		private DrawTheScene _drawer;
		private eKeyPress _moveDirection;
		private Game.Snake _snake;
		private long _frameTime = 0;//Число милисекунд, прошедших с последнего обновления карты уровня
#endregion
		public enum eArts
		{
			Pause,
			GameOver
		}
		public PixelArt(eArts art)
		{
			InitializeComponent();
			switch (art)
			{
				case eArts.Pause:
					StartPauseArt();
					CompositionTarget.Rendering += PauseArt;
					break;
				case eArts.GameOver:
					StartGameOverArt();
					CompositionTarget.Rendering += GameOverArt;
					break;
			}
		}
#region PauseArt
		private void PauseArt(object sender, EventArgs e)
		{
			DateTime dt = DateTime.Now;
			long t = dt.Millisecond + dt.Second * 1000 + dt.Minute * 60 * 1000 + dt.Hour * 60 * 60 * 1000;
			if ((t - _frameTime) < Common.TimeToMovePlayer) return;
			_frameTime = t;

			canvasPixelArt.Children.Clear();
			_moveDirection = GenerateMoveDrection();
			_snake.Move(_moveDirection);//Двигаем змейку
			eSnakeMove sm = _level.Update(_snake.SnakeCoordinates, false);//Обновляем карту уровня
			_drawer.Draw(_level, ref canvasPixelArt);//Перерисовываем сцену

			switch (sm)//Учитываем результат движения змейки
			{
				case eSnakeMove.Normal:
					break;
				case eSnakeMove.Died:
					StartPauseArt();
					break;
				case eSnakeMove.Fed:
					_snake.LengthUp();
					break;
			}
		}
		private void StartPauseArt()
		{
			_sizeX = 28;//28x10 - размеры необходимые для арта
			_sizeY = 10;
			canvasPixelArt.Width = _sizeX * Common.PixelSize;
			canvasPixelArt.Height = _sizeY * Common.PixelSize;
			_drawer = new DrawTheScene(_sizeX, _sizeY);

			_level = new Level(_sizeX, _sizeY);
			_level.Start("PauseArt");//Стартовая карта уровня
			_moveDirection = _level.SnakeStartDirection;
			_snake = new Snake(_level.StartSnakeCoord, _moveDirection);//Стартовое положение змейки
			_snake.SnakeCoordinates = SetSnakeCoord();
			_level.Update(_snake.SnakeCoordinates, false);//Учитываем положение змейки
			_drawer.Draw(_level, ref canvasPixelArt);//Рисуем сцену

			DateTime dt = DateTime.Now;
			long t = dt.Millisecond + dt.Second * 1000 + dt.Minute * 60 * 1000 + dt.Hour * 60 * 60 * 1000;
			_frameTime = t;
		}
		public void StopPauseArt()
		{
			CompositionTarget.Rendering -= PauseArt;
		}
		/// <summary>
		/// Генерирует направление движениея
		/// </summary>
		private eKeyPress GenerateMoveDrection()
		{
			bool good = false;
			int x, count = 0;
			do
			{
				Random coord = new Random(DateTime.Now.Millisecond);
				x = coord.Next(4);
				Point snakeHead = _snake.SnakeCoordinates[0];
				switch (x)//Учитываем результат движения змейки
				{
					case 0://Up
						Point p1 = new Point(snakeHead.X, snakeHead.Y - 1);
						if (_level.LevelPixels[(int)p1.X, (int)p1.Y] != ePixelType.Block &&
							_level.LevelPixels[(int)p1.X, (int)p1.Y] != ePixelType.SnakePart &&
							_moveDirection != eKeyPress.Down)
							good = true;
						break;
					case 1://Right
						Point p2 = new Point(snakeHead.X + 1, snakeHead.Y);
						if (_level.LevelPixels[(int)p2.X, (int)p2.Y] != ePixelType.Block &&
							_level.LevelPixels[(int)p2.X, (int)p2.Y] != ePixelType.SnakePart &&
							_moveDirection != eKeyPress.Left)
							good = true;
						break;
					case 2://Down
						Point p3 = new Point(snakeHead.X, snakeHead.Y + 1);
						if (_level.LevelPixels[(int)p3.X, (int)p3.Y] != ePixelType.Block &&
							_level.LevelPixels[(int)p3.X, (int)p3.Y] != ePixelType.SnakePart &&
							_moveDirection != eKeyPress.Up)
							good = true;
						break;
					case 3://Left
						Point p4 = new Point(snakeHead.X - 1, snakeHead.Y);
						if (_level.LevelPixels[(int)p4.X, (int)p4.Y] != ePixelType.Block &&
							_level.LevelPixels[(int)p4.X, (int)p4.Y] != ePixelType.SnakePart &&
							_moveDirection != eKeyPress.Right)
							good = true;
						break;
				}
				count ++;
			} while (!good && count < 100);
			
			return (eKeyPress)x;
		}
		/// <summary>
		/// Устанавливает начальную карту уровня
		/// </summary>
		private void SetLevel(ref Level level)
		{
			int[,] levelPixels = new int[, ]
			{
			{1,1,1,1,1,1,1,1,1,1},
			{1,0,0,0,0,0,0,0,0,1},
			{1,0,2,0,0,2,0,2,0,1},
			{1,0,2,0,0,2,0,2,0,1},
			{1,0,2,0,0,2,0,2,0,1},
			{1,0,2,2,2,2,2,2,0,1},
			{1,0,0,0,0,0,0,0,0,1},
			{1,0,0,0,0,0,0,0,0,1},
			{1,0,0,0,0,0,0,0,0,1},
			{1,0,0,0,0,0,0,0,0,1},
			{1,0,0,0,0,0,0,0,0,1},
			{1,0,0,0,0,0,0,0,0,1},
			{1,0,2,2,2,2,2,2,0,1},
			{1,0,0,0,0,0,0,2,0,1},
			{1,0,0,0,0,0,0,2,0,1},
			{1,0,2,2,2,2,2,0,0,1},
			{1,0,0,0,0,0,0,0,0,1},
			{1,0,2,2,2,2,2,2,0,1},
			{1,0,2,0,0,2,0,0,0,1},
			{1,0,2,0,0,2,0,0,0,1},
			{1,0,0,2,2,2,2,2,0,1},
			{1,0,0,0,0,0,0,0,0,1},
			{1,0,0,2,2,0,0,0,0,1},
			{1,0,2,0,0,2,0,0,0,1},
			{1,0,2,0,0,2,0,0,0,1},
			{1,0,2,2,2,2,2,2,0,1},
			{1,0,0,0,0,0,0,0,0,1},
			{1,1,1,1,1,1,1,1,1,1}
			};
			for (int i = 0; i < levelPixels.GetLength(0); i++)
				for (int j = 0; j < levelPixels.GetLength(1); j++)
				{
					switch (levelPixels[i,j])
					{
						case 0:
							level.LevelPixels[i, j] = ePixelType.None;
							break;
						case 1:
							level.LevelPixels[i, j] = ePixelType.Block;
							break;
						case 2:
							level.LevelPixels[i, j] = ePixelType.Food;
							break;
						case 3:
							level.LevelPixels[i, j] = ePixelType.SnakePart;
							break;
					}
				}
		}
		/// <summary>
		/// Устаравливает начальное положение змейки
		/// </summary>
		/// <returns></returns>
		private List<Point> SetSnakeCoord()
		{
			List<Point> p = new List<Point>();
			p.Add(new Point(17, 7));
			p.Add(new Point(18, 7));
			p.Add(new Point(19, 7));
			p.Add(new Point(20, 7));
			p.Add(new Point(20, 6));
			p.Add(new Point(20, 5));
			p.Add(new Point(19, 5));
			p.Add(new Point(18, 5));
			p.Add(new Point(17, 5));
			p.Add(new Point(17, 4));
			p.Add(new Point(17, 3));
			p.Add(new Point(17, 2));
			p.Add(new Point(18, 2));
			p.Add(new Point(19, 2));
			p.Add(new Point(20, 2));

			return p;
		}
#endregion
		private void GameOverArt(object sender, EventArgs e)
		{
			
		}
		private void StartGameOverArt()
		{
			
		}

		private void Window_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Space)
				this.Close();
		}

	}
}
