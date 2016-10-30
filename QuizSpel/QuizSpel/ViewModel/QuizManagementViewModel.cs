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
    public class QuizManagementViewModel : ViewModelBase
    {
        public static Context Context = new Context();

        #region WindowRegion
        private QuizBuildingWindow QuizBuildingWindow;
        public ICommand ShowQuizBuildingWindowCommand { get; set; }

        // START Open Window Methods
        public void ShowQuizBuildingWindow()
        {
            QuizBuildingWindow = new QuizBuildingWindow();
            QuizBuildingWindow.Show();
        }
        // END Open Window Methods
        #endregion

        #region ObservableCollections
        public ObservableCollection<QuestionViewModel> Questions { get; set; }
        public ObservableCollection<QuestionViewModel> QuizQuestions { get; set; }
        public ObservableCollection<AnswerViewModel> Answers { get; set; }
        public ObservableCollection<QuestionViewModel> QuizList { get; set; }
        public ObservableCollection<QuizViewModel> Quizzes { get; set; }

        public ObservableCollection<QuestionViewModel> RelevantQuestions { get; set; }
        #endregion

        public QuizManagementViewModel()
        {
            // START RelayCommands
            // QuizManagementWindow
            AddQuestionCommand      = new RelayCommand(AddQuestion, AbleToAddQuestion);
            DeleteQuestionCommand   = new RelayCommand(DeleteQuestion, AbleToDeleteQuestion);
            AddAnswerCommand        = new RelayCommand(AddAnswer, AbleToAddAnswer);
            DeleteAnswerCommand     = new RelayCommand(DeleteAnswer, AbleToDeleteAnswer);
            // Window
            ShowQuizBuildingWindowCommand = new RelayCommand(ShowQuizBuildingWindow);

            //QuizBuildingWindow
            AddQuizCommand          = new RelayCommand(AddQuiz, AbleToAddQuiz);
            AddToQuizCommand        = new RelayCommand(AddToQuiz, AbleToAddDeleteQuestionToQuiz);
            RemoveFromQuizCommand   = new RelayCommand(RemoveFromQuiz, AbleToAddDeleteQuestionToQuiz);
            // END RelayCommands
            
            //QuizManagementWindow
            List<Question> tempQuestions = Context.Questions.ToList();
            Questions   = new ObservableCollection<QuestionViewModel>(tempQuestions.Select(q => new QuestionViewModel(q)));

            SelectedQuestion = Questions.FirstOrDefault();

            //QuizBuildingWindow
            QuizList    = new ObservableCollection<QuestionViewModel>();
            List<Question> tempsQuizQuestions = Context.Questions.Where(q => q.Answers.Count() == 4).ToList();
            QuizQuestions = new ObservableCollection<QuestionViewModel>(tempsQuizQuestions.Select(q => new QuestionViewModel(q)));
        }

        #region ICommands
        // START ICommands
        public ICommand AddQuestionCommand { get; set; }
        public ICommand DeleteQuestionCommand { get; set; }
        public ICommand AddAnswerCommand { get; set; }
        public ICommand DeleteAnswerCommand { get; set; }
        // END ICommands
        #endregion

        #region Question CRD
        // Method for Adding a Question to the database.
        // Input: text from QuizManagementWindow.
        public void AddQuestion()
        {
            if (!String.IsNullOrEmpty(_questionText))
            {
                var qvm = new QuestionViewModel();

                if (SelectedQuestion == null)
                {
                    SelectedQuestion = new QuestionViewModel();
                }

                qvm.Category = SelectedQuestion.Category;
                qvm.Text = _questionText;
                qvm.Id = SelectedQuestion.Id;

                QuestionText = string.Empty;

                Questions.Add(qvm);
                Context.Questions.Add(qvm.ToModel());
                Context.SaveChanges();
            }
        }
        // Method for checking if a Question can be added.
        // Conditions: The Question texbox cannot be empty.
        public bool AbleToAddQuestion()
        {
            //if (SelectedQuestion == null)
            //    return true;
            ////if (String.IsNullOrEmpty(_questionText.Text))
            //    //return false;
            //else
            //    return false;
            return true;
        } 
        // Method for Deleting a Question from the database.
        // Input: delete button from QuizManagementWindow.
        public void DeleteQuestion()
        {
            QuestionViewModel qvm = SelectedQuestion;
            Questions.Remove(SelectedQuestion);
            Context.Entry(qvm.ToModel()).State = System.Data.Entity.EntityState.Deleted;
            Context.SaveChanges();
        }
        // Method for checking if a Question can be deleted.
        // Conditions: A Question must be selected.
        public bool AbleToDeleteQuestion()
        {
            if (SelectedQuestion == null)
                return false;
            else
                return true;
        }
        #endregion

        #region AnswerCRD
        // Method for Adding an Answer to a Question and saving it in the database.
        public void AddAnswer()
        {
            if (!String.IsNullOrEmpty(_answerText))
            {
                var avm = new Answer();

                avm.Text = _answerText;
                avm.IsCorrect = _answerCorrect;

                AnswerText = string.Empty;
                _selectedQuestion.AddAnswer(avm);
                SelectedAnswer = new AnswerViewModel(_selectedQuestion.Answers.First());

                RaisePropertyChanged("SelectedQuestion");
                RaisePropertyChanged("SelectedAnswer");
                RaisePropertyChanged("SelectedQuestion.Count");
                RaisePropertyChanged("AnswerCount");

                ChangeAnswers();
            }
        }

        // Method for checking if a Answer can be added to a question.
        // Conditions: A Question must be selected. A Question cannot already have 4 Answers.
        public bool AbleToAddAnswer()
        {
            //if (SelectedQuestion == null)
            //    return false;
            //else if (SelectedQuestion.AnswerCount < 4)
            //    return true;
            //else
            //    return false;
            return true;
        }
        // Method for Deleting an Answer from a question and the database.
        public void DeleteAnswer()
        {
            Answer answer = SelectedAnswer.ToModel();
            SelectedQuestion.RemoveAnswer(answer);
            Answer deleteAbleAnswer = Context.Answers.Where(at => at.Id == answer.Id).First<Answer>();
            Context.Entry(deleteAbleAnswer).State = System.Data.Entity.EntityState.Deleted;
            Context.SaveChanges();
            RaisePropertyChanged("Answers");
            ChangeAnswers();
        }
        // Method for checking if a Answer can be deleted.
        // Conditions: A Question and an Answer must be selected.
        public bool AbleToDeleteAnswer()
        {
            //if (SelectedQuestion == null)
            //    return false;
            //if (SelectedQuestion != null)
            //    if (SelectedAnswer == null)
            //        return false;
            return true;
        }
        #endregion

        private void ChangeAnswers()
        {
            if (SelectedQuestion != null && SelectedQuestion.Answers != null)
            {
                IEnumerable<Answer> tempAnswer = SelectedQuestion.Answers.ToList();
                IEnumerable<AnswerViewModel> answerCollection = tempAnswer.Select(a => new AnswerViewModel(a));

                Answers = new ObservableCollection<AnswerViewModel>(answerCollection);

                RaisePropertyChanged("SelectedQuestion");

                //RaisePropertyChanged("Questions");
                RaisePropertyChanged("Answers");

                var answerList = SelectedQuestion.Answers.Select(a => new AnswerViewModel(a));
                Answers = new ObservableCollection<AnswerViewModel>(answerList);

                Answer1 = Answers.Count() >= 1 ? Answers.First() : new AnswerViewModel();
                Answer2 = Answers.Count() >= 2 ? Answers.ElementAt(1) : new AnswerViewModel();
                Answer3 = Answers.Count() >= 3 ? Answers.ElementAt(2) : new AnswerViewModel();
                Answer4 = Answers.Count() >= 4 ? Answers.ElementAt(3) : new AnswerViewModel();
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


        // START Selected Question
        private QuestionViewModel _selectedQuestion { get; set; }

        public QuestionViewModel SelectedQuestion
        {
            get { return _selectedQuestion; }
            set
            {
                _selectedQuestion = value;
                ChangeAnswers();
                RaisePropertyChanged("SelectedQuestion");
            }
        }
        // END Selected Question

        // START Selected Answer
        private AnswerViewModel _selectedAnswer { get; set; }

        public AnswerViewModel SelectedAnswer
        {
            get { return _selectedAnswer; }
            set
            {
                _selectedAnswer = value;
                RaisePropertyChanged("SelectedAnswer");
            }
        }
        // END Selected Question

        #region QuizManagement Input fields
        private string _questionText;
        public string QuestionText
        {
            get { return _questionText; }
            set
            {
                _questionText = value;
                RaisePropertyChanged("QuestionText");
            }
        }

        private string _answerText;
        public string AnswerText
        {
            get { return _answerText; }
            set
            {
                _answerText = value;
                RaisePropertyChanged("AnswerText");
            }
        }

        // Method for setting a Answer true or false.
        // Input: checkbox from QuizManagementWindow.
        private bool _answerCorrect;
        public bool AnswerCorrect
        {
            set
            {
                if (value)
                    _answerCorrect = true;
                else
                    _answerCorrect = false;
            }
        }
        #endregion


        public QuizViewModel _selectedQuiz { get; set; }
        public QuizViewModel SelectedQuiz
        {
            get { return _selectedQuiz; }
            set
            {
                _selectedQuiz = value;
                GetRelevantQuestions();
                RaisePropertyChanged("SelectedQuiz");
            }
        }

        private string _quizText;
        public string QuizText
        {
            get { return _quizText; }
            set
            {
                _quizText = value;
                RaisePropertyChanged("QuizText");
            }
        }

        public ICommand AddQuizCommand { get; set; }
        public ICommand AddToQuizCommand { get; set; }
        public ICommand RemoveFromQuizCommand { get; set; }

        public void AddQuiz()
        {
            if (!String.IsNullOrEmpty(_quizText))
            {          
                SelectedQuiz = new QuizViewModel();
                SelectedQuiz.Text = QuizText;
                QuizText = string.Empty;

                Context.Quizzes.Add(SelectedQuiz.ToModel());
                Context.SaveChanges();

                foreach (var item in QuizList)
                {
                    QuizListViewModel newQuizList = new QuizListViewModel();
                    newQuizList.Quiz = SelectedQuiz.Id;
                    newQuizList.Question = item.Id;
                    Context.QuizLists.Add(newQuizList.ToModel());
                    Context.SaveChanges();                
                }
                RaisePropertyChanged("Quizzes");
                // needs to be turned on when working!!
                QuizBuildingWindow.Hide();
            }
        }

        private bool AbleToAddQuiz()
        {
            //if (QuizList.Count() >= 2 && QuizList.Count() < 10)
            //{
                return true;
            //}

                //  Question count check: MIN: 2 or more and MAX: 10 or les
            //return false;
        }

        public void AddToQuiz()
        {
            QuizList.Add(SelectedQuestion);
        }

        public void RemoveFromQuiz()
        {
            QuizList.Remove(SelectedQuestion);
        }

        public bool AbleToAddDeleteQuestionToQuiz()
        {
            if (SelectedQuestion != null)
            {
                return true;
            }
            return false;
        }

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
    }
}
