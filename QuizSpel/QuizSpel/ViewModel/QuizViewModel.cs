using GalaSoft.MvvmLight;
using QuizSpel.Model;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace QuizSpel.ViewModel
{
    public class QuizViewModel : ViewModelBase
    {
        private Quiz _quiz;
        public QuizViewModel()
        {
            this._quiz = new Quiz();
        }
        public QuizViewModel(Quiz quiz)
        {
            this._quiz = quiz;
        }

        public int Id
        {
            get { return _quiz.Id; }
            set
            {
                _quiz.Id = value;
                //QuizManagementViewModel.Context.SaveChanges();
                RaisePropertyChanged("Id");
            }
        }
        public string Text
        {
            get { return _quiz.Text; }
            set
            {
                _quiz.Text = value;
                //QuizManagementViewModel.Context.SaveChanges();
                RaisePropertyChanged("Text");
            }
        }

        public Quiz ToModel()
        {
            return _quiz;
        }
    }
}