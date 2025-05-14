using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Core.DTOs.User
{
    public class UserStatisticsDto
    {
        public int TotalBorrowed { get; set; }
        public int NotReturned { get; set; }
        public int TotalVisitors { get; set; }
        public int BorrowedBooks { get; set; }
        public int OverdueBooks { get; set; }
        public int NewMembers { get; set; }
        public int AvailableBooks { get; set; }
        public int ReservedBooks { get; set; }

    }
}
