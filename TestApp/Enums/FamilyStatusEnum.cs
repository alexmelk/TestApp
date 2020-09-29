using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TestApp.Enums
{
    public enum FamilyStatusEnum
    {
        [Display(Name = "В браке")]
        Married = 0,
        [Display(Name = "Не в браке")]
        NotMarried = 1,

    }
}
