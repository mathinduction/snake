using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace snake
{
#region Enum
	/// <summary>
	/// Какая клавиша управления нажата
	/// </summary>
	public enum eKeyPress
	{
		Up = 0,
		Right = 1,
		Down = 2,
		Left = 3
	}
	/// <summary>
	/// Тип пикселя
	/// </summary>
	public enum ePixelType
	{
		None = 0,		//Пустой пиксель
		Block = 1,		//Пиксель с блоком
		Food = 2,		//Пиксель с едой
		SnakePart = 3,	//Пиксель с частью змейки
		FoeSnakePart = 4//Пиксель с частью змейки-конкурента
	}
	/// <summary>
	/// Результат движение змейки
	/// </summary>
	public enum eSnakeMove
	{
		Normal,	//Обычное движение
		Died,	//Врезался в блок/часть змейки
		Fed		//Съел еду
	}
#endregion

	/// <summary>
	/// Некоторые константы
	/// </summary>
	public static class Common
	{
#region Private

		private static int _numberPixelHeight = 20;
		private static int _numberPixelWidth = 15;
		private const int _pixelSize = 15;

		private const int _timeToMove = 600;
		private const int _speedUp = 3;
		private static bool _holdKey = false;

		private const string _pathLevels = "Levels";

		private const int _recursionDepth = 10;

#endregion

#region Properties
		/// <summary>
		/// Число пикселей в высоту
		/// </summary>
		public static int NumberPixelHeight
		{
			set { _numberPixelHeight = value; }
			get { return _numberPixelHeight; }
		}
		/// <summary>
		/// Число пикселей в ширину
		/// </summary>
		public static int NumberPixelWidth
		{
			set { _numberPixelWidth = value; }
			get { return _numberPixelWidth; }
		}
		/// <summary>
		/// Размеры пикселя
		/// </summary>
		public static int PixelSize
		{
			get { return _pixelSize; }
		}
		/// <summary>
		/// Время между двумя ходами змейки игрока
		/// </summary>
		public static int TimeToMovePlayer
		{
			get
			{
				if (_holdKey)
					return _timeToMove / _speedUp;
				else
					return _timeToMove;
			}
		}
		/// <summary>
		/// Время между двумя ходами змейки-ИИ
		/// </summary>
		public static int TimeToMoveAI
		{
			get { return _timeToMove; }
		}
		/// <summary>
		/// Во сколько раз увеличивается скорость при ускорении
		/// </summary>
		public static int SpeedUp
		{
			get { return _speedUp; }
		}
		/// <summary>
		/// Включено ли ускорение
		/// </summary>
		public static bool HoldKey
		{
			set { _holdKey = value; }
			get { return _holdKey; }
		}
		/// <summary>
		/// Название папки, где хранятся файлы с картами уровня
		/// </summary>
		public static string PathLevels
		{
			get { return _pathLevels; }
		}
		/// <summary>
		/// Глубина рекурсии
		/// </summary>
		public static int RecursionDepth
		{
			get { return _recursionDepth; }
		}
#endregion
	}
}
