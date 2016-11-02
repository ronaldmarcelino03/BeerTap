using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IQ.Platform.Framework.Common;
using IQ.Platform.Framework.WebApi.Model.Hypermedia;

namespace BeerTapV2.Model
{
    public class OfficeModel : IStatelessResource, IIdentifiable<int>
    {
        public int Id { get; set; }
        public string Location { get; set; }

        //public List<TapModel> Taps { get; set; }
    }
}
