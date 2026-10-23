using System;

namespace WebApplicationRooms.Models
{
    [Serializable]
    public class Customer : Person
    {
        
        public Customer(string name, string email, string phone)
        {
            Id ++;
            Name = name;
            Email = email;
            Phone = phone;
        }
    }
}