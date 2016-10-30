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
    [Table("QuizList")]
    public class QuizList
    {
        public QuizList()
        {
            //Questions = new Collection<Question>();
        }
        [Key]
        public int Id { get; set; }
        public int Quiz { get; set; }
        public int Question { get; set; }
    }
}
