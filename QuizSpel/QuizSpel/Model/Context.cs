using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace QuizSpel.Model
{
    public class Context : DbContext
    {
        public Context() : base("Prog5DB") { }

        public DbSet<Quiz> Quizzes { get; set; }
        public DbSet<QuizList> QuizLists { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<Scoreboard> ScoreBoard { get; set; }
    }
}
