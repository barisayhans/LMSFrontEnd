using CleanArchitecture.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Core.Entities
{
    public class Borrow : BaseEntity
    {
        public Guid BookId { get; set; }
        public string UserId { get; set; }
        public DateTime BorrowedAt { get; set; }
        public DateTime? ReturnedAt { get; set; }
        public DateTime DueDate { get; set; }
        public decimal LateFee { get; set; }
        public BorrowStatus Status { get; set; }
    }
}
