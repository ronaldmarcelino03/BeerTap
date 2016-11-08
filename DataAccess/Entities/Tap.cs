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
		public int OfficeId { get; set; }
		public string BeerName { get; set; }
		public int Content { get; set; }
		public int MaxContent { get; set; }
		public string UnitOfMeasurement { get; set; }
		public string KegState { get; set; }
	}
}
