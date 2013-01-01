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
		private int _width;
		private int _heigth;
		private ePixelType[,] _levelPixels;
		private Point _startSnakeCoord = new Point();
		private Point _startFoeSnakeCoord = new Point();
		private eKeyPress _snakeStartDirection;
		private eKeyPress _foeSnakeStartDirection;

		public Level(int Width, int Heigth)
		{
			_width = Width;
			_heigth = Heigth;
			_levelPixels = new ePixelType[_width,_heigth];
		}
		/// <summary>
		/// Инициальзирует пустую карту
		/// </summary>
		public void Init()
		{
			for (int i = 0; i < _levelPixels.GetLength(0); i++)
				for (int j = 0; j < _levelPixels.GetLength(1); j++)
				{
					_levelPixels[i, j] = ePixelType.None;
				}
		}
		/// <summary>
		/// Обновляет уровень в соответствии с новыми координатами змейки (isFoeSnake=true -> обновление для змейки-конкурента)
		/// </summary>
		public eSnakeMove Update(List<Point> snakeCoords, bool isFoeSnake)
		{
			ePixelType pt = ePixelType.SnakePart;
			if (isFoeSnake)
				pt = ePixelType.FoeSnakePart;

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
				case ePixelType.FoeSnakePart:
					return eSnakeMove.Died;
				case ePixelType.Food:
					sm = eSnakeMove.Fed;
					break;
			}
			for (int i = 0; i < _levelPixels.GetLength(0); i++)//Убираем старые записи о координатах змейки на карте уровня
				for (int j = 0; j < _levelPixels.GetLength(1); j++)
				{
					if (_levelPixels[i, j] == pt)
						_levelPixels[i, j] = ePixelType.None;
						
				}
			for (int i = 0; i < snakeCoords.Count; i++)//Записываем новые
			{
				_levelPixels[(int) snakeCoords[i].X, (int) snakeCoords[i].Y] = pt;
			}

			return sm;
		}
		/// <summary>
		/// Генерирует "еду"
		/// </summary>
		public void GenerateFood()
		{
			bool good = false;
			int x = 0, y = 0;
			do
			{
				Random coord = new Random(DateTime.Now.Millisecond);
				x = coord.Next(_levelPixels.GetLength(0));
				y = coord.Next(_levelPixels.GetLength(1));
				if (_levelPixels[x, y] == ePixelType.None)
					good = true;
			} while (!good);
			_levelPixels[x, y] = ePixelType.Food;
		}
		/// <summary>
		/// Стартовая карта
		/// </summary>
		public void Start(string levelName)
		{
			string path = Common.PathLevels + "//" + levelName + ".lvl";
			string line;
			System.IO.StreamReader file = new System.IO.StreamReader(path);
			line = file.ReadLine();
			_snakeStartDirection = (eKeyPress)int.Parse(line);
			line = file.ReadLine();
			_foeSnakeStartDirection = (eKeyPress)int.Parse(line);

			line = file.ReadLine();//TODO просто пропскаю 2 строчки
			line = file.ReadLine();

			for (int i = 0; i < _levelPixels.GetLength(0); i++)
				for (int j = 0; j < _levelPixels.GetLength(1); j++)
				{
					if ((line = file.ReadLine()) != null)
					{
						int d = int.Parse(line);
						switch (d)
						{
							case 1:
								_levelPixels[i, j] = ePixelType.Block;
								break;
							case 2:
								_levelPixels[i, j] = ePixelType.Food;
								break;
							case 3:
								_startSnakeCoord = new Point(i, j);
								break;
							case 4:
								_startFoeSnakeCoord = new Point(i, j);
								break;
							default:
								_levelPixels[i, j] = ePixelType.None;
								break;
						}	
					}
					else
						break;
				}
		}
		/// <summary>
		/// Не рабоает!
		/// </summary>
		public void Resize()
		{
			ePixelType[,] levelPixels = new ePixelType[_width, _heigth];
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
		/// Стартовые кооординаты змейки-конкурента
		/// </summary>
		public Point StartFoeSnakeCoord
		{
			set { _startFoeSnakeCoord = value; }
			get { return _startFoeSnakeCoord; }
		}
		/// <summary>
		/// Стартовое направление движения змейки
		/// </summary>
		public eKeyPress SnakeStartDirection
		{
			set { _snakeStartDirection = value; }
			get { return _snakeStartDirection; }
		}
		/// <summary>
		/// Стартовое направление движения змейки-конкурента
		/// </summary>
		public eKeyPress FoeSnakeStartDirection
		{
			set { _foeSnakeStartDirection = value; }
			get { return _foeSnakeStartDirection; }
		}
#endregion
	}
}
