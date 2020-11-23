using System;

namespace ORMCompare.Models
{
    public class TeamMember
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int TeamID { get; set; }
    }
}