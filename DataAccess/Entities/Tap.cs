using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Entities;

namespace DataAccess
{
    public class Tap
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        //[StringLength(50)]
        //public string Description { get; set; }
        public int Content { get; set; }
        public int MaxContent { get; set; }
        //public string UnitOfMeasurement { get; set; }
        public int OfficeId { get; set; }
        public string TapState { get; set; }
    }
}
