using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using QuizSpel.Model;
using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Windows.Input;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Diagnostics;
using QuizSpel.View;

namespace QuizSpel.ViewModel
{
    public class QuizPlayViewModel : ViewModelBase
    {
        public static Context Context = new Context();

        #region WindowRegion
        // Start Open Window
        private QuizScorebordWindow QuizScorebordWindow;
        private QuizDetailWindow QuizDetailWindow;
        private QuizWindow QuizWindow;
        private QuizGameoverWindow QuizGameoverWindow;
        
        public ICommand ShowQuizScorebordWindowCommand { get; set; }
        public ICommand ShowQuizDetailWindowCommand { get; set; }
        public ICommand ShowQuizWindowCommand { get; set; }

        public void ShowQuizScorebordWindow()
        {
            QuizScorebordWindow = new QuizScorebordWindow();
            List<Scoreboard> tempScore = Context.ScoreBoard.ToList();
            ScoreBoard = new ObservableCollection<ScoreboardViewModel>(tempScore.Select(s => new ScoreboardViewModel(s)));

            RaisePropertyChanged("ScoreBoard");
            QuizScorebordWindow.Show();
        }
        public void ShowQuizDetailWindow()
        {
            QuizDetailWindow = new QuizDetailWindow();
            GetRelevantQuestions();
            QuizDetailWindow.Show();
        }
        public void ShowQuizWindow()
        {
            QuestionsCorrect = 0;
            QuestionCounter = 0;

            QuizWindow = new QuizWindow();
            GetRelevantQuestions();
            GetNextQuestion();
            QuizWindow.Show();
        }
        // End Open Window
        #endregion

        #region ObservableCollections
        public ObservableCollection<QuizViewModel> Quizzes { get; set; }
        public ObservableCollection<QuestionViewModel> Questions { get; set; }
        public ObservableCollection<AnswerViewModel> Answers { get; set; }
        public ObservableCollection<QuestionViewModel> RelevantQuestions { get; set; }
        public ObservableCollection<ScoreboardViewModel> ScoreBoard { get; set; }
        #endregion

        public QuizPlayViewModel()
        {
            // Start window RelayCommands
            ShowQuizScorebordWindowCommand = new RelayCommand(ShowQuizScorebordWindow);
            ShowQuizDetailWindowCommand = new RelayCommand(ShowQuizDetailWindow);
            ShowQuizWindowCommand = new RelayCommand(ShowQuizWindow, AbleToPlayQuiz);
            // End window RelayCommands

            // Start RelayCommands
            GiveAnswerTLCommand = new RelayCommand(GiveAnswerTL, AbleToAnswerTL);
            GiveAnswerTRCommand = new RelayCommand(GiveAnswerTR, AbleToAnswerTR);
            GiveAnswerBLCommand = new RelayCommand(GiveAnswerBL, AbleToAnswerBL);
            GiveAnswerBRCommand = new RelayCommand(GiveAnswerBR, AbleToAnswerBR);
            SubmitScoreCommand = new RelayCommand(SubmitScore);
            // End RelayCommands

            List<Quiz> tempQuizzes = Context.Quizzes.ToList();
            Quizzes = new ObservableCollection<QuizViewModel>(tempQuizzes.Select(q => new QuizViewModel(q)));
            SelectedQuiz = Quizzes.FirstOrDefault();

        }

        #region ICommands
        public ICommand GiveAnswerTLCommand { get; set; }
        public ICommand GiveAnswerTRCommand { get; set; }
        public ICommand GiveAnswerBLCommand { get; set; }
        public ICommand GiveAnswerBRCommand { get; set; }

        public ICommand SubmitScoreCommand { get; set; }
        #endregion


        // START Selected Question
        private QuizViewModel _selectedQuiz { get; set; }

        public QuizViewModel SelectedQuiz
        {
            get { return _selectedQuiz; }
            set
            {
                _selectedQuiz = value;
                //ChangeAnswers();
                RaisePropertyChanged("SelectedQuiz");
            }
        }

        private QuestionViewModel _selectedQuestion { get; set; }
        public QuestionViewModel SelectedQuestion
        {
            get { return _selectedQuestion; }
            set
            {
                _selectedQuestion = value;
                RaisePropertyChanged("SelectedQuestion");
            }
        }
        // END Selected Question

        public ScoreboardViewModel Score { get; set; }

        private bool AbleToPlayQuiz()
        {
            if (SelectedQuiz != null)
            {
                return true;
            }
            return false;
        }

        private int _questionCounter;
        public int QuestionCounter
        {
            get { return _questionCounter; }
            set { _questionCounter = value; }
        }
        private int _questionsCorrect;
        public int QuestionsCorrect
        {
            get { return _questionsCorrect; }
            set { _questionsCorrect = value; }
        }
        private int _answerCounter;
        public int AnswerCounter
        {
            get { return _answerCounter; }
            set { _answerCounter = value; }
        }

        #region PlayQuiz Answers
        public AnswerViewModel AnswerTopLeft
        {
            get;
            set;
        }
        public AnswerViewModel AnswerTopRight
        {
            get;
            set;
        }
        public AnswerViewModel AnswerBottomLeft
        {
            get;
            set;
        }
        public AnswerViewModel AnswerBottomRight
        {
            get;
            set;
        }
        
        private void GiveAnswerTL()
        {
            if (AnswerTopLeft.IsCorrect)
            {
                QuestionsCorrect++;
            }
            GetNextQuestion();
        }
        private void GiveAnswerTR()
        {
            if (AnswerTopRight.IsCorrect)
            {
                QuestionsCorrect++;
            }
            GetNextQuestion();
        }
        private void GiveAnswerBL()
        {
            if (AnswerBottomLeft.IsCorrect)
            {
                QuestionsCorrect++;
            }
            GetNextQuestion();
        }
        private void GiveAnswerBR()
        {
            if (AnswerBottomRight.IsCorrect)
            {
                QuestionsCorrect++;
            }
            GetNextQuestion();
        }

        private bool AbleToAnswerTL()
        {
            //if (AnswerTopLeft != null)
            //{
            //    return true;
            //}
            //return false;
            return true;
        }
        private bool AbleToAnswerTR()
        {
            //if (AnswerTopRight != null)
            //{
            //    return true;
            //}
            //return false;
            return true;
        }
        private bool AbleToAnswerBL()
        {
            //if (AnswerBottomLeft != null)
            //{
            //    return true;
            //}
            //return false;
            return true;
        }
        private bool AbleToAnswerBR()
        {
            //if (AnswerBottomRight != null)
            //{
            //    return true;
            //}
            //return false;
            return true;
        }
        #endregion

        private void GetRelevantQuestions()
        {
            if (SelectedQuiz != null)
            {
                // id van quizz ophalen
                // haal alle question die quiz id bevatten
                List<int> tempQuizlist = Context.QuizLists.Where(q => q.Quiz == SelectedQuiz.Id).Select(q => q.Question).ToList();

                RelevantQuestions = new ObservableCollection<QuestionViewModel>();
                foreach (int question in tempQuizlist)
                {
                    QuestionViewModel temp = new QuestionViewModel(Context.Questions.First(q => q.Id == question));
                    RelevantQuestions.Add(temp);
                }
                RaisePropertyChanged("RelevantQuestions");
            }
        }

        private void GetNextQuestion()
        {
            if (QuestionCounter < RelevantQuestions.Count())
            {
                SelectedQuestion = RelevantQuestions[QuestionCounter];
                GetRelevantAnswers();

                if (Answers.Count > 0) AnswerTopLeft = Answers[0];
                if (Answers.Count > 1) AnswerTopRight = Answers[1];
                if (Answers.Count > 2) AnswerBottomLeft = Answers[2];
                if (Answers.Count > 3) AnswerBottomRight = Answers[3];

                RaisePropertyChanged("SelectedQuestion");
                RaisePropertyChanged("AnswerTopLeft");
                RaisePropertyChanged("AnswerTopRight");
                RaisePropertyChanged("AnswerBottomLeft");
                RaisePropertyChanged("AnswerBottomRight");
                QuestionCounter++;
            }
            else
            {
                Score = new ScoreboardViewModel();
                QuizGameoverWindow = new QuizGameoverWindow();
                QuizGameoverWindow.Show();
            }
        }
        private void GetRelevantAnswers()
        {
            if (SelectedQuestion != null)
            {
                List<Answer> tempListAnswers = Context.Questions.Where(q => q.Id == _selectedQuestion.Id).SelectMany(a => a.Answers).ToList();
                Answers = new ObservableCollection<AnswerViewModel>(tempListAnswers.Select(a => new AnswerViewModel(a)));
                AnswerCounter = Answers.Count();
                RaisePropertyChanged("Amount");
                RaisePropertyChanged("Answers");
            }
        }

        private void SubmitScore()
        {
            Score.Quiz = SelectedQuiz.Text;
            Score.Score = QuestionsCorrect;
            Context.ScoreBoard.Add(Score.ToModel());
            Context.SaveChanges();
            QuizGameoverWindow.Hide();
            QuizWindow.Hide();
        }
    }
}
