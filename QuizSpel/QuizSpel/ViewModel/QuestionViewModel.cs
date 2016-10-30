using GalaSoft.MvvmLight;
using QuizSpel.Model;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace QuizSpel.ViewModel
{
    public class QuestionViewModel : ViewModelBase
    {
        private Question _question;
        public QuestionViewModel()
        {
            this._question = new Question();
        }
        public QuestionViewModel(Question question)
        {
            this._question = question;
        }

        public int Id
        {
            get { return _question.Id; }
            set
            {
                _question.Id = value;
                QuizManagementViewModel.Context.SaveChanges();
                RaisePropertyChanged("Id");
            }
        }
        public string Text
        {
            get { return _question.Text; }
            set
            {
                _question.Text = value;
                QuizManagementViewModel.Context.SaveChanges();
                RaisePropertyChanged("Text");
            }
        }
        public string Category
        {
            get { return _question.Category; }
            set
            {
                _question.Category = value;
                QuizManagementViewModel.Context.SaveChanges();
                RaisePropertyChanged("Category");
            }
        }
        public ICollection<Answer> Answers
        {
            get { return _question.Answers; }
            set
            {
                _question.Answers = value;
                QuizManagementViewModel.Context.SaveChanges();
                RaisePropertyChanged("Answers");
                RaisePropertyChanged("AnswerCount");
            }
        }
        public int AnswerCount
        {
            get { return Answers.Count; }
        }
        
        public void RemoveQuestion(Question question)
        {
            QuizManagementViewModel.Context.Questions.Remove(question);
            QuizManagementViewModel.Context.SaveChanges();
            RaisePropertyChanged("Questions");
        }

        public void RemoveAnswer(Answer answer)
        {
            _question.Answers.Remove(answer);
            QuizManagementViewModel.Context.SaveChanges();
            RaisePropertyChanged("AnswerCount");
            RaisePropertyChanged("Answers");
        }
        public void AddAnswer(Answer answer)
        {
            _question.Answers.Add(answer);
            QuizManagementViewModel.Context.SaveChanges();
            RaisePropertyChanged("AnswerCount");
            RaisePropertyChanged("Answers");
        }

        public int CountCorrectAnswers()
        {
            int count = 0;

            foreach (Answer answer in _question.Answers)
            {
                if (answer.IsCorrect)
                {
                    count++;
                }
            }
            return count;
        }

        public Question ToModel()
        {
            return _question;
        }
    }
}
