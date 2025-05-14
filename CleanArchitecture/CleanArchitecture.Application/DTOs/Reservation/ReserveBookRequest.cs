using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Core.DTOs.Reservation
{
    public class ReserveBookRequest
    {
        public string UserId { get; set; }
        public Guid BookId { get; set; }
    }
}
