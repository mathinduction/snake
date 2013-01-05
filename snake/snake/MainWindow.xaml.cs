using System.Windows;
using snake.Editor;
using snake.Graphics;

namespace snake
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
		}

		private void buttonStart1_Click(object sender, RoutedEventArgs e)
		{
			Graphics.MainGameWindow gameWindow = new MainGameWindow(false);
			gameWindow.Closing += new System.ComponentModel.CancelEventHandler(gameWindow_Closing);
			gameWindow.Show();
			this.Visibility = Visibility.Collapsed;
		}
		private void buttonStart2_Click(object sender, RoutedEventArgs e)
		{
			Graphics.MainGameWindow gameWindow = new MainGameWindow(true);
			gameWindow.Closing += new System.ComponentModel.CancelEventHandler(gameWindow_Closing);
			gameWindow.Show();
			this.Visibility = Visibility.Collapsed;
		}

		void gameWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			this.Visibility = Visibility.Visible;
		}

		private void buttonEditor_Click(object sender, RoutedEventArgs e)
		{
			Editor.EditorWindow editorWindow = new EditorWindow();
			editorWindow.Closing += new System.ComponentModel.CancelEventHandler(editorWindow_Closing);
			editorWindow.Show();
			this.Visibility = Visibility.Collapsed;
		}

		void editorWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			this.Visibility = Visibility.Visible;
		}
	}
}
