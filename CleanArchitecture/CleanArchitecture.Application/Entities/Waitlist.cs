using CleanArchitecture.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Core.Entities
{
    public class Waitlist : BaseEntity
    {
        public Guid BookId { get; set; }
        public string UserId { get; set; }
        public int Position { get; set; }
        public WaitStatus Status { get; set; }
    }
}
