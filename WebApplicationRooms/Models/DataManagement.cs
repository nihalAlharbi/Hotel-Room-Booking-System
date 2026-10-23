using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace WebApplicationRooms.Models
{
    [Serializable]
    public class StatisticsResult
    {
        public int TotalBookings { get; set; }
        public decimal TotalRevenue { get; set; }
     
        public int TotalGuests { get; set; }
        public double BookingChange { get; set; }
        public double RevenueChange { get; set; }
      
        public double GuestsChange { get; set; }
       
    }
    [Serializable]
    public class Activity
    {
        public string Type { get; set; }
        public string Description { get; set; }
        public DateTime Timestamp { get; set; }
    }
    [Serializable]
    public static class DataManagement
    {
        private static bool dataLoaded = false;
        private static List<User> users;
        private static List<Room> rooms;
        private static List<Booking> bookings;
        private static List<Notification> notifications;
       

        // Initialize data on first access
        private static void EnsureDataLoaded()
        {
            if (!dataLoaded)
            {
                var allData = JSONManager.LoadAllData();
                users = allData.users;
                rooms = allData.rooms;
                bookings = allData.bookings;
                notifications = allData.notifications;
                dataLoaded = true;
            }
        }

        // Save all data to JSON files
        private static void SaveAllData()
        {
            JSONManager.SaveAllData(users, rooms, bookings,notifications);
        }
      
        public static List<User> Users
        {
            get
            {
                EnsureDataLoaded();
                return users; 
            }
        }

        public static List<Room> Rooms
        {
            get
            {
                EnsureDataLoaded();
                return rooms;
            }
        }

        public static List<Booking> Bookings
        {
            get
            {
                EnsureDataLoaded();
                return bookings;
            }
        }

        public static List<Notification> Notifications
        {
            get
            {
                EnsureDataLoaded();
                return notifications;
            }
        }

        // Methods to modify data that require saving
        public static void AddUser(User user)
        {
            EnsureDataLoaded();
            users.Add(user);
            SaveAllData();
        }


        public static void AddRoom(Room room)
        {
           
            EnsureDataLoaded();
            room.Id = rooms.Count > 0 ? rooms.Max(r => r.Id) + 1 : 1;
            rooms.Add(room);
            SaveAllData();
        }

        public static void AddNotification(Notification notification)
        {
            Notification lastNotification = notifications.LastOrDefault();
            if (lastNotification != null)
            {

                notification.Id = lastNotification.Id + 1;
            }
            else
            {
                notification.Id = 1;
            }
            EnsureDataLoaded();
            notifications.Add(notification);
            SaveAllData();
        }

        public static void UpdateRoom(Room updatedRoom)
        {
            EnsureDataLoaded();
            var existingRoom = rooms.FirstOrDefault(r => r.Id == updatedRoom.Id);
            if (existingRoom != null)
            {
                rooms.Remove(existingRoom);
                rooms.Add(updatedRoom);
                SaveAllData();
            }
        }

        public static void AddBooking(Booking booking)
        {
            EnsureDataLoaded();

            // Auto-generate ID if not set
            if (booking.Id == 0)
            {
                booking.Id = bookings.Count > 0 ? bookings.Max(b => b.Id) + 1 : 1;
            }

            bookings.Add(booking);
            SaveAllData();
        }

        public static void UpdateBooking(Booking updatedBooking)
        {
            EnsureDataLoaded();
            var existingBooking = bookings.FirstOrDefault(b => b.Id == updatedBooking.Id);
            if (existingBooking != null)
            {
                bookings.Remove(existingBooking);
                bookings.Add(updatedBooking);
                SaveAllData();
            }
        }

        public static bool DeleteNotification(int notificationId)
        {
            Notification notification = notifications.FirstOrDefault(n => n.Id == notificationId);
            if (notification != null)
            {
                notifications.Remove(notification);
                SaveAllData();
                return true;
            }
            return false;
        }
        public static User AuthenticateUser(string username, string password)
        {
            foreach (User user in Users)
            {
                if (user.Username == username && user.Password == DataManagement.HashPassword(password))
                {
                    return user;
                }
            }
            return null;
        }
        public static decimal CalculatePrice(Room room, DateTime checkIn, DateTime checkOut)
        {
           
            if (checkOut < checkIn)
            {
                throw new ArgumentException("Check-out date must be after check-in date");
            }

           
            decimal basePrice = 0;
            bool roomTypeFound = false;

            foreach (Room rm in Rooms)
            {
                if (room == rm)
                {
                    basePrice = room.BasePrice;
                    roomTypeFound = true;
                    break;
                }
            }

            if (!roomTypeFound)
            {
                throw new ArgumentException($"Room type '{room.Name}' not found");
            }

            
            int nights = (checkOut - checkIn).Days;

            
            if (nights < 1)
            {
                nights = 1;
            }

            decimal totalPrice = basePrice * nights;

            bool hasPeakSeason = false;
            bool hasOffSeason = false;

            for (int i = 0; i < nights; i++)
            {
                DateTime currentDay = checkIn.AddDays(i);
                if (IsPeakSeason(currentDay))
                {
                    hasPeakSeason = true;
                }
                else if (IsOffSeason(currentDay))
                {
                    hasOffSeason = true;
                }
            }

            if (hasPeakSeason)
            {
                totalPrice *= 1.2m;
            }
            else if (hasOffSeason)
            {
                 totalPrice *= 0.8m;
            }

             if (nights >= 7 && !hasPeakSeason)
            {
                totalPrice *= 0.9m; 
            }

            if (totalPrice < 0)
            {
                totalPrice = basePrice * nights; 
            }

            return Math.Round(totalPrice, 2); 
        }

        private static bool IsPeakSeason(DateTime date)
        {
            return date.Month >= 6 && date.Month <= 8;
        }

        private static bool IsOffSeason(DateTime date)
        {
            return date.Month == 1 || date.Month == 2 || date.Month == 12;
        }

        public static bool IsRoomAvailable(int roomId, DateTime checkIn, DateTime checkOut)
        {
            foreach (Booking booking in Bookings)
            {
                if (booking.Room.Id == roomId && (booking.Status != "Cancelled" && booking.Status != "Checked-Out"))
                {
                    return !IsBusyDate(booking, checkIn, checkOut);
                }
            }
            return true;
        }
        public static bool IsRoomAvailable(Booking currentBooking, int roomId, DateTime checkIn, DateTime checkOut)
        {
            foreach (Booking booking in Bookings)
            {
                if (booking.Id != currentBooking.Id && booking.Room.Id == roomId && (booking.Status != "Cancelled" && booking.Status != "Checked-Out"))
                {
                    return !IsBusyDate(booking, checkIn, checkOut);
                }
            }
            return true;
        }
        private static bool IsBusyDate(Booking booking, DateTime checkIn, DateTime checkOut)
        {
            return ((checkIn >= booking.CheckInDate && checkIn < booking.CheckOutDate) ||
                       (checkOut > booking.CheckInDate && checkOut <= booking.CheckOutDate) ||
                       (checkIn <= booking.CheckInDate && checkOut >= booking.CheckOutDate));
        }

        public static decimal CalculateCancellationPenalty(Booking booking, DateTime cancelTime)
        {
            var hoursPassed = (cancelTime - booking.CreatedAt).TotalHours;

            if (hoursPassed <= 24)
                return 0m; // free
            if (hoursPassed <= 48)
                return booking.TotalPrice * 0.5m; // 50%

            return booking.TotalPrice; // 100%
        }
        public static StatisticsResult GetStatistics(DateTime startDate, DateTime endDate)
        {
            var result = new StatisticsResult();

            // Get previous period for comparison
            DateTime previousStartDate = startDate.AddDays(-(endDate - startDate).Days);
            DateTime previousEndDate = startDate.AddDays(-1);

            // Current period statistics
            var currentBookings = GetBookingsInPeriod(startDate, endDate);
            var previousBookings = GetBookingsInPeriod(previousStartDate, previousEndDate);

            // Total Bookings
            result.TotalBookings = currentBookings.Count;
            int previousTotalBookings = previousBookings.Count;
            result.BookingChange = CalculatePercentageChange(previousTotalBookings, result.TotalBookings);

            // Total Revenue
            result.TotalRevenue = currentBookings.Sum(b => b.TotalPrice);
            decimal previousRevenue = previousBookings.Sum(b => b.TotalPrice);
            result.RevenueChange = CalculatePercentageChange((double)previousRevenue, (double)result.TotalRevenue);

            // Total Guests (assuming each booking has at least 1 guest)
            result.TotalGuests = currentBookings.Sum(b => b.GuestCount);
            int previousGuests = previousBookings.Count;
            result.GuestsChange = CalculatePercentageChange(previousGuests, result.TotalGuests);

            return result;
        }

        private static List<Booking> GetBookingsInPeriod(DateTime startDate, DateTime endDate)
        {
            // Filter bookings that overlap with the period
            return Bookings.Where(b =>
                ((b.CheckInDate <= endDate && b.CheckInDate>=startDate)&& (b.Status == "Checked-In" || b.Status == "Checked-Out"))
            ).ToList();
        }

     
        private static double CalculatePercentageChange(double previousValue, double currentValue)
        {
            if (previousValue == 0) return currentValue > 0 ? 100 : 0;
            return ((currentValue - previousValue) / previousValue) * 100;
        }

        public static List<Activity> GetRecentActivities(int count)
        {
            var activities = new List<Activity>();

            // Get recent bookings as activities
            var recentBookings = Bookings
                .OrderByDescending(b => b.CheckInDate)
                .Take(count / 2)
                .ToList();

            foreach (var booking in recentBookings)
            {
                activities.Add(new Activity
                {
                    Type = "Booking",
                    Description = $"New booking from {booking.Customer.Name} for {GetRoomName(booking.Room.Id.ToString())}",
                    Timestamp = booking.CheckInDate
                });
            }

            // Add check-in/check-out activities
            var statusChanges = Bookings
                .Where(b => b.Status == "Checked-In" || b.Status == "Checked-Out")
                .OrderByDescending(b => b.CheckInDate)
                .Take(count / 2)
                .ToList();

            foreach (var booking in statusChanges)
            {
                activities.Add(new Activity
                {
                    Type = booking.Status == "Checked-In" ? "CheckIn" : "CheckOut",
                    Description = $"{booking.Customer.Name} {booking.Status} for {GetRoomName(booking.Room.Id.ToString())}",
                    Timestamp = booking.CheckInDate.AddDays(1) // Simulate recent activity
                });
            }

            // Add some cancellation activities
            var cancellations = Bookings
                .Where(b => b.Status == "Cancelled")
                .OrderByDescending(b => b.CheckInDate)
                .Take(2)
                .ToList();

            foreach (var booking in cancellations)
            {
                activities.Add(new Activity
                {
                    Type = "Cancellation",
                    Description = $"Booking cancelled by {booking.Customer.Name}",
                    Timestamp = booking.CheckInDate.AddDays(-1) // Simulate recent cancellation
                });
            }

            return activities
                .OrderByDescending(a => a.Timestamp)
                .Take(count)
                .ToList();
        }

        // Helper method to get room by ID
        public static Room GetRoomById(int roomId)
        {
            return Rooms.FirstOrDefault(r => r.Id == roomId);
        }

        
        public static string GetRoomName(string roomId)
        {
            if (int.TryParse(roomId, out int id))
            {
                var room = Rooms.FirstOrDefault(r => r.Id == id);
                return room?.Name ?? "Unknown Room";
            }
            return "Unknown Room";
        }

        public static void MakeNotificationToUser(int userId, string title, string message)
        {
            Notification notification = new Notification();
            notification.UserId = userId;
            notification.Title = title;
            notification.Message = message;
            notification.CreatedAt = DateTime.Now;
            DataManagement.AddNotification(notification);
        }
        public static string HashPassword(string rawData)
        {
            // Create a SHA256 hash algorithm object
            using (SHA256 sha256 = SHA256.Create())
            {
                // Convert the string to bytes and compute the hash
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(rawData));

                // Convert the byte array to a readable hexadecimal string
                StringBuilder builder = new StringBuilder();
                foreach (byte b in bytes)
                    builder.Append(b.ToString("x2")); // "x2" means two-digit lowercase hex

                return builder.ToString();
            }
        }

    }
}
