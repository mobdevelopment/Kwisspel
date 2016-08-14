using System.Windows;
using QuizGamePack.ViewModel;

namespace QuizGamePack
{
    /// <summary>
    /// This application's main window.
    /// </summary>
    public partial class QuestionWindow : Window
    {
        /// <summary>
        /// Initializes a new instance of the MainWindow class.
        /// </summary>
        public QuestionWindow()
        {
            InitializeComponent();
            Closing += (s, e) => ViewModelLocator.Cleanup();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            this.Visibility = Visibility.Hidden;
        }
    }
}