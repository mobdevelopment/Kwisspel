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
    public class QuizPlay : ViewModelBase
    {
        private int _index = 0;
        private int _score;
        private int _currentAnswered = 0;

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

        public int Score
        {
            get { return _score; }
            set { _score = value; RaisePropertyChanged("Score");}
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

        public ICommand AnswerQuestion { get; set; }

        public static Context context = new Context();

        public QuizPlay()
        {

            IEnumerable<Question> tempQuestions = context.Questions.OrderBy(r => Guid.NewGuid()).Take(10).ToList();
            IEnumerable<QuestionViewModel> questionCollection = tempQuestions.Select(q => new QuestionViewModel(q));

            Questions = new ObservableCollection<QuestionViewModel>(questionCollection);
            SelectedQuestion = Questions.First();

            // Commands for playing
            AnswerQuestion          = new RelayCommand<AnswerViewModel>(GiveAnswer, CanAnswer);

            ChangeAnswers();
        }

        private void removeQuestion()
        {
            Questions.Remove(SelectedQuestion);
        }

        private void ClearQuestion()
        {
            this.SelectedQuestion = new QuestionViewModel();
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

        private void GiveAnswer(AnswerViewModel answer)
        {
            answer.setAnswered();
            if (answer.IsCorrect) _score++;

            _currentAnswered++;
            if (!answer.IsCorrect || _currentAnswered == SelectedQuestion.CountCorrectAnswers())
            {
                if (CanNextQuestion())
                {
                    NextQuestion();
                }
                else
                {
                    ScoreWindow scoreWindow = new ScoreWindow();
                    scoreWindow.Show();
                }
            }
        }

        public bool CanAnswer(AnswerViewModel answer)
        {
            if (answer != null)
            {
                if (answer.Text != null && !answer.isAnswered())
                {
                    return true;
                }
            }
            return false;
        }

        private void NextQuestion()
        {
            while (CanNextQuestion())
            {
                SelectedQuestion = Questions.ElementAt(_index + 1);
                _currentAnswered = 0;
                _index++;

                if (SelectedQuestion.AnswerCount > 0) return;
            }
        }

        private void PrevQuestion()
        {
            SelectedQuestion = Questions.ElementAt(_index - 1);
            _currentAnswered = 0;
            _index++;
        }

        private bool CanNextQuestion()
        {
            return _index < Questions.Count() - 1;
        }
    }
}