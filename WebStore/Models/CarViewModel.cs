using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebStore.Models
{
    public class CarViewModel
    {
        [ScaffoldColumn(false)]
        public int Id { get; set; }
        [Display(Name ="Марка")]
        public string Mark { get; set; }
        [Display(Name = "Модель")]
        public string CarModel { get; set; }
        [Display(Name = "Тип кузова")]
        public string BodyType { get; set; }
        [Display(Name = "Год выпуска")]
        public int YearRelease { get; set; }
        [Display(Name = "Трансмиссия")]
        public string Transmission { get; set; }
        [Display(Name = "Цвет")]
        public string Color { get; set; }
        [Display(Name = "Привод")]
        public string Drive { get; set; }
        [Display(Name = "Объем двигателя")]
        public double EngineVolume { get; set; }
    }
}
