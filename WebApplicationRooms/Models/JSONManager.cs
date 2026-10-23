using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Xml;
using Formatting = Newtonsoft.Json.Formatting;

namespace WebApplicationRooms.Models
{
    [Serializable]
    public class JSONManager
    {
        private static readonly string DataFolderPath = HostingEnvironment.MapPath("~/App_Data/");
        private static readonly string UsersFile = Path.Combine(DataFolderPath, "users.json");
        private static readonly string RoomsFile = Path.Combine(DataFolderPath, "rooms.json");
        private static readonly string BookingsFile = Path.Combine(DataFolderPath, "bookings.json");
        private static readonly string NotificationFile = Path.Combine(DataFolderPath, "notifications.json");

        static JSONManager()
        {
            // Ensure data directory exists
            if (!Directory.Exists(DataFolderPath))
            {
                Directory.CreateDirectory(DataFolderPath);
            }
        }

        // Users management
        public static List<User> LoadUsers()
        {
            try
            {
                if (File.Exists(UsersFile))
                {
                    string json = File.ReadAllText(UsersFile);
                    return JsonConvert.DeserializeObject<List<User>>(json) ?? new List<User>();
                }
            }
            catch (Exception ex)
            {
                // Log error and return default users
                System.Diagnostics.Debug.WriteLine($"Error loading users: {ex.Message}");
            }
            return new List<User>();
        }

        public static void SaveUsers(List<User> users)
        {
            try
            {
                string json = JsonConvert.SerializeObject(users, Formatting.Indented);
                File.WriteAllText(UsersFile, json);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error saving users: {ex.Message}");
                throw;
            }
        }

        // Rooms management
        public static List<Room> LoadRooms()
        {
            try
            {
                if (File.Exists(RoomsFile))
                {
                    string json = File.ReadAllText(RoomsFile);
                    return JsonConvert.DeserializeObject<List<Room>>(json) ?? InitializeDefaultRooms();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error loading rooms: {ex.Message}");
            }
            return InitializeDefaultRooms();
        }

        public static void SaveRooms(List<Room> rooms)
        {
            try
            {
                string json = JsonConvert.SerializeObject(rooms, Formatting.Indented);
                File.WriteAllText(RoomsFile, json);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error saving rooms: {ex.Message}");
                throw;
            }
        }

        // Bookings management
        public static List<Booking> LoadBookings()
        {
            try
            {
                if (File.Exists(BookingsFile))
                {
                    string json = File.ReadAllText(BookingsFile);
                    return JsonConvert.DeserializeObject<List<Booking>>(json) ?? new List<Booking>();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error loading bookings: {ex.Message}");
            }
            return new List<Booking>();
        }

        public static void SaveBookings(List<Booking> bookings)
        {
            try
            {
                string json = JsonConvert.SerializeObject(bookings, Formatting.Indented);
                File.WriteAllText(BookingsFile, json);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error saving bookings: {ex.Message}");
                throw;
            }
        }

        // load Notifications
        public static List<Notification> LoadNotifications()
        {
            try
            {
                if (File.Exists(NotificationFile))
                {
                    string json = File.ReadAllText(NotificationFile);
                    return JsonConvert.DeserializeObject<List<Notification>>(json) ?? new List<Notification>();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error loading bookings: {ex.Message}");
            }
            return new List<Notification>();
        }
        public static void SaveNotifications(List<Notification> notifications)
        {
            try {                
                string json = JsonConvert.SerializeObject(notifications, Formatting.Indented);
                File.WriteAllText(NotificationFile, json);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error saving notificaton: {ex.Message}");
                throw;
            }
        }
        // Save all data
        public static void SaveAllData(List<User> users, List<Room> rooms, List<Booking> bookings, List<Notification> notifications)
        {
            SaveUsers(users);
            SaveRooms(rooms);
            SaveBookings(bookings);
            SaveNotifications(notifications);
        }

        // Load all data
        public static (List<User> users, List<Room> rooms, List<Booking> bookings, List<Notification> notifications) LoadAllData()
        {
            return (LoadUsers(), LoadRooms(), LoadBookings(), LoadNotifications());
        }

       
        private static List<Room> InitializeDefaultRooms()
        {
            return new List<Room>
            {
                // Standard Rooms
                new Room { Id = 1, Name = "Al Khaleej Standard", Type = "Standard", BasePrice = 350, Capacity = 2, Amenities = "WiFi, Smart TV, Air Conditioning, Arabic Coffee Set", IsAvailable = true },
                new Room { Id = 2, Name = "Al Yamama Standard", Type = "Standard", BasePrice = 350, Capacity = 2, Amenities = "WiFi, Smart TV, Air Conditioning, Prayer Mat", IsAvailable = true },
                
                // Deluxe Rooms
                new Room { Id = 3, Name = "Al Najm Deluxe", Type = "Deluxe", BasePrice = 550, Capacity = 3, Amenities = "WiFi, Smart TV, Air Conditioning, Mini Bar, City View", IsAvailable = true },
                new Room { Id = 4, Name = "Al Qamar Deluxe", Type = "Deluxe", BasePrice = 550, Capacity = 3, Amenities = "WiFi, Smart TV, Air Conditioning, Mini Bar, Balcony", IsAvailable = true },
                new Room { Id = 5, Name = "Al Shams Deluxe", Type = "Deluxe", BasePrice = 600, Capacity = 3, Amenities = "WiFi, Smart TV, Air Conditioning, Mini Bar, Mosque View", IsAvailable = true },
                
                // Suite Rooms
                new Room { Id = 6, Name = "Al Malik Suite", Type = "Suite", BasePrice = 850, Capacity = 4, Amenities = "WiFi, Smart TV, Air Conditioning, Living Area, Dining Table", IsAvailable = true },
                new Room { Id = 7, Name = "Al Amir Suite", Type = "Suite", BasePrice = 950, Capacity = 4, Amenities = "WiFi, Smart TV, Air Conditioning, Separate Lounge, Kitchenette", IsAvailable = true },
                
                // Family Rooms
                new Room { Id = 8, Name = "Al Aila Family Suite", Type = "Family", BasePrice = 1200, Capacity = 6, Amenities = "WiFi, Smart TV, Air Conditioning, Two Bedrooms, Kitchenette", IsAvailable = true },
                
                // Royal Suite
                new Room { Id = 9, Name = "Al Mamlaka Royal Suite", Type = "Royal", BasePrice = 2000, Capacity = 4, Amenities = "WiFi, Smart TV, Air Conditioning, Private Lounge, Dining Room, Jacuzzi", IsAvailable = true },
                
                // Presidential Suite
                new Room { Id = 10, Name = "Al Riyadh Presidential", Type = "Presidential", BasePrice = 3500, Capacity = 6, Amenities = "WiFi, Smart TV, Air Conditioning, Private Elevator, Meeting Room, Private Chef Service", IsAvailable = true }
            };
        }

    }
}