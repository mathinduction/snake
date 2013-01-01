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
	public  class DrawTheScene
	{
		private Point[,] _levelPixelCoord;//Координаты каждого пикселя в окне
		private Brush _colorNone = Brushes.Black;//Цвет пустого пикселя
		private Brush _colorBlock = Brushes.White;//Цвет пикселя с блоком
		private Brush _colorFood = Brushes.Yellow;//Цвет пикселя с едой
		private Brush _colorSnake = Brushes.YellowGreen;//Цвет пикселя с частью змейки
		private Brush _colorFoeSnake = Brushes.Red;//Цвет пикселя с частью змейки-конкурента

		public DrawTheScene(int width, int heigth)
		{
			CalculateCoords(width, heigth);
		}
		/// <summary>
		/// Отрисовка.
		/// </summary>
		public void Draw(Game.Level level, ref Canvas canvas)
		{
			if (_levelPixelCoord == null) return;
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
						case ePixelType.FoeSnakePart:
							rect = Pixel.DrawPixel(_levelPixelCoord[i, j].X, _levelPixelCoord[i, j].Y, _colorFoeSnake);
							break;
					}
					canvas.Children.Add(rect);
				}
		}
		/// <summary>
		/// Вычисляем координаты пикселей в окне заданого размера
		/// </summary>
		private void CalculateCoords(int width, int heigth)
		{
			_levelPixelCoord = new Point[width,heigth];

			for (int i = 0; i < _levelPixelCoord.GetLength(0); i++)
			{
				int x = i * Common.PixelSize;
				for (int j = 0; j < _levelPixelCoord.GetLength(1); j++)
				{
					int y = j * Common.PixelSize;
					_levelPixelCoord[i, j] = new Point(x, y);
				}
			}
		}

		/// <summary>
		/// Функция для дебага. Рисует указанный пиксель указанным цветом
		/// </summary>
		public void DrawPixel(int x, int y, Brush color, ref Canvas canvas)//TEMP
		{
			Rectangle rect = new Rectangle();
			rect = Pixel.DrawPixel(_levelPixelCoord[x, y].X, _levelPixelCoord[x, y].Y, color);
			canvas.Children.Add(rect);
		}
	}
}
