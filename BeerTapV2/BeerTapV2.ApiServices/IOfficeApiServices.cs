using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeerTapV2.Model;
using IQ.Platform.Framework.WebApi;

namespace BeerTapV2.ApiServices
{
    public interface IOfficeApiService :
        IGetAResourceAsync<OfficeModel, int>,
        IGetManyOfAResourceAsync<OfficeModel, int>
    {
    }
}
