using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestApp.Models.AppDbContextModels;

namespace TestApp.Models
{
    public class ShowAnswersViewModel
    {
        public List<(Answer, Question)> Answers { get; set; } = new List<(Answer, Question)>();

    }
}
