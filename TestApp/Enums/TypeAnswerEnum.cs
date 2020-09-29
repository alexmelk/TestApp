using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TestApp.Enums
{
    public enum TypeAnswerEnum
    {
        [Display(Name = "Строка")]
        String = 0,
        [Display(Name = "Целочисленное число")]
        Int = 1,
        [Display(Name = "Флаг да/нет")]
        Bool = 2,
        [Display(Name = "Дата")]
        Date = 3,
        [Display(Name = "Перечисление (пол)")]
        Gender = 4,
        [Display(Name = "Перечисление (семейное положение)")]
        FamilyStatus = 5,
    }
}
