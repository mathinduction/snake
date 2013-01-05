using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using snake.AI;
using snake.Game;

namespace snake.Graphics
{
	/// <summary>
	/// Interaction logic for MainGameWindow.xaml
	/// </summary>
	public partial class MainGameWindow : Window
	{
#region Private

		private bool _useAI = false;//Режим игры: true-есть змейка-конкурент, false-её нет.
		private Game.Snake _snake;//Змейка
		private Game.Snake _foeSnake;//Змейка-конкурент
		private AI.SnakeAI _snakeAi;
		private Game.Level _level;//Уровень
		private eKeyPress _keyPress;//Нажатая клавиша управления
		private eKeyPress _moveDirection;//Направление движения змейки
		private long _frameTimePlayer = 0;//Число милисекунд, прошедших с последнего хода игрока
		private long _frameTimeAI = 0;//Число милисекунд, прошедших с последнего ходи ИИ
		private bool _holdKey = false;//Флаг удержания клавиши. Нужен для включения ускорения
		private bool _pause = false;//Включена ли пауза
		private int _victoryPoints = 0;//Победные очки
		private int _victoryPointsFoe = 0;//Победные очки змейки-конкурента
		private DrawTheScene _drawer;//Рисовалищик
		private PixelArt _pixelArt;
#endregion
		public MainGameWindow(bool useAI)
		{
			InitializeComponent();
			_useAI = useAI;

			CompositionTarget.Rendering += OnFrame;
			canvasGame.Width = Common.NumberPixelWidth * Common.PixelSize;
			canvasGame.Height = Common.NumberPixelHeight * Common.PixelSize;

			_drawer = new DrawTheScene(Common.NumberPixelWidth, Common.NumberPixelHeight);

			textBlockVPFoe.Visibility = Visibility.Hidden;
			textBlockVPointsFoe.Visibility = Visibility.Hidden;
			StartGame();
		}

#region Events
		private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			CompositionTarget.Rendering -= OnFrame;
		}
		/// <summary>
		/// Смена кадра
		/// </summary>
		private void OnFrame(object sender, EventArgs e)
		{
			if (_pause) return;
			
			DateTime dt = DateTime.Now;
			long t = dt.Millisecond + dt.Second*1000 + dt.Minute*60*1000 + dt.Hour*60*60*1000;
			bool player=false, AI = false;
			//Отдельно для каждой змейки определяем, когда она должна ходить. 
			//Нужно для того,что бы ускорение змейки-игрока не влияло на змейку-ИИ.
			if ((t - _frameTimePlayer) > Common.TimeToMovePlayer) 
			{
				player = true;
				_frameTimePlayer = t;
			}
			if ((t - _frameTimeAI) > Common.TimeToMoveAI)
			{
				AI = true;
				_frameTimeAI = t;
			}
			if (!player && !AI) return;

			canvasGame.Children.Clear();
			if (player)
			{
				#region Player-snake

				_moveDirection = _keyPress;
				_snake.Move(_moveDirection); //Двигаем змейку
				eSnakeMove sm = _level.Update(_snake.SnakeCoordinates, false); //Обновляем карту уровня
				switch (sm) //Учитываем результат движения змейки
				{
					case eSnakeMove.Normal:
						break;
					case eSnakeMove.Died:
						MessageBox.Show("GameOver! Fail!");
						this.Close();
						break;
					case eSnakeMove.Fed:
						_snake.LengthUp();
						_level.GenerateFood();
						_victoryPoints++;
						textBlockVPoints.Text = _victoryPoints.ToString();
						break;
				}

				#endregion
			}
			if (AI)
			{
				#region Snake-AI

				if (_useAI)
				{
					//_foeSnake.Move(_snakeAi.DetermineTheDirection(_foeSnake.SnakeCoordinates[0]));
					_foeSnake.Move(_snakeAi.DetermineTheDirectionRecursive(_foeSnake.SnakeCoordinates[0]));
					eSnakeMove smF = _level.Update(_foeSnake.SnakeCoordinates, true);
					switch (smF) //Учитываем результат движения змейки-конкурента
					{
						case eSnakeMove.Normal:
							break;
						case eSnakeMove.Died:
							MessageBox.Show("GameOver! Win!");
							this.Close();
							break;
						case eSnakeMove.Fed:
							_foeSnake.LengthUp();
							_level.GenerateFood();
							_victoryPointsFoe++;
							textBlockVPointsFoe.Text = _victoryPointsFoe.ToString();
							break;
					}
				}

				#endregion
			}
			_drawer.Draw(_level, ref canvasGame);//Перерисовываем сцену
		}

		private void Window_KeyDown(object sender, KeyEventArgs e)
		{
			bool good = false;
			switch (e.Key)
			{
				case Key.Up:
					if (_moveDirection != eKeyPress.Down)
					{
						_keyPress = eKeyPress.Up;
						good = true;
					}
					break;
				case Key.Down:
					if (_moveDirection != eKeyPress.Up)
					{
						_keyPress = eKeyPress.Down;
						good = true;
					}
					break;
				case Key.Left:
					if (_moveDirection != eKeyPress.Right)
					{
						_keyPress = eKeyPress.Left;
						good = true;
					}
					break;
				case Key.Right:
					if (_moveDirection != eKeyPress.Left)
					{
						_keyPress = eKeyPress.Right;
						good = true;
					}
					break;
				case Key.Space:
					Pause();
					break;
			}
			if (good)
			{
				if (_holdKey)
				{
					Common.HoldKey = true;//Включаем ускорение
				}
				else
				{
					_holdKey = true;
				}
			}
		}
		private void Window_KeyUp(object sender, KeyEventArgs e)
		{
			_holdKey = false;
			Common.HoldKey = false;
		}

		private void Window_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
		{
			Pause(true);
		}
		private void Window_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
		{
			Pause(false);
		}
#endregion

#region Private	
		/// <summary>
		/// Установление начальных параметров игры
		/// </summary>
		private void StartGame()
		{
			_victoryPoints = 0;
			_victoryPointsFoe = 0;

			_level = new Level(Common.NumberPixelWidth, Common.NumberPixelHeight);
			_level.Start("Level1");//Стартовая карта уровня //TODO хардкод
			_keyPress = _level.SnakeStartDirection;
			_snake = new Snake(_level.StartSnakeCoord, _level.SnakeStartDirection);//Стартовое положение змейки
			_level.Update(_snake.SnakeCoordinates, false);//Учитываем положение змейки
			if (_useAI)
			{
				textBlockVPFoe.Visibility = Visibility.Visible;
				textBlockVPointsFoe.Visibility = Visibility.Visible;
				_foeSnake = new Snake(_level.StartFoeSnakeCoord, _level.FoeSnakeStartDirection);//Стартовое положение змейки-конкурента
				_level.Update(_foeSnake.SnakeCoordinates, true);
				_snakeAi = new SnakeAI(_level, _level.FoeSnakeStartDirection);
			}
			_level.GenerateFood();//Генерируем еду
			_drawer.Draw(_level, ref canvasGame);//Рисуем сцену

			DateTime dt = DateTime.Now;
			long t = dt.Millisecond + dt.Second * 1000 + dt.Minute * 60 * 1000 + dt.Hour * 60 * 60 * 1000;
			_frameTimePlayer = t;
			_frameTimeAI = t;
		}
		/// <summary>
		/// Пауза
		/// </summary>
		private void Pause()
		{
			Pause(!_pause);
		}
		private void Pause(bool pause)
		{
			if (_pause && pause) return;
			_pause = pause;
			if (_pause)
			{
				//_pixelArt = new PixelArt(PixelArt.eArts.Pause);
				//_pixelArt.Closed += new EventHandler(_pixelArt_Closed);
				//_pixelArt.Show();
				//this.Visibility = Visibility.Collapsed;
			}
			else
			{	
				
			}
		}

		void _pixelArt_Closed(object sender, EventArgs e)
		{
			if (_pixelArt != null)
				_pixelArt.StopPauseArt();
			this.Visibility = Visibility.Visible;
		}
#endregion
	}
}
