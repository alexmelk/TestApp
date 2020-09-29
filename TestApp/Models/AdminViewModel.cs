using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestApp.Models
{
    public class AdminViewModel
    {
        public List<User> Users { get; set; }
        public List<AppDbContextModels.Question> Questions { get; set; }
    }
}
