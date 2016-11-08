using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using BeerTapV2.ApiServices;
using BeerTapV2.Model;
using IQ.Platform.Framework.Common;
using IQ.Platform.Framework.WebApi;
using IQ.Platform.Framework.WebApi.Hypermedia;
using IQ.Platform.Framework.WebApi.Hypermedia.Specs;
using IQ.Platform.Framework.WebApi.Model.Hypermedia;

namespace BeerTapV2.WebApi.Hypermedia
{
    public class PullBeerSpec : SingleStateResourceSpec<PullBeerModel, int>
    {
        public static ResourceUriTemplate Uri = ResourceUriTemplate.Create("Offices({officeId})/Taps({tapId})/PullBeer");

        protected override IEnumerable<ResourceLinkTemplate<PullBeerModel>> Links()
        {
            yield return CreateLinkTemplate<LinkParameters>(CommonLinkRelations.Self, Uri, c => c.Parameters.OfficeId, c => c.Parameters.TapId);
			yield return CreateLinkTemplate<LinkParameters>(LinkRelations.Tap, TapSpec.Uri, c => c.Parameters.OfficeId, c => c.Parameters.TapId);
		}
        public override IResourceStateSpec<PullBeerModel, NullState, int> StateSpec
        {
            get
            {
                return
                    new SingleStateSpec<PullBeerModel, int>
                    {
                        Links =
                        {
                            CreateLinkTemplate<LinkParameters>(LinkRelations.Tap, TapSpec.Uri.Many, c => c.Parameters.OfficeId)
                        },
                        Operations = new StateSpecOperationsSource<PullBeerModel, int>
                        {
                            InitialPost = ServiceOperations.Create
                        }
                    };
            }
        }
    }
}