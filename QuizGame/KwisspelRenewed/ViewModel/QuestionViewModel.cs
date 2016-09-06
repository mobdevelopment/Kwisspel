using GalaSoft.MvvmLight;
using KwisspelRenewed.Model;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace KwisspelRenewed.ViewModel
{
    public class QuestionViewModel : ViewModelBase
    {
        public int Id
        {
            get { return _question.Id; }
            set { _question.Id = value; QuizCrud.context.SaveChanges(); RaisePropertyChanged("Id"); }
        }


        public string Text
        {
            get { return _question.Text; }
            set { _question.Text = value; QuizCrud.context.SaveChanges(); RaisePropertyChanged("Text"); }
        }

        public string Category
        {
            get { return _question.Category; }
            set { _question.Category = value; QuizCrud.context.SaveChanges(); RaisePropertyChanged("Category"); }
        }

        public ICollection<Answer> Answers
        {
            get { return _question.Answers; }
            set { _question.Answers = value; QuizCrud.context.SaveChanges(); RaisePropertyChanged("Answers"); RaisePropertyChanged("AnswerCount"); }
        }

        public int AnswerCount
        {
            get { return Answers.Count; }
            private set {; }
        }

        private Question _question;

        public QuestionViewModel()
        {
            this._question = new Question();
        }

        public QuestionViewModel(Question question)
        {
            this._question = question;
        }

        public void removeAnswer(Answer answer)
        {
            _question.Answers.Remove(answer);
            QuizCrud.context.SaveChanges();
            RaisePropertyChanged("AnswerCount");
            RaisePropertyChanged("Answers");
        }

        public void addAnswer(Answer answer)
        {
            _question.Answers.Add(answer);
            QuizCrud.context.SaveChanges();
            RaisePropertyChanged("AnswerCount");
            RaisePropertyChanged("Answers");
        }

        public int CountCorrectAnswers()
        {
            int count = 0;

            foreach (Answer answer in _question.Answers)
            {
                if (answer.IsCorrect) count++;
            }

            return count;
        }

        public Question ToModel()
        {
            return _question;
        }
    }
}
