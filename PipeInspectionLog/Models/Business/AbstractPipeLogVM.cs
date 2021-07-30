using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PipeInspectionLog.Models.Business
{
    public abstract class AbstractPipeLogVM
    {
        //номер трубы
        [Required(ErrorMessage = "Пожалуйста введите номер трубы")]
        [Display(Name = "Номер трубы")]
        [MinLength(1), MaxLength(100)]
        [DataType(DataType.Text)]
        public string PipeNumber { get; set; }

        //Целевой внешний диаметр
        [Required(ErrorMessage = "Пожалуйста введите целевой диаметр")]
        [Display(Name = "Целевой внешний диаметр, мм")]
        [Range(1, int.MaxValue, ErrorMessage = "Пожалуйста введите целевой диаметр")]
        public int DiameterId { get; set; }
        public SelectList Diameters { get; set; }
        
        //Номенклатуры трубы
        [Required(ErrorMessage = "Пожалуйста введите номенклатуру")]
        [Display(Name = "Номенклатура трубы")]
        [Range(1, int.MaxValue, ErrorMessage = "Пожалуйста введите номенклатуру")]
        public int NomenclatureId { get; set; }
        public SelectList Nomenclatures { get; set; }

        //Внешний диаметр на конце 1
        [Required(ErrorMessage = "Пожалуйста введите внешний диаметр на конце 1")]
        [Display(Name = "Внешний диаметр на конце 1, мм")]
        [Range(1, 4000, ErrorMessage = "Пожалуйста введите внешний диаметр на конце 1")]
        public double EndExternalDiameter1 { get; set; }

        //Внешний диаметр на конце 2
        [Required(ErrorMessage = "Пожалуйста введите внешний диаметр на конце 2")]
        [Display(Name = "Внешний диаметр на конце 2, мм")]
        [Range(1, 4000, ErrorMessage = "Пожалуйста введите внешний диаметр на конце 2")]
        public double EndExternalDiameter2 { get; set; }

        //Внешний диаметр в центре
        [Display(Name = "Внешний диаметр в центре")]
        [Range(1, 4000, ErrorMessage = "Пожалуйста введите корректный внешний диаметр в центре")]
        public double? CenterExternalDiameter { get; set; }

        //Примечание
        [Display(Name = "Примечание")]
        [MaxLength(300)]
        [DataType(DataType.MultilineText)]
        public string Note { get; set; }
    }
}