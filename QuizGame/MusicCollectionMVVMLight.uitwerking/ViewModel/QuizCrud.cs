using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using QuizGamePack.Model;
using MusicCollectionMVVMMVVMLight.Model;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Linq;
using System.Collections.Generic;
using System.Windows.Controls;

namespace QuizGamePack.ViewModel
{
    public class QuizCrud : ViewModelBase
    {
        public ObservableCollection<QuestionViewModel> Questions { get; set; }

        public ObservableCollection<AnswerViewModel> Answers { get; set; }

        private QuestionViewModel _selectedQuestion;
        public QuestionViewModel SelectedQuestion { 
            get { return _selectedQuestion; }
            set
            {
                _selectedQuestion = value;
                ChangeAnswers();
                RaisePropertyChanged();
            }
        }

        private AnswerViewModel _selectedAnswer;
        public AnswerViewModel SelectedAnswer
        {
            get { return _selectedAnswer; }
            set { 
                _selectedAnswer = value;
                RaisePropertyChanged("SelectedAnswer");
            }
        }

        private AnswerViewModel _answer1;
        private AnswerViewModel _answer2;
        private AnswerViewModel _answer3;
        private AnswerViewModel _answer4;

        public AnswerViewModel Answer1
        {
            get { return _answer1; }
            set { _answer1 = value; RaisePropertyChanged(); }
        }

        public AnswerViewModel Answer2
        {
            get { return _answer2; }
            set { _answer2 = value; RaisePropertyChanged(); }
        }

        public AnswerViewModel Answer3
        {
            get { return _answer3; }
            set { _answer3 = value; RaisePropertyChanged(); }
        }

        public AnswerViewModel Answer4
        {
            get { return _answer4; }
            set { _answer4 = value; RaisePropertyChanged(); }
        }

        public ICommand AddQuestion { get; set; }
        public ICommand AddAnswer { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand DeleteAnswerCommand { get; set; }

        public static Context context = new Context();

        public QuizCrud()
        {

            IEnumerable<Question> tempQuestions = context.Questions.ToList();
            IEnumerable<QuestionViewModel> questionCollection = tempQuestions.Select(q => new QuestionViewModel(q));

            Questions = new ObservableCollection<QuestionViewModel>(questionCollection);
            SelectedQuestion = Questions.First();

            // Commands for CRUD
            AddQuestion             = new RelayCommand<TextBox>(AddNewQuestion, CanAddNewQuestion);
            AddAnswer               = new RelayCommand<TextBox>(AddNewAnswer);
            DeleteCommand           = new RelayCommand(removeQuestion);
            DeleteAnswerCommand     = new RelayCommand(removeAnswer);

            ChangeAnswers();
        }

        private void removeQuestion()
        {
            QuestionViewModel qvm = SelectedQuestion;
            Questions.Remove(SelectedQuestion);
            context.Entry(qvm.ToModel()).State = System.Data.Entity.EntityState.Deleted;
            context.SaveChanges();
        }

        public void ChangeAnswers()
        {
            if (SelectedQuestion != null && SelectedQuestion.Answers != null)
            {
                IEnumerable<Answer> tempAnswer = SelectedQuestion.Answers.ToList();
                IEnumerable<AnswerViewModel> answerCollection = tempAnswer.Select(a => new AnswerViewModel(a));

                Answers = new ObservableCollection<AnswerViewModel>(answerCollection);

                RaisePropertyChanged("Answers");

                var answerList = SelectedQuestion.Answers.Select(a => new AnswerViewModel(a));
                Answers = new ObservableCollection<AnswerViewModel>(answerList);

                Answer1 = Answers.Count() >= 1 ? Answers.First() : new AnswerViewModel();
                Answer2 = Answers.Count() >= 2 ? Answers.ElementAt(1) : new AnswerViewModel();
                Answer3 = Answers.Count() >= 3 ? Answers.ElementAt(2) : new AnswerViewModel();
                Answer4 = Answers.Count() >= 4 ? Answers.ElementAt(3) : new AnswerViewModel();
            }
        }

        public void removeAnswer()
        {
            Answer answer = SelectedAnswer.ToModel();
            SelectedQuestion.removeAnswer(answer);
            Answer deleteAbleAnswer = context.Answers.Where(at => at.Id == answer.Id).First<Answer>();
            context.Entry(deleteAbleAnswer).State = System.Data.Entity.EntityState.Deleted;
            context.SaveChanges();
            RaisePropertyChanged("Answers");
            ChangeAnswers();
        }

        private void AddNewQuestion(TextBox questionTB)
        {
            var qvm = new QuestionViewModel();

            qvm.Category = SelectedQuestion.Category;
            qvm.Text = questionTB.Text;
            qvm.Id = SelectedQuestion.Id;

            Questions.Add(qvm);
            context.Questions.Add(qvm.ToModel());
            context.SaveChanges();
        }

        private bool CanAddNewQuestion(TextBox irrelevant)
        {
            if (SelectedQuestion == null)
                return false;
            
            if (String.IsNullOrEmpty(SelectedQuestion.Text) || String.IsNullOrEmpty(SelectedQuestion.Category))
                return false;

            return true;
        }

        private void AddNewAnswer(TextBox answerTB)
        {
            if (SelectedQuestion.AnswerCount < 4)
            {
                var avm = new Answer();

                avm.IsCorrect = false;
                avm.Text = answerTB.Text;

                _selectedQuestion.addAnswer(avm);
                SelectedAnswer = new AnswerViewModel(_selectedQuestion.Answers.First());

                RaisePropertyChanged("SelectedQuestion");
                RaisePropertyChanged("SelectedAnswer");
                RaisePropertyChanged("SelectedQuestion.Count");
                RaisePropertyChanged("AnswerCount");

                ChangeAnswers();
            }
        }
    }
}