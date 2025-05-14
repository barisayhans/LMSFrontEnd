using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Core.Entities
{
    public class Fine : BaseEntity
    {
        public string UserId { get; set; }        
        public decimal Amount { get; set; }
        public DateTime IssuedAt { get; set; }
        public bool IsPaid { get; set; }
    }
}
