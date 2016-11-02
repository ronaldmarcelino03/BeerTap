using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entities
{
    public class Office
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Location { get; set; }
        //public virtual ICollection<Tap> Collection { get; set; }
    }
}
