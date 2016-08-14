using GalaSoft.MvvmLight;
using QuizGamePack.Model;
using QuizGamePack.ViewModel;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MusicCollectionMVVMMVVMLight.Model
{
    public class AnswerViewModel : ViewModelBase
    {
        private bool _isAnswered = false;

        public int Id
        {
            get { return _answer.Id; }
            set { _answer.Id = value; QuizCrud.context.SaveChanges(); RaisePropertyChanged("Id"); }
        }


        public string Text
        {
            get { return _answer.Text; }
            set { _answer.Text = value; QuizCrud.context.SaveChanges(); RaisePropertyChanged("Text"); }
        }

        public bool IsCorrect
        {
            get { return _answer.IsCorrect; }
            set { _answer.IsCorrect = value; QuizCrud.context.SaveChanges(); RaisePropertyChanged("IsCorrect"); }
        }

        private Answer _answer;

        public AnswerViewModel()
        {
            this._answer = new Answer();
        }

        public AnswerViewModel(Answer answer)
        {
            this._answer = answer;
        }

        public bool isAnswered()
        {
            return _isAnswered;
        }

        public void setAnswered()
        {
            _isAnswered = true;
        }

        public Answer ToModel()
        {
            return _answer;
        }
    }
}
