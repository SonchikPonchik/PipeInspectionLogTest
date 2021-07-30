using PipeInspectionLog.Models.Business;
using System.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace PipeInspectionLog.Models.VM
{
    public class UpdateViewModel : AbstractPipeLogVM
    {
        [Required]
        public int PipeLogId { get; set; }

    }
}