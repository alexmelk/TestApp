using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using TestApp.Enums;

namespace TestApp.Models.AppDbContextModels
{
    [Table("Questions")]
    public class Question
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public TypeAnswerEnum TypeAnswer { get; set; }       
    }
}
