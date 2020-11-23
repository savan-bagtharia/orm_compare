using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ORMCompare.EFModel
{
    [Table("Team")]
    public partial class TeamEF
    {
        public TeamEF()
        {
            TeamMembers = new HashSet<TeamMemberEF>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }

        [Required]
        [StringLength(200)]
        public string Name { get; set; }

        public DateTime CreatedDate { get; set; }

        public int ProjectID { get; set; }

        public virtual ICollection<TeamMemberEF> TeamMembers { get; set; }

        public virtual ProjectEF Project { get; set; }
    }
}
