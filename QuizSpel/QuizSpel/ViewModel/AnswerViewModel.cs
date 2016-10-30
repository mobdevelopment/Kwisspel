using GalaSoft.MvvmLight;
using QuizSpel.Model;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace QuizSpel.ViewModel
{
    public class AnswerViewModel : ViewModelBase
    {
        private Answer _answer;

        public AnswerViewModel()
        {
            this._answer = new Answer();
        }
        public AnswerViewModel(Answer answer)
        {
            this._answer = answer;
        }

        public int Id
        {
            get { return _answer.Id; }
            set
            {
                _answer.Id = value;
                QuizManagementViewModel.Context.SaveChanges();
                RaisePropertyChanged("Id");
            }
        }
        public string Text
        {
            get { return _answer.Text; }
            set
            {
                _answer.Text = value;
                QuizManagementViewModel.Context.SaveChanges();
                RaisePropertyChanged("Text");
            }
        }
        public bool IsCorrect
        {
            get { return _answer.IsCorrect; }
            set
            {
                _answer.IsCorrect = value;
                QuizManagementViewModel.Context.SaveChanges();
                RaisePropertyChanged("IsCorrect");
            }
        }

        private bool _isAnswered = false;
        public bool IsAnswered()
        {
            return _isAnswered;
        }
        public void SetAnswered()
        {
            _isAnswered = true;
        }

        public Answer ToModel()
        {
            return _answer;
        }
    }
}
