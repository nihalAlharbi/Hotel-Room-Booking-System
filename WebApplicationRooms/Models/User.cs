using System;

namespace WebApplicationRooms.Models
{
    [Serializable]
    public class User:Person
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Role { get; set; } // Admin, Guest
       


    }
}
