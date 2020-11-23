using System;

namespace ORMCompare.NHibernateModel
{
    public class TeamMember {
        public virtual int Id { get; set; }
        public virtual Team Team { get; set; }
        public virtual int TeamID { get; set; }
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
        public virtual DateTime DateOfBirth { get; set; }
    }
}
