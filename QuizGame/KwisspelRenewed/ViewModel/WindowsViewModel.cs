using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace KwisspelRenewed.ViewModel
{
    public class WindowsViewModel : ViewModelBase
    {
        private QuestionWindow _questionWindow;
        private QuizCRUDWindow _quizCRUDWindow;
        private ScoreWindow _scoreWindow;

        public ICommand ShowQuestionWindowCommand { get; set; }

        public ICommand ShowQuizCRUDWindowCommand { get; set; }

        public ICommand ShowScoreWindowCommand { get; set; }

        public WindowsViewModel()
        {
            _questionWindow = new QuestionWindow();
            _quizCRUDWindow = new QuizCRUDWindow();
            _scoreWindow = new ScoreWindow();

            ShowQuestionWindowCommand = new RelayCommand(showQuestionWindow, canShowQuestionWindow);
            ShowQuizCRUDWindowCommand = new RelayCommand(showQuizCRUDWindow, canShowQuizCRUDWindow);
            ShowScoreWindowCommand = new RelayCommand(showScoreWindowCommand, canShowScoreWindow);
        }


        private bool canShowQuestionWindow()
        {
            return _questionWindow.IsVisible == false;
        }

        private void showQuestionWindow()
        {
            _questionWindow.Show();
        }

        private bool canShowQuizCRUDWindow()
        {
            return _quizCRUDWindow.IsVisible == false;
        }

        private void showQuizCRUDWindow()
        {
            _quizCRUDWindow.Show();
        }

        private bool canShowScoreWindow()
        {
            return _scoreWindow.IsVisible == false;
        }

        private void showScoreWindowCommand()
        {
            _scoreWindow.Show();
        }
    }
}
