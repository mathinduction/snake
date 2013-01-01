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
//#define _DEBUG  //Вкл/выкл дебажный режим
			bool good = false;
			eKeyPress direction;
			Point snakeHead = position;
			int [] directionRank = new int[4];//Массив оценок направлений, каждый элемент соответствует одному направлению

			using (System.IO.StreamWriter file = new System.IO.StreamWriter("debug.txt", true))
			{
#if _DEBUG
				file.WriteLine(snakeHead.X);
				file.WriteLine(snakeHead.Y);
#endif
			//Рассматриваем все 3 возможных напрвления и выбираем лучшее
			#region Up
			if (_moveDirection != eKeyPress.Down)
			{
				directionRank[0] = DirectionRank(new Point(snakeHead.X, snakeHead.Y - 1));
			}
			else
				directionRank[0] = -1;//Нельзя выбрать это направление
#if _DEBUG
			file.WriteLine("Up");
			file.WriteLine(snakeHead.X);
			file.WriteLine(snakeHead.Y - 1);
			file.WriteLine(directionRank[0]);
#endif
			#endregion
			#region Right
			if (_moveDirection != eKeyPress.Left)
			{
				directionRank[1] = DirectionRank(new Point(snakeHead.X + 1, snakeHead.Y));
			}
			else
				directionRank[1] = -1;//Нельзя выбрать это направление
#if _DEBUG
			file.WriteLine("Right");
			file.WriteLine(snakeHead.X + 1);
			file.WriteLine(snakeHead.Y);
			file.WriteLine(directionRank[1]);
#endif
			#endregion
			#region Down
			if (_moveDirection != eKeyPress.Up)
			{
				directionRank[2] = DirectionRank(new Point(snakeHead.X, snakeHead.Y + 1));
			}
			else
				directionRank[2] = -1;//Нельзя выбрать это направление
#if _DEBUG
			file.WriteLine("Down");
			file.WriteLine(snakeHead.X);
			file.WriteLine(snakeHead.Y + 1);
			file.WriteLine(directionRank[2]);
#endif
			#endregion
			#region Left
			if (_moveDirection != eKeyPress.Right)
			{
				directionRank[3] = DirectionRank(new Point(snakeHead.X - 1, snakeHead.Y));
			}
			else
				directionRank[3] = -1;//Нельзя выбрать это направление
#if _DEBUG
			file.WriteLine("Left");
			file.WriteLine(snakeHead.X - 1);
			file.WriteLine(snakeHead.Y);
			file.WriteLine(directionRank[3]);
#endif
			#endregion

			//Если несколько направлений с одинаковой оценкой, то выбираем случайное из них.
			List<eKeyPress> d = new List<eKeyPress>();
			int max = directionRank.Max();
			for (int i = 0; i < 4; i++)
			{
				if (directionRank[i] == max)
					d.Add((eKeyPress)i);
			}
			Random coord = new Random(DateTime.Now.Millisecond);
			_moveDirection = d[coord.Next(d.Count)];
#if _DEBUG
			file.WriteLine(_moveDirection);
			file.WriteLine();
#endif
			return _moveDirection;
			}
		}

		private int DirectionRank(Point position)
		{
			if (_level.LevelPixels[(int)position.X, (int)position.Y] == ePixelType.Block ||
					_level.LevelPixels[(int)position.X, (int)position.Y] == ePixelType.SnakePart ||
					_level.LevelPixels[(int)position.X, (int)position.Y] == ePixelType.FoeSnakePart)
				return 0;//Самое нежелательное направление, т.к. там ожидается столкновение //TODO анализ того,что если это последняя часть змеи,то она может сдвинуться
			if (_level.LevelPixels[(int)position.X, (int)position.Y] == ePixelType.Food)
				return 5;//Самое желательное направление
			//if (_level.LevelPixels[(int)position.X, (int)position.Y] == ePixelType.None)
				return 2;//Возможное нправление //TODO анализ в зависимости от близости еды
		}
	}
}
