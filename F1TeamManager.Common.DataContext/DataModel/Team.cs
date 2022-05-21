using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F1.Common.DataContext.DataModel
{
    [Table("Team")]
    public class Team
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string? Name { get; set; }
        public DateTime EstabilishmentDate { get; set; }


        public virtual ICollection<Champion> Champions { get; set; }
        public virtual ICollection<LicenseFee> LicenseFees { get; set; }

        public Team()
        {
            Champions = new HashSet<Champion>();
            LicenseFees = new HashSet<LicenseFee>();
        }
    }
}
