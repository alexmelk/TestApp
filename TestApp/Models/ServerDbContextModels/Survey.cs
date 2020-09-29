using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TestApp.Models.ServerDbContextModels
{
    [Table("Survey")]
    public class Survey
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
    }
}
