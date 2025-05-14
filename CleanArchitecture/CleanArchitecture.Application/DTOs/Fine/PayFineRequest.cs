using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Core.DTOs.Fine
{
    public class PayFineRequest
    {
        public string UserId { get; set; }
        public Guid FineId { get; set; }
    }
}
