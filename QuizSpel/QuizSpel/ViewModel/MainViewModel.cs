using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using QuizSpel.Model;
using System.Linq;
using System.Collections.Generic;
using QuizSpel.View;

namespace QuizSpel.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        public static Context context = new Context();

        public MainViewModel()
        {
            ShowQuizSelectionWindowCommand = new RelayCommand(ShowQuizSelectionWindow);
            ShowQuizManagementWindowCommand = new RelayCommand(ShowQuizManagementWindow);
        }

        #region WindowRegion
        private QuizSelectionWindow QuizSelectionWindow;
        private QuizManagementWindow QuizManagementWindow;

        // START Open Window Commands
        public ICommand ShowQuizSelectionWindowCommand { get; set; }
        public ICommand ShowQuizManagementWindowCommand { get; set; }
        // END Open Window Commands

        // START Open Window Methods
        public void ShowQuizSelectionWindow()
        {
            QuizSelectionWindow = new QuizSelectionWindow();
            QuizSelectionWindow.Show();
        }
        public void ShowQuizManagementWindow()
        {
            QuizManagementWindow = new QuizManagementWindow();
            QuizManagementWindow.Show();
        }
        // END Open Window Methods
        #endregion
    }
}