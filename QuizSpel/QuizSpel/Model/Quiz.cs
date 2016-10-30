using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.ObjectModel;

namespace QuizSpel.Model
{
    [Table("Quiz")]
    public class Quiz
    {
        public Quiz()
        { }
        [Key]
        public int Id { get; set; }
        public string Text { get; set; }
    }
}
