using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizSpel.Model
{
    [Table("ScoreBoard")]
    public class Scoreboard
    {
        public Scoreboard()
        { }
        [Key]
        public int Id { get; set; }
        public String Name { get; set; }
        public int Score { get; set; }
        public String Quiz { get; set; }
    }
}
