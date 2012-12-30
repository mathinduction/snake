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
		private ePixelType[,] _levelPixels = new ePixelType[Common.NumberPixelWidth, Common.NumberPixelHeight];
		private Point _startSnakeCoord = new Point();
		private eKeyPress _snakeStartDirection;

		/// <summary>
		/// обновляет уровень в соответствии с новыми координатами змейки
		/// </summary>
		public eSnakeMove Update(List<Point> snakeCoords)
		{
			eSnakeMove sm = eSnakeMove.Normal;
			switch (_levelPixels[(int)snakeCoords[0].X, (int)snakeCoords[0].Y])
			{
				case ePixelType.None:
					sm = eSnakeMove.Normal;
					break;
				case ePixelType.Block:
					return eSnakeMove.Died;
				case ePixelType.SnakePart:
					return eSnakeMove.Died;
				case ePixelType.Food:
					sm = eSnakeMove.Fed;
					break;
			}
			for (int i = 0; i < _levelPixels.GetLength(0); i++)//Убираем старые записи о координатах змейки на карте уровня
				for (int j = 0; j < _levelPixels.GetLength(0); j++)
				{
					if (_levelPixels[i, j] == ePixelType.SnakePart)
						_levelPixels[i, j] = ePixelType.None;
						
				}
			for (int i = 0; i < snakeCoords.Count; i++)//Записываем новые
			{
				_levelPixels[(int) snakeCoords[i].X, (int) snakeCoords[i].Y] = ePixelType.SnakePart;
			}

			return sm;
		}
		public void GenerateFood()
		{
			
		}
		/// <summary>
		/// Инициальзирует пустую карту
		/// </summary>
		public void Init()
		{
			for (int i = 0; i < _levelPixels.GetLength(0); i++)
				for (int j = 0; j < _levelPixels.GetLength(1); j++)
				{
					_levelPixels[i,j] = ePixelType.None;
				}
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

			_startSnakeCoord = new Point(7, 11);

			_snakeStartDirection = eKeyPress.Up;
		}

		public void Resize()
		{
			ePixelType[,] levelPixels = new ePixelType[Common.NumberPixelHeight, Common.NumberPixelWidth];
			for (int i = 0; i < levelPixels.GetLength(0); i++)
				for (int j = 0; j < levelPixels.GetLength(1); j++)
				{
					if (i < _levelPixels.GetLength(0) && j < _levelPixels.GetLength(1))
					{
						levelPixels[i, j] = _levelPixels[i,j];
					}
				}
			_levelPixels = levelPixels;
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
		/// <summary>
		/// Стартовые кооординаты змейки
		/// </summary>
		public Point StartSnakeCoord
		{
			set { _startSnakeCoord = value; }
			get { return _startSnakeCoord; }
		}
		/// <summary>
		/// Стартовое направление движения змейки
		/// </summary>
		public eKeyPress SnakeStartDirection
		{
			set { _snakeStartDirection = value; }
			get { return _snakeStartDirection; }
		}
#endregion
	}
}
