using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class Diameter
    {
        [Required]
        [Key]
        public int DiameterId { get; set; }

        [Required]
        [Display(Name = "Целевой диаметр, мм")]
        public double Value { get; set; }

        public virtual ICollection<PipeLog> PipeLogs { get; set; }
    }
}
