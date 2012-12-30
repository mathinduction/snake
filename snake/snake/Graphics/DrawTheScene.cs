using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
//using System.Drawing;

namespace snake.Graphics
{
	/// <summary>
	/// Класс для отрисовки сцены
	/// </summary>
	public static class DrawTheScene
	{
		private static Point[,] _levelPixelCoord = new Point[Common.NumberPixelWidth, Common.NumberPixelHeight];//Координаты каждого пикселя в окне
		private static Brush _colorNone = Brushes.Black;//Цвет пустого пикселя
		private static Brush _colorBlock = Brushes.White;//Цвет пикселя с блоком
		private static Brush _colorFood = Brushes.Yellow;//Цвет пикселя с едой
		private static Brush _colorSnake = Brushes.YellowGreen;//Цвет пикселя с частью змейки

		static DrawTheScene()
		{
			for (int i = 0; i < Common.NumberPixelWidth; i++)
			{
				int x = i*Common.PixelSize;
				for (int j = 0; j < Common.NumberPixelHeight; j++)
				{
					int y = j*Common.PixelSize;
					_levelPixelCoord[i,j] = new Point(x,y);
				}
			}
		}

		/// <summary>
		/// Отрисовка
		/// </summary>
		public static void Draw(Game.Level level, ref Canvas canvas)
		{
			for (int i = 0; i < level.LevelPixels.GetLength(0); i++)
				for (int j = 0; j < level.LevelPixels.GetLength(1); j++)
				{
					Rectangle rect = new Rectangle();
					switch (level.LevelPixels[i, j])
					{
						case ePixelType.None:
							rect = Pixel.DrawPixel(_levelPixelCoord[i, j].X, _levelPixelCoord[i, j].Y, _colorNone);
							break;
						case ePixelType.Block:
							rect = Pixel.DrawPixel(_levelPixelCoord[i, j].X, _levelPixelCoord[i, j].Y, _colorBlock);
							break;
						case ePixelType.Food:
							rect = Pixel.DrawPixel(_levelPixelCoord[i, j].X, _levelPixelCoord[i, j].Y, _colorFood);
							break;
						case ePixelType.SnakePart:
							rect = Pixel.DrawPixel(_levelPixelCoord[i, j].X, _levelPixelCoord[i, j].Y, _colorSnake);
							break;
					}
					canvas.Children.Add(rect);
				}
		}
	}
}
