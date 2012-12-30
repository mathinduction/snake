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
		None = 0,
		Up = 1,
		Right = 2,
		Down = 3,
		Left = 4
	}
	/// <summary>
	/// Тип пикселя
	/// </summary>
	public enum ePixelType
	{
		None,		//Пустой пиксель
		Block,		//Пиксель с блоком
		Food,		//Пиксель с едой
		SnakePart	//Пиксель с частью змейки
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
		private const int _pixelSize = 20;

		private const int _timeToMove = 1000;

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
		/// Время между двуме сдвигами змейки/обновлениями карты уровня
		/// </summary>
		public static int TimeToMove
		{
			get { return _timeToMove; }
		}
#endregion
	}
}
