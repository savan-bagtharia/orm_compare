using System;

namespace ORMCompare.NHibernateModel
{
    public class Team {
        public virtual int Id { get; set; }
        public virtual Project Project { get; set; }
        public virtual string Name { get; set; }
        public virtual int ProjectID { get; set; }
        public virtual DateTime CreatedDate { get; set; }
    }
}
