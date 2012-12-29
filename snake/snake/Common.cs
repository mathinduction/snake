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
		None,
		Up,
		Right,
		Down,
		Left
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
#endregion

	/// <summary>
	/// Некоторые константы
	/// </summary>
	public static class Common
	{
#region Private

		private const int _numberPixelHeight = 15;
		private const int _numberPixelWidth = 15;
		private const int _pixelSize = 10;
#endregion

#region Properties
		/// <summary>
		/// Число пикселей в высоту
		/// </summary>
		public static int NumberPixelHeight
		{
			get { return _numberPixelHeight; }
		}
		/// <summary>
		/// Число пикселей в ширину
		/// </summary>
		public static int NumberPixelWidth
		{
			get { return _numberPixelWidth; }
		}
		/// <summary>
		/// Размеры пикселя
		/// </summary>
		public static int PixelSize
		{
			get { return _pixelSize; }
		}
#endregion
	}
}
