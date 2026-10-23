using System;

namespace WebApplicationRooms.Models
{
    [Serializable]
    public class Room
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; } // Standard, Deluxe, Suite
        public decimal BasePrice { get; set; }
        public int Capacity { get; set; }
        public string Amenities { get; set; }
        public bool IsAvailable { get; set; }
    }
}
