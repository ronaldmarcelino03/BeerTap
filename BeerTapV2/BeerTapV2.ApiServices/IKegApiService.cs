using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeerTapV2.Model;
using IQ.Platform.Framework.WebApi;
using IQ.Platform.Framework.WebApi.Model.Hypermedia;

namespace BeerTapV2.ApiServices
{
    public interface IKegApiService :
        IGetAResourceAsync<KegModel, int>,
        IGetManyOfAResourceAsync<KegModel, int>
    {
    }
}
