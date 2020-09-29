using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using TestApp.Models.AppDbContextModels;
using TestApp.Models.ServerDbContextModels;

namespace TestApp.Models
{
    [Table("Answer")]
    public class Answer
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Неверно введено значение")]
        public string Text { get; set; }
        public bool IsCheck { get; set; }
        public Survey Survey { get; set; }
        public Question Question { get; set; }
    }
}
