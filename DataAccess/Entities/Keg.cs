using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entities
{
    public class Keg
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int Content { get; set; }
        public int MaxContent { get; set; }
        public string UnitOfMeasurement { get; set; }
        //public string KegState { get; set; }
        public int TapId { get; set; }
        public int OfficeId { get; set; }
    }
}
