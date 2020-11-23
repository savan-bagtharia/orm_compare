using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ORMCompare.EFModel
{
    [Table("TeamMember")]
    public partial class TeamMemberEF
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }

        [Required]
        [StringLength(200)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(200)]
        public string LastName { get; set; }

        public DateTime DateOfBirth { get; set; }

        public int TeamID { get; set; }

        public virtual TeamEF Team { get; set; }
    }
}
