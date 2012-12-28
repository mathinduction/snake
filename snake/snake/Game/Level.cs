using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace snake.Game
{
	/// <summary>
	/// Уровень/сцена
	/// </summary>
	public class Level
	{
		private ePixelType[,] _levelPixels = new ePixelType[Common.NumberPixelHeight,Common.NumberPixelWidth];

		/// <summary>
		/// обновляет уровень в соответствии с новыми координатами змейки
		/// </summary>
		public void Update(List<Point> snakeCoords)
		{
			
		}
		public void GenerateFood()
		{
			
		}
		/// <summary>
		/// Стартовая карта
		/// </summary>
		public void Start()
		{
			
		}
#region Properties
		/// <summary>
		/// Все пиксели уровня
		/// </summary>
		public ePixelType[,] LevelPixels
		{
			set { _levelPixels = value; }
			get { return _levelPixels; }
		}
#endregion
	}
}
