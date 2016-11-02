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
        public string Location { get; set; }
        public int OfficeId { get; set; }
    }
}
