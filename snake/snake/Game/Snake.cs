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
#region Public
		/// <summary>
		/// Двигаем змейку в выбранном направлении
		/// </summary>
		public void Move(eKeyPress key)
		{
			//TODO должен возвращать результат передвижения
		}
		/// <summary>
		/// Установка в стартовые координаты - середину экрана
		/// </summary>
		public void ToTheStart()
		{
			
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
