using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace snake.Game
{
	class Snake
	{
		private List<Point> _snakeCoords = new List<Point>();
		private int _length;//Текущая длина змейки
#region Public
		public Snake(List<Point> StartSnakeCoords)
		{
			_length = 3;
			_snakeCoords = StartSnakeCoords;
		}

		/// <summary>
		/// Двигаем змейку в выбранном направлении
		/// </summary>
		public void Move(eKeyPress key)
		{
			Point newPart;
			newPart = _snakeCoords[_snakeCoords.Count - 1];
			for (int i = _snakeCoords.Count - 1; i > 0; i--)
			{
				_snakeCoords[i] = _snakeCoords[i - 1];
			}
			switch (key)
			{
				case eKeyPress.Down:
					Point p1 = new Point(_snakeCoords[0].X, _snakeCoords[0].Y + 1);
					_snakeCoords[0] = p1;
					break;
				case eKeyPress.Left:
					Point p2 = new Point(_snakeCoords[0].X - 1, _snakeCoords[0].Y);
					_snakeCoords[0] = p2;
					break;
				case eKeyPress.Right:
					Point p3 = new Point(_snakeCoords[0].X + 1, _snakeCoords[0].Y);
					_snakeCoords[0] = p3;
					break;
				case eKeyPress.Up:
					Point p4 = new Point(_snakeCoords[0].X, _snakeCoords[0].Y - 1);
					_snakeCoords[0] = p4;
					break;
			}
			if (_length > _snakeCoords.Count)//Если змейка увеличилась в длине
			{
				_snakeCoords.Add(newPart);
			}
		}
		/// <summary>
		/// Установка в стартовые координаты - середину экрана
		/// </summary>
		public void ToTheStart()
		{
			//TODO уже не нужная ф-я?
		}
		/// <summary>
		/// Увеличение длины змейки
		/// </summary>
		public void LengthUp()
		{
			_length++;
		}
#endregion
#region Properties
		/// <summary>
		/// Текущие координаты змейки
		/// </summary>
		public List<Point> SnakeCoordinates
		{
			get { return _snakeCoords; }
		}
#endregion
	}
}
