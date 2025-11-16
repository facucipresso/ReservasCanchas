using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservasCanchas.BusinessLogic.Dtos.Complex
{
    public class UpdateComplexServiceRequestDTO
    {
        public List<int> ServicesIds { get; set; } = new List<int>();
    }
}
