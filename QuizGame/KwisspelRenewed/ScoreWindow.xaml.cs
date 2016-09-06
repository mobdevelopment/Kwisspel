using System.Windows;
using KwisspelRenewed.ViewModel;

namespace KwisspelRenewed
{
    /// <summary>
    /// This application's main window.
    /// </summary>
    public partial class ScoreWindow : Window
    {
        /// <summary>
        /// Initializes a new instance of the MainWindow class.
        /// </summary>
        public ScoreWindow()
        {
            InitializeComponent();
            Closing += (s, e) => ViewModelLocator.Cleanup();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            System.Environment.Exit(0);
        }
    }
}