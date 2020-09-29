using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestApp.Enums;
using TestApp.Models.AppDbContextModels;

namespace TestApp.Models
{
    public class SurveyViewModel
    {
        public int Position { get; set; } 
        public List<Question> Questions { get; set; }
        public Answer Answer { get; set; }

    }
}
