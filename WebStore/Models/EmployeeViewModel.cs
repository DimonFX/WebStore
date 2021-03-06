﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebStore.Models
{
    public class EmployeeViewModel
    {

        public int Id { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Имя является обязательным")]
        [Display(Name = "Имя")]
        public string FirstName { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Фамилия является обязательным")]
        [StringLength(100,MinimumLength = 2,ErrorMessage = "В имени дложно быть не менее 2х не более 100")]
        [Display(Name = "Фамилия")]
        public string SurName { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Отчество является обязательным")]
        [Display(Name = "Отчество")]
        public string Patronymic { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Возраст является обязательным")]
        [Display(Name = "Возраст")]
        public int Age { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Должность является обязательным")]
        [Display(Name = "Должность")]
        public string Position { get; set; }
    }
}
