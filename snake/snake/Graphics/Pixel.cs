using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;
using System.Windows.Shapes;
//using System.Drawing;

namespace snake.Graphics
{
	/// <summary>
	/// Графическая единица уровня
	/// </summary>
	public static class Pixel
	{
		private static Brush _defaultColor;
		private static Brush _stroke;

		static Pixel()
		{
			_defaultColor = Brushes.White;
			_stroke = Brushes.Black;
		}

		/// <summary>
		/// Рисует пиксель заданного цвета
		/// </summary>
		public static Rectangle DrawPixel(Brush color)
		{
			Rectangle rect = new Rectangle();
			rect.Fill = color;
			rect.Stroke = _stroke;
			rect.Height = Common.PixelSize;
			rect.Width = Common.PixelSize;

			return rect;
		}
	}
}
