﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizSpel.Model
{
    interface IScoreboardRepository
    {
        List<Scoreboard> ToList();
    }
}
