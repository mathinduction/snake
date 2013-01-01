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
		public Snake(Point startSnakeCoord, eKeyPress direction)
		{
			_length = 3;
			_snakeCoords.Add(startSnakeCoord);
			switch (direction)
			{
				case eKeyPress.Down:
					_snakeCoords.Add(new Point(startSnakeCoord.X, startSnakeCoord.Y - 1));
					_snakeCoords.Add(new Point(startSnakeCoord.X, startSnakeCoord.Y - 2));
					break;
				case eKeyPress.Left:
					_snakeCoords.Add(new Point(startSnakeCoord.X + 1, startSnakeCoord.Y));
					_snakeCoords.Add(new Point(startSnakeCoord.X + 2, startSnakeCoord.Y));
					break;
				case eKeyPress.Right:
					_snakeCoords.Add(new Point(startSnakeCoord.X - 1, startSnakeCoord.Y));
					_snakeCoords.Add(new Point(startSnakeCoord.X - 2, startSnakeCoord.Y));
					break;
				case eKeyPress.Up:
					_snakeCoords.Add(new Point(startSnakeCoord.X, startSnakeCoord.Y + 1));
					_snakeCoords.Add(new Point(startSnakeCoord.X, startSnakeCoord.Y + 2));
					break;
			}
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
			switch (key)//Выставляем новую координату для "головы"
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
			//Для "порталов" по караям экрана
			if (_snakeCoords[0].X < 0)
			{
				_snakeCoords[0] = new Point(Common.NumberPixelWidth - 1, _snakeCoords[0].Y);
			}
			if (_snakeCoords[0].X >= Common.NumberPixelWidth)
			{
				_snakeCoords[0] = new Point(0, _snakeCoords[0].Y);
			}
			if (_snakeCoords[0].Y < 0)
			{
				_snakeCoords[0] = new Point(_snakeCoords[0].X, Common.NumberPixelHeight - 1);
			}
			if (_snakeCoords[0].Y >= Common.NumberPixelHeight)
			{
				_snakeCoords[0] = new Point(_snakeCoords[0].X, 0);
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
			set { _snakeCoords = value; }
			get { return _snakeCoords; }
		}
#endregion
	}
}
