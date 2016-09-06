using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using QuizGamePack.Model;

namespace QuizGamePack
{
    public class Database : DbContext
    {
        public DbSet<Question> question { get; set; }
    }
}
