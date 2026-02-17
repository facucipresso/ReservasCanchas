using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinFormsApp1.Enum;

namespace WinFormsApp1.Models.Complex
{
    public class UpdateComplexStateDTO
    {
        public ComplexState State { get; set; }
        public string? CancelationReason { get; set; }
    }
}
