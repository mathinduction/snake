using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace snake.AI
{
	public class SnakeAI
	{
		private Game.Level _level;
		private eKeyPress _moveDirection;

		public SnakeAI(Game.Level level, eKeyPress startDirection)
		{
			_level = level;
			_moveDirection = startDirection;
		}
		/// <summary>
		/// Определяет оптимальное направление движения змейки.
		/// </summary>
		public eKeyPress DetermineTheDirection(Point position)
		{
			bool good = false;
			int x, count = 0;
			do
			{
				Random coord = new Random(DateTime.Now.Millisecond);
				x = coord.Next(4);
				Point snakeHead = position;
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
				count++;
			} while (!good && count < 100);
			_moveDirection = (eKeyPress) x;
			return (eKeyPress)x;
		}
	}
}
