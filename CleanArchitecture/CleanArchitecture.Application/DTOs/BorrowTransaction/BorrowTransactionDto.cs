using CleanArchitecture.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Core.DTOs.BorrowTransaction
{
    public class BorrowTransactionDto
    {
        public Guid Id { get; set; }
        public Guid BookId { get; set; }
        public Guid UserId { get; set; }
        public DateTime BorrowedAt { get; set; }
        public DateTime? ReturnedAt { get; set; }
        public DateTime DueDate { get; set; }
        public decimal LateFee { get; set; }
        public BorrowStatus Status { get; set; }
    }
}
