using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace snake.AI
{
	public class SnakeAI
	{
#region Private
		/// <summary>
		/// То,чем закончилось рекурсивное просматривание ветки
		/// </summary>
		private enum eEndRecursion
		{
			Food,//Нашли еду
			Block,//Тупик
			Unknown//Вышли по оганичению на глубину рекурсии
		}
		private Game.Level _level;
		private eKeyPress _moveDirection;
#endregion
		
#region Public
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
			Point snakeHead = position;
			int[] directionRank = new int[4];//Массив оценок направлений, каждый элемент соответствует одному направлению

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

		/// <summary>
		/// Определяет оптимальное направление движения змейки (рекурсивно).
		/// </summary>
		public eKeyPress DetermineTheDirectionRecursive(Point position)
		{
			//#define _DEBUG  //Вкл/выкл дебажный режим
			Point snakeHead = position;
			List<Tuple<eEndRecursion, int>> rank = new List<Tuple<eEndRecursion, int>>();
			int[] directionRank = new int[4];//Массив оценок направлений, каждый элемент соответствует одному направлению

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
					DirectionRankRecursive(new Point(snakeHead.X, snakeHead.Y - 1), eKeyPress.Up, 0, ref rank);
					directionRank[0] = MakeRank(rank);
					rank.Clear();
				}
				else
					directionRank[0] = Common.RecursionDepth + 3; //Нельзя выбрать это направление
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
					DirectionRankRecursive(new Point(snakeHead.X + 1, snakeHead.Y), eKeyPress.Right, 0, ref rank);
					directionRank[1] = MakeRank(rank);
					rank.Clear();
				}
				else
					directionRank[1] = Common.RecursionDepth + 3; //Нельзя выбрать это направление
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
					DirectionRankRecursive(new Point(snakeHead.X, snakeHead.Y + 1), eKeyPress.Down, 0, ref rank);
					directionRank[2] = MakeRank(rank);
					rank.Clear();
				}
				else
					directionRank[2] = Common.RecursionDepth + 3; //Нельзя выбрать это направление
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
					DirectionRankRecursive(new Point(snakeHead.X - 1, snakeHead.Y), eKeyPress.Left, 0, ref rank);
					directionRank[3] = MakeRank(rank);
					rank.Clear();
				}
				else
					directionRank[3] = Common.RecursionDepth + 3; //Нельзя выбрать это направление
#if _DEBUG
			file.WriteLine("Left");
			file.WriteLine(snakeHead.X - 1);
			file.WriteLine(snakeHead.Y);
			file.WriteLine(directionRank[3]);
#endif

				#endregion

				//Если несколько направлений с одинаковой оценкой, то выбираем случайное из них.
				List<eKeyPress> d = new List<eKeyPress>();
				int min = directionRank.Min();
				for (int i = 0; i < 4; i++)
				{
					if (directionRank[i] == min)
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
#endregion

#region Private
		/// <summary>
		/// Возвращает оценку указанной позиции, т.е. стоит ли передвигаться в неё.
		/// </summary>
		private int DirectionRank(Point position)
		{
			if (_level.LevelPixels[(int)position.X, (int)position.Y] == ePixelType.Block ||
					_level.LevelPixels[(int)position.X, (int)position.Y] == ePixelType.SnakePart ||
					_level.LevelPixels[(int)position.X, (int)position.Y] == ePixelType.FoeSnakePart)
				return 0;//Самое нежелательное направление, т.к. там ожидается столкновение 
			if (_level.LevelPixels[(int)position.X, (int)position.Y] == ePixelType.Food)
				return 5;//Самое желательное направление
			//if (_level.LevelPixels[(int)position.X, (int)position.Y] == ePixelType.None)
			return 2;//Возможное нправление
		}

		/// <summary>
		/// Возвращает оценку указанного направления, куда перемещаемся из указанной позиции. 
		/// (Оценка рекурсивна, count - глубина рекурсии; rank - список оценок для этого направления: Item1-результат, Item2-число шагов)
		/// </summary>
		private void DirectionRankRecursive(Point position, eKeyPress direction, int count, ref List<Tuple<eEndRecursion, int>> rank)
		{
			if (!(position.X >= 0 && //Проверка на корректность координат
				position.Y >= 0 &&
				position.X < _level.LevelPixels.GetLength(0) &&
				position.Y < _level.LevelPixels.GetLength(1)))
			{
				rank.Add(new Tuple<eEndRecursion, int>(eEndRecursion.Block, count));
				return;
			}

			if (_level.LevelPixels[(int)position.X, (int)position.Y] == ePixelType.Block ||
					_level.LevelPixels[(int)position.X, (int)position.Y] == ePixelType.SnakePart ||
					_level.LevelPixels[(int)position.X, (int)position.Y] == ePixelType.FoeSnakePart)
			{
				rank.Add(new Tuple<eEndRecursion, int>(eEndRecursion.Block, count));
				return; //Самое нежелательное направление, т.к. там ожидается столкновение 
				//TODO анализ того,что если это последняя часть змеи,то она может сдвинуться
			}

			if (_level.LevelPixels[(int)position.X, (int)position.Y] == ePixelType.Food)
			{
				rank.Add(new Tuple<eEndRecursion, int>(eEndRecursion.Food, count));
				return; //Самое желательное направление 
				//TODO а вдруго сразу потом врежемся?
			}

			if (count >= Common.RecursionDepth)
			{
				rank.Add(new Tuple<eEndRecursion, int>(eEndRecursion.Unknown, count));
				return; //Возможное направление
			}

			#region Up
			if (_moveDirection != eKeyPress.Down)
			{
				DirectionRankRecursive(new Point(position.X, position.Y - 1), eKeyPress.Up, count + 1, ref rank);
			}

			#endregion
			#region Right
			if (_moveDirection != eKeyPress.Left)
			{
				DirectionRankRecursive(new Point(position.X + 1, position.Y), eKeyPress.Right, count + 1, ref rank);
			}

			#endregion
			#region Down
			if (_moveDirection != eKeyPress.Up)
			{
				DirectionRankRecursive(new Point(position.X, position.Y + 1), eKeyPress.Down, count + 1, ref rank);
			}

			#endregion
			#region Left
			if (_moveDirection != eKeyPress.Right)
			{
				DirectionRankRecursive(new Point(position.X - 1, position.Y), eKeyPress.Left, count + 1, ref rank);
			}
			#endregion
		}
		/// <summary>
		/// Анализирует спик оценок направления. Выделяет наилучший вариант(длину кратчайшего пути до еды в этом направлении).
		/// </summary>
		private int MakeRank(List<Tuple<eEndRecursion, int>> rank)
		{
			int rez;
			bool isUnknown = false;
			rez = Common.RecursionDepth + 2;
			foreach (var tuple in rank)
			{
				if (tuple.Item1 == eEndRecursion.Food)
				{
					if (rez > tuple.Item2)
						rez = tuple.Item2;
				}
				if (tuple.Item1 == eEndRecursion.Unknown)
					isUnknown = true;
			}

			//Это для того,что если в данном направлении не нашли еды,то ситуации,когда все пути заканчиваются тупиками 
			//были менее предпочтительны,чем те, где был хотя бы 1 выход по ограничению на глубину рекурсии, 
			//т.е. хотя бы одна возможноть не врезеаться
			if (rez == (Common.RecursionDepth + 2) && isUnknown)
				rez = Common.RecursionDepth + 1;
			return rez;
		}
		
#endregion
		
		
		
		
	}
}
