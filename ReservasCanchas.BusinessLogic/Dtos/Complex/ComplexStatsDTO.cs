using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservasCanchas.BusinessLogic.Dtos.Complex
{
    public class ComplexStatsDTO
    {
        public int totalPossibleSlots { get; set; }
        public int occupiedSlots { get; set; }
        public decimal totalRevenue { get; set; }
    }
}
