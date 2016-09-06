using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KwisspelRenewed.Model
{
    class DummyQuestionRepository : IQuestionRepository
    {
        public List<Question> ToList()
        {
            var questions = new List<Question>();

            questions.Add(new Question
            {
                Id = 1,
                Text = "Wat is 1 + 1",
                Category = "Rekenen",
                Answers = new List<Answer> {
                new Answer { Id = 1, Text = "2", IsCorrect = true},
                new Answer { Id = 2, Text = "11", IsCorrect = false}
            }
            });

            questions.Add(new Question
            {
                Id = 2,
                Text = "Wat is 2 + 2",
                Category = "Rekenen",
                Answers = new List<Answer> {
                new Answer { Id = 1, Text = "6", IsCorrect = false},
                new Answer { Id = 2, Text = "8", IsCorrect = false},
                new Answer { Id = 3, Text = "4", IsCorrect = true},
                new Answer { Id = 4, Text = "Een rekensom", IsCorrect = true},
            }
            });

            questions.Add(new Question
            {
                Id = 3,
                Text = "Wat is liefde",
                Category = "Rekenen",
                Answers = new List<Answer> {
                new Answer { Id = 1, Text = "Baby don't hurt me", IsCorrect = false },
                new Answer { Id = 2, Text = "An emotion", IsCorrect = true },
                new Answer { Id = 3, Text = "An hormone", IsCorrect = false }

            }
            });

            questions.Add(new Question
            {
                Id = 4,
                Text = "Welke programmeertaal is cewl?",
                Category = "Rekenen",
                Answers = new List<Answer> {
                new Answer { Id = 1, Text = "Java", IsCorrect = false },
                new Answer { Id = 2, Text = "C#", IsCorrect = true },
                new Answer { Id = 3, Text = "Python", IsCorrect = false},
                new Answer { Id = 4, Text = "Javascript", IsCorrect = false},
            }
            });

            return questions;
        }
    }
}
