using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IQ.Platform.Framework.Common;
using IQ.Platform.Framework.WebApi.Model.Hypermedia;

namespace BeerTapV2.Model
{
    public class ReplaceKegModel : IIdentifiable<int>, IStatelessResource
    {
        public int Id { get; }
        public string Name { get; set; }
        public int KegId { get; set; }
        public int TapId { get; set; }
        public int OfficeId { get; set; }
    }
}
