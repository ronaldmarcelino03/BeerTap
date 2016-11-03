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
        public static ResourceUriTemplate Uri = ResourceUriTemplate.Create("Offices({officeId})/Taps({tapId})/PullBeer");
        protected override IEnumerable<ResourceLinkTemplate<PullBeerModel>> Links()
        {
            yield return CreateLinkTemplate(CommonLinkRelations.Self, Uri, c => c.OfficeId, c => c.TapId);
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
                            CreateLinkTemplate(LinkRelations.Tap, TapSpec.Uri.Many, c => c.OfficeId)
                        },
                        Operations = new StateSpecOperationsSource<PullBeerModel, int>
                        {
                            //Get = ServiceOperations.Get,
                            InitialPost = ServiceOperations.Create,
                            Post = ServiceOperations.Update,
                            //Delete = ServiceOperations.Delete
                        }
                    };
            }
        }
    }
}