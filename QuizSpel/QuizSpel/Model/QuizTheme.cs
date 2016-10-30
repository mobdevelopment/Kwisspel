using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizSpel.Model
{
    public class QuizTheme
    {
        public const int GENERAL = 1;
        public const int SCHOOL = 2;
        public const int MEDIA = 3;
        public const int POLITICS = 4;
        public const int NATURE = 5;

        public int ID { get; set; }

        public string Name { get; set; }
    }
}
