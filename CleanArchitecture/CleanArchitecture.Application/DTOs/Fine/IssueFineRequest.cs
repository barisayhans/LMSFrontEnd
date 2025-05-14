using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Core.DTOs.Fine
{
    public class IssueFineRequest
    {
        public string UserId { get; set; }
        public decimal Amount { get; set; }
    }
}
