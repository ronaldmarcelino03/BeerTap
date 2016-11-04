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
    public class TapSpec : ResourceSpec<TapModel, KegState, int>
    {
        public static ResourceUriTemplate Uri = ResourceUriTemplate.Create("Offices({officeId})/Taps({id})");

        public override string EntrypointRelation
        {
            get { return LinkRelations.Tap; }
        }

        protected override IEnumerable<ResourceLinkTemplate<TapModel>> Links()
        {
            yield return CreateLinkTemplate(CommonLinkRelations.Self, Uri, c => c.OfficeId, c => c.Id);
            yield return CreateLinkTemplate(LinkRelations.Keg, KegSpec.Uri.Many, c => c.OfficeId, c => c.Id);
        }

        protected override IEnumerable<IResourceStateSpec<TapModel, KegState, int>> GetStateSpecs()
       {
            // New Keg
            yield return new ResourceStateSpec<TapModel, KegState, int>(KegState.New)
            {
                Links =
                 {
                     CreateLinkTemplate(LinkRelations.Kegs.GetAllYouWant, PullBeerSpec.Uri.Many, c => c.OfficeId, c => c.Id)
                 },
                Operations = new StateSpecOperationsSource<TapModel, int>()
                {
                    InitialPost = ServiceOperations.Create
                }
            };

            // Going down
            yield return new ResourceStateSpec<TapModel, KegState, int>(KegState.GoingDown)
            {
                Links =
                    {
                        CreateLinkTemplate(LinkRelations.Kegs.GetSomeMore, PullBeerSpec.Uri.Many, c => c.OfficeId, c => c.Id)
                    },
                Operations = new StateSpecOperationsSource<TapModel, int>()
                {
                    InitialPost = ServiceOperations.Create
                }
            };

            // Almost empty
            yield return new ResourceStateSpec<TapModel, KegState, int>(KegState.AlmostEmpty)
            {
                Links =
                    {
                        CreateLinkTemplate(LinkRelations.Kegs.GetWhatIsLeft, PullBeerSpec.Uri.Many, c => c.OfficeId, c => c.Id)
                    },
                Operations = new StateSpecOperationsSource<TapModel, int>()
                {
                    InitialPost = ServiceOperations.Create
                }
            };

            // Empty
            yield return new ResourceStateSpec<TapModel, KegState, int>(KegState.Empty)
            {
                Links =
                    {
                        CreateLinkTemplate(LinkRelations.Kegs.ReplaceKeg, ReplaceKegSpec.Uri.Many, c => c.OfficeId, c => c.Id)
                    },
                Operations = new StateSpecOperationsSource<TapModel, int>()
                {
                    InitialPost = ServiceOperations.Create
                }
            };
        }
    }
}