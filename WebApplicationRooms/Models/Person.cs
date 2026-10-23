using System;

namespace WebApplicationRooms.Models
{
    [Serializable]
    public abstract class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }

        public Person(string name, DateTime dateOfBirth, string phoneNumber)
        {
            Name = name;
            DateOfBirth = dateOfBirth;
            Phone = phoneNumber;
        }

        public Person() { }

     
        // Calculate age from date of birth
        public int GetAge()
        {
            DateTime today = DateTime.Today;
            int age = today.Year - DateOfBirth.Year;
            if (DateOfBirth.Date > today.AddYears(-age)) age--;
            return age;
        }
    }
}

