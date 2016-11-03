using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IQ.Platform.Framework.WebApi;

namespace BeerTapV2.ApiServices
{
    public interface IExtractDataFromARequestContext
    {
        int ExtractOfficeId<TResource>(IRequestContext context) where TResource : class;
        int ExtractTapId<TResource>(IRequestContext context) where TResource : class;
    }
}
