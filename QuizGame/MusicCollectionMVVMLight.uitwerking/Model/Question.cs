using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.ObjectModel;

namespace QuizGamePack.Model
{
    [Table("Question")]
    public partial class Question
    {
        public Question()
        {
            Answers = new Collection<Answer>();
        }

        [Key]
        public int Id { get; set; }

        public string Text { get; set; }

        public string Category { get; set; }

        public virtual ICollection<Answer> Answers { get; set; }
    }
}
