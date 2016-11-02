using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeerTapV2.ApiServices
{
    public class LinkParameters
    {
        public LinkParameters(int officeId, int tapId)
        {
            OfficeId = officeId;
            TapId = tapId;
        }

        public int OfficeId { get; private set; }
        public int TapId { get; private set; }
    }
}
