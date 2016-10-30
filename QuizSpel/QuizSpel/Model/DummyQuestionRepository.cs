using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizSpel.Model
{
    class DummyQuestionRepository : IThemeRepository
    {
        public List<QuizTheme> ToList()
        {
            var themes = new List<QuizTheme>();

            themes.Add(new QuizTheme
            {
                ID = QuizTheme.GENERAL,
                Name = "Algemeen",
            });

            themes.Add(new QuizTheme
            {
                ID = QuizTheme.SCHOOL,
                Name = "Educatie",
            });

            themes.Add(new QuizTheme
            {
                ID = QuizTheme.MEDIA,
                Name = "Media",
            });

            themes.Add(new QuizTheme
            {
                ID = QuizTheme.NATURE,
                Name = "Natuur",
            });

            themes.Add(new QuizTheme
            {
                ID = QuizTheme.POLITICS,
                Name = "Politiek",
            });

            return themes;
        }
    }
}
