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
			int [,] level = new int[,]//TODO хардкод
			                	{
			{1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
			{1,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
			{1,0,0,0,0,0,1,0,0,0,0,0,0,0,1},
			{1,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
			{1,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
			{1,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
			{1,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
			{1,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
			{1,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
			{1,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
			{1,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
			{1,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
			{1,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
			{1,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
			{1,1,1,1,1,1,1,1,1,1,1,1,1,1,1}
			                	}; 

			for (int i = 0; i < Common.NumberPixelHeight; i++)
				for (int j = 0; j < Common.NumberPixelWidth; j++)
				{
					if (level[i, j] == 1)
						_levelPixels[i, j] = ePixelType.Block;
					else
						_levelPixels[i, j] = ePixelType.None;
				}
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
