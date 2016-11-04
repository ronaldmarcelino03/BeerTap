using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BeerTapV2.ApiServices;
using BeerTapV2.Model;
using IQ.Platform.Framework.WebApi.Hypermedia;
using IQ.Platform.Framework.WebApi.Hypermedia.Specs;
using IQ.Platform.Framework.WebApi.Model.Hypermedia;

namespace BeerTapV2.WebApi.Hypermedia
{
    public class KegSpec : SingleStateResourceSpec<KegModel, int>
    {
        public static ResourceUriTemplate Uri = ResourceUriTemplate.Create("Offices({officeId})/Taps({tapId})/Kegs({id})");

        public override string EntrypointRelation
        {
            get { return LinkRelations.Keg; }
        }

        protected override IEnumerable<ResourceLinkTemplate<KegModel>> Links()
        {
            yield return CreateLinkTemplate<LinkParameters>(CommonLinkRelations.Self, Uri, c => c.Parameters.OfficeId, c => c.Parameters.TapId, c => c.Resource.Id);
        }

        public override IResourceStateSpec<KegModel, NullState, int> StateSpec
        {
            get
            {
                return
                    new SingleStateSpec<KegModel, int>
                    {
                        Links =
                        {
                            CreateLinkTemplate(LinkRelations.Tap, TapSpec.Uri.Many, c => c.Id)
                        },
                        Operations = new StateSpecOperationsSource<KegModel, int>
                        {
                            Get = ServiceOperations.Get
                        }
                    };
            }
        }
    }
}