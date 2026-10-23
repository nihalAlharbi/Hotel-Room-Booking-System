using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace WebApplicationRooms.Models
{
    [Serializable]
    public class Notification
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public DateTime CreatedAt { get; set; }
        public int UserId { get; set; }

    }
}
