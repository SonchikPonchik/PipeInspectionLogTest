using System;
using System.ComponentModel.DataAnnotations;

namespace Core
{
    public class PipeLog
    {
        [Required]
        [Key]
        public int PipeLogId { get; set; }

        [Required]
        [Display(Name = "Дата осмотра")]
        public DateTime InspectionDate { get; set; }

        //номер трубы
        [Required(ErrorMessage = "Пожалуйста введите номер трубы")]
        [Display(Name = "Номер трубы")]
        [MinLength(1), MaxLength(100)]
        [DataType(DataType.Text)]
        public string PipeNumber { get; set; }

        //Целевой внешний диаметр
        [Required]
        [Display(Name = "Целевой внешний диаметр")]
        public int DiameterId { get; set; }

        //Номенклатуры трубы
        [Required]
        [Display(Name = "Номенклатурный номер")]
        public int NomenclatureId { get; set; }

        //Внешний диаметр на конце 1
        [Required(ErrorMessage = "Пожалуйста введите внешний диаметр на конце 1")]
        [Display(Name = "Внешний диаметр на конце 1, мм")]
        public double EndExternalDiameter1 { get; set; }

        //Внешний диаметр на конце 2
        [Required(ErrorMessage = "Пожалуйста введите внешний диаметр на конце 2")]
        [Display(Name = "Внешний диаметр на конце 2, мм")]
        public double EndExternalDiameter2 { get; set; }

        //Внешний диаметр в центре
        [Display(Name = "Внешний диаметр в центре, мм")]
        public double? CenterExternalDiameter { get; set; }

        //Максимальное отклонение
        [Required]
        [Display(Name = "Максимальное отклонение диаметров, мм")]
        public double MaxDiameterDeviation { get; set; }

        //Примечание
        [Display(Name = "Примечание")]
        [MaxLength(300)]
        [DataType(DataType.MultilineText)]
        public string Note { get; set; }

        public virtual Nomenclature Nomenclature { get; set; }
        public virtual Diameter Diameter { get; set; }
    }
}