using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservasCanchas.BusinessLogic.Dtos.Dashboard
{
    public class SumaryDashboardDTO
    {
        public int PendingNotifications { get; set; }
        public int TotalUsers { get; set; }
        public int EnabledComplexes { get; set; }
        public int TotalReviews { get; set; }
    }
}
