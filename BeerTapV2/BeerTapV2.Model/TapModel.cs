using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IQ.Platform.Framework.Common;
using IQ.Platform.Framework.WebApi.Model.Hypermedia;

namespace BeerTapV2.Model
{
    public class TapModel : IIdentifiable<int>, IStatelessResource
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int OfficeId { get; set; }
        public int KegId { get; set; }
    }
}
