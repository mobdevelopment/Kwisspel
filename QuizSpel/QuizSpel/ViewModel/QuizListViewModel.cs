using GalaSoft.MvvmLight;
using QuizSpel.Model;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace QuizSpel.ViewModel
{
    public class QuizListViewModel : ViewModelBase
    {
        private QuizList _quizList;

        public QuizListViewModel()
        {
            this._quizList = new QuizList();
        }
        public QuizListViewModel(QuizList quizList)
        {
            this._quizList = quizList;
        }
        public int Id
        {
            get { return _quizList.Id; }
            set
            {
                _quizList.Id = value;
                //QuizManagementViewModel.Context.SaveChanges();
                RaisePropertyChanged("Id");
            }
        }
        public int Quiz
        {
            get { return _quizList.Quiz; }
            set
            {
                _quizList.Quiz = value;

                RaisePropertyChanged("Quiz");
            }
        }

        public int Question
        {
            get { return _quizList.Question; }
            set
            {
                _quizList.Question = value;
                RaisePropertyChanged("Question");
            }
        }

        //public ICollection<Question> Questions
        //{
        //    get { return _quizList.Questions; }
        //    set
        //    {
        //        _quizList.Questions = value;
        //        QuizManagementViewModel.Context.SaveChanges();
        //        RaisePropertyChanged("Questions");
        //        RaisePropertyChanged("QuestionCount");
        //    }
        //}
        //public int QuestionCount
        //{
        //    get { return Questions.Count; }
        //}
        //public void AddQuestion(Question question)
        //{
        //    _quizList.Question.Add(question);
        //    QuizManagementViewModel.Context.SaveChanges();
        //    RaisePropertyChanged("QuestionCount");
        //    RaisePropertyChanged("Questions");
        //}

        //public void RemoveQuestion(Question question)
        //{
        //    _quizList.Questions.Remove(question);
        //    QuizManagementViewModel.Context.SaveChanges();
        //    RaisePropertyChanged("QuestionCount");
        //    RaisePropertyChanged("Questions");
        //}
        

        public QuizList ToModel()
        {
            return _quizList;
        }
    }
}
