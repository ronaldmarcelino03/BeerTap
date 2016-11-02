using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BeerTapV2.Model;
using IQ.Platform.Framework.WebApi.Hypermedia;
using IQ.Platform.Framework.WebApi.Hypermedia.Specs;
using IQ.Platform.Framework.WebApi.Model.Hypermedia;

namespace BeerTapV2.WebApi.Hypermedia
{
    public class PullBeerSpec : SingleStateResourceSpec<PullBeerModel, int>
    {
        public static ResourceUriTemplate UriPullBeer = ResourceUriTemplate.Create("Kegs({kegId})/PullBeer");
        protected override IEnumerable<ResourceLinkTemplate<PullBeerModel>> Links()
        {
            yield return CreateLinkTemplate(CommonLinkRelations.Self, KegSpec.UriTapOfKegAtOffice, c => c.Id);
        }
    }
}