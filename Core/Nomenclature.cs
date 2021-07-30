using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class Nomenclature
    {
        [Required]
        [Key]
        [Display(Name = "Номенклатурный номер")]
        public int NomenclatureId { get; set; }

        [Required]
        [Display(Name = "Отклонение диаметра")]
        public int DiameterDeviation { get; set; }

        public virtual ICollection<PipeLog> PipeLogs { get; set; }
    }
}
