using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using BeerTapV2.ApiServices.Security;
using IQ.Platform.Framework.Common;
using IQ.Platform.Framework.WebApi;
using IQ.Platform.Framework.WebApi.AspNet;

namespace BeerTapV2.ApiServices.RequestContext
{
    public class RequestContextExtractor : IExtractDataFromARequestContext
    {
        public int ExtractOfficeId<TResource>(IRequestContext context) where TResource : class
        {
            if (context == null) throw new ArgumentNullException(nameof(context));

            var option = context.UriParameters.GetByName<int>("officeId");
            var officeId = option.EnsureValue(() => context.CreateHttpResponseException<TResource>("Cannot find office identifier in the uri", HttpStatusCode.BadRequest));

            context.LinkParameters.Set(new LinkParameters(officeId, 0));

            return officeId;
        }

        public int ExtractTapId<TResource>(IRequestContext context) where TResource : class
        {
            if (context == null) throw new ArgumentNullException(nameof(context));
          
            var officeOption = context.UriParameters.GetByName<int>("officeId");
            var officeId = officeOption.EnsureValue(() => context.CreateHttpResponseException<TResource>("Cannot find office identifier in the uri", HttpStatusCode.BadRequest));
            var tapOption = context.UriParameters.GetByName<int>("tapId");
            var tapId = tapOption.EnsureValue(() => context.CreateHttpResponseException<TResource>("Cannot find tap identifier in the uri", HttpStatusCode.BadRequest));

            context.LinkParameters.Set(new LinkParameters(officeId, tapId));

            return tapId;
        }
    }
}
