using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KwisspelRenewed.Model
{
    interface IAnswerRepository
    {
        List<Answer> ToList();
    }
}
