using CleanArchitecture.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Core.Entities
{
    public class Reservation : BaseEntity
    {
        public Guid BookId { get; set; }
        public string UserId { get; set; }
        public DateTime ReservedAt { get; set; }
        public ReservationStatus Status { get; set; }
        public DateTime? ExpiryDate { get; set; }
    }
}
