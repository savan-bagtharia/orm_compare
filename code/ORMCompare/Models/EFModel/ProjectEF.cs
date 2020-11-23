using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ORMCompare.EFModel
{
    [Table("Project")]
    public partial class ProjectEF
    {
        public ProjectEF()
        {
            Teams = new HashSet<TeamEF>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }

        [StringLength(100)]
        public string Name { get; set; }

        public virtual ICollection<TeamEF> Teams { get; set; }
    }
}
