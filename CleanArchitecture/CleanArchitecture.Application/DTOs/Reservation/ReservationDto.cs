using CleanArchitecture.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Core.DTOs.Reservation
{
    public class ReservationDto
    {
        public Guid Id { get; set; }
        public Guid BookId { get; set; }
        public Guid UserId { get; set; }
        public DateTime ReservedAt { get; set; }
        public ReservationStatus Status { get; set; }
        public DateTime? ExpiryDate { get; set; }
    }
}
