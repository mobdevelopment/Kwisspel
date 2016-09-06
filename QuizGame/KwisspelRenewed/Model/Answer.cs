using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KwisspelRenewed.Model
{
    [Table("Answer")]
    public class Answer
    {
        public Answer()
        {
            IsCorrect = false;
        }

        [Key]
        public int Id { get; set; }

        public string Text { get; set; }

        public bool IsCorrect { get; set; }
    }
}
