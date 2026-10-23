using System;

namespace WebApplicationRooms.Models
{
    [Serializable]
    public class Booking
    {
        public int Id { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public string Status { get; set; } // Pending, Confirmed, Checked-In, Checked-Out, Cancelled
        public string PaymentStatus { get; set; } = "Unpaid"; // Paid, Unpaid
        public decimal TotalPrice { get; set; }
        public string AdditionalServices { get; set; }
        public string SpecialRequests { get; set; }
        public string AdminComments { get; set; }
        public DateTime BookingDate { get; set; }
        public int UserId { get; set; }
        public Room Room { get; set; }
        public Customer Customer { get; set; }

        public int GuestCount { get; set; } = 1;

        public decimal CancelPenalty { get; set; }
        public DateTime CreatedAt { get; set; }

        

    }
}
