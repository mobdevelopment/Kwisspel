using System.Windows;
using QuizGamePack.ViewModel;
using QuizGamePack.Model;

namespace QuizGamePack
{
    /// <summary>
    /// This application's main window.
    /// </summary>
    public partial class QuizCRUDWindow : Window
    {
        /// <summary>
        /// Initializes a new instance of the MainWindow class.
        /// </summary>
        public QuizCRUDWindow()
        {
            InitializeComponent();
            Closing += (s, e) => ViewModelLocator.Cleanup();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            this.Visibility = Visibility.Hidden;
        }

        private void TextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {

        }
    }
}