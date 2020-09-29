using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TestApp.Enums
{
    public enum GenderEnum
    {
        [Display(Name = "Муж.")]
        Male = 0,
        [Display(Name = "Жен.")]
        Female = 1,
    }
}
