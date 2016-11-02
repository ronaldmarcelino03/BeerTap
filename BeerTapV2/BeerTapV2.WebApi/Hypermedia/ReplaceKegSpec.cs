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
    public class ReplaceKegSpec : SingleStateResourceSpec<ReplaceKegModel, int>
    {
        public static ResourceUriTemplate UriReplaceKeg = ResourceUriTemplate.Create("Offices({officeId})/Taps({tapId})/Kegs({kegId})/ReplaceKeg");
        
        protected override IEnumerable<ResourceLinkTemplate<ReplaceKegModel>> Links()
        {
            yield return CreateLinkTemplate(CommonLinkRelations.Self, UriReplaceKeg, c => c.KegId);
        }
    }
}