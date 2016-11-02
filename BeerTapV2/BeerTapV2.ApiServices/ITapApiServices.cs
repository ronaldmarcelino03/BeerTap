using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeerTapV2.Model;
using IQ.Platform.Framework.WebApi;

namespace BeerTapV2.ApiServices
{
    public interface ITapApiService :
        IGetAResourceAsync<TapModel, int>,
        IGetManyOfAResourceAsync<TapModel, int>,
        ICreateAResourceAsync<TapModel, int>,
        IUpdateAResourceAsync<TapModel, int>,
        IDeleteResourceAsync<TapModel, int>
    {
    }
}
