using GalaSoft.MvvmLight;
using QuizSpel.Model;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace QuizSpel.ViewModel
{
    public class ScoreboardViewModel : ViewModelBase
    {
        private Scoreboard _scoreboard;
        public ScoreboardViewModel()
        {
            this._scoreboard = new Scoreboard();
        }
        public ScoreboardViewModel(Scoreboard scoreboard)
        {
            this._scoreboard = scoreboard;
        }

        public int Id
        {
            get { return _scoreboard.Id; }
            set
            {
                _scoreboard.Id = value;
                QuizPlayViewModel.Context.SaveChanges();
                RaisePropertyChanged("Id");
            }
        }
        public string Name
        {
            get { return _scoreboard.Name; }
            set
            {
                _scoreboard.Name = value;
                QuizPlayViewModel.Context.SaveChanges();
                RaisePropertyChanged("Name");
            }
        }
        public int Score
        {
            get { return _scoreboard.Score; }
            set
            {
                _scoreboard.Score = value;
                QuizPlayViewModel.Context.SaveChanges();
                RaisePropertyChanged("Score");
            }
        }
        public string Quiz
        {
            get { return _scoreboard.Quiz; }
            set
            {
                _scoreboard.Quiz = value;
                QuizPlayViewModel.Context.SaveChanges();
                RaisePropertyChanged("Quiz");
            }
        }
        public Scoreboard ToModel()
        {
            return _scoreboard;
        }
    }
}
