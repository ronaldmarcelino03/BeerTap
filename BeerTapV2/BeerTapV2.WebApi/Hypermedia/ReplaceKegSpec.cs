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
    public class ReplaceKegSpec : SingleStateResourceSpec<ReplaceKegModel, int>
    {
        public static ResourceUriTemplate Uri = ResourceUriTemplate.Create("Offices({officeId})/Taps({tapId})/ReplaceKeg");

        protected override IEnumerable<ResourceLinkTemplate<ReplaceKegModel>> Links()
        {
            yield return CreateLinkTemplate(CommonLinkRelations.Self, Uri, c => c.OfficeId, c => c.TapId);
        }

        public override IResourceStateSpec<ReplaceKegModel, NullState, int> StateSpec
        {
            get
            {
                return
                    new SingleStateSpec<ReplaceKegModel, int>
                    {
                        Links =
                        {
                            CreateLinkTemplate(LinkRelations.Tap, TapSpec.Uri.Many, c => c.OfficeId)
                        },
                        Operations = new StateSpecOperationsSource<ReplaceKegModel, int>
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