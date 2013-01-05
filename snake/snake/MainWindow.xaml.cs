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
			gameWindow.Closing += new System.ComponentModel.CancelEventHandler(Window_Closing);
			gameWindow.Show();
			this.Visibility = Visibility.Collapsed;
		}
		private void buttonStart2_Click(object sender, RoutedEventArgs e)
		{
			Graphics.MainGameWindow gameWindow = new MainGameWindow(true);
			gameWindow.Closing += new System.ComponentModel.CancelEventHandler(Window_Closing);
			gameWindow.Show();
			this.Visibility = Visibility.Collapsed;
		}

		private void buttonEditor_Click(object sender, RoutedEventArgs e)
		{
			Editor.EditorWindow editorWindow = new EditorWindow();
			editorWindow.Closing += new System.ComponentModel.CancelEventHandler(Window_Closing);
			editorWindow.Show();
			this.Visibility = Visibility.Collapsed;
		}

		private void buttonSettings_Click(object sender, RoutedEventArgs e)
		{
			SettingsWindow settingsWindow = new SettingsWindow();
			settingsWindow.Closing += new System.ComponentModel.CancelEventHandler(Window_Closing);
			settingsWindow.Show();
			this.Visibility = Visibility.Collapsed;
		}

		void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			this.Visibility = Visibility.Visible;
		}
	}
}
