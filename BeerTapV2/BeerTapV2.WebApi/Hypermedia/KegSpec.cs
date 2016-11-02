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
    public class KegSpec : ResourceSpec<KegModel, KegState, int>
    {
        //public static ResourceUriTemplate UriTapOfKeg = ResourceUriTemplate.Create("Taps({tapId})/Kegs({id})");
        public static ResourceUriTemplate UriTapOfKegAtOffice = ResourceUriTemplate.Create("Offices({officeId})/Taps({tapId})/Kegs");

        public override string EntrypointRelation
        {
            get { return LinkRelations.Keg; }
        }

        protected override IEnumerable<ResourceLinkTemplate<KegModel>> Links()
        {
            //yield return CreateLinkTemplate(CommonLinkRelations.Self, UriTapOfKeg, _ => _.TapId, _ => _.Id);
            yield return
                CreateLinkTemplate<LinkParameters>(CommonLinkRelations.Self, UriTapOfKegAtOffice,
                    c => c.Parameters.OfficeId, c => c.Parameters.TapId);
        }

        protected override IEnumerable<IResourceStateSpec<KegModel, KegState, int>> GetStateSpecs()
        {
            // To be refactored
            //New Keg
            yield return new ResourceStateSpec<KegModel, KegState, int>(KegState.New)
            {
                Links =
                {
                    CreateLinkTemplate(LinkRelations.Kegs.GetAllYouWant, PullBeerSpec.UriPullBeer.Many, c => c.TapId, c => c.Id)
                },
                Operations = new StateSpecOperationsSource<KegModel, int>()
                {
                    Get = ServiceOperations.Get,
                    InitialPost = ServiceOperations.Create,
                    Post = ServiceOperations.Update,
                    Put = ServiceOperations.Update,
                    Delete = ServiceOperations.Delete
                }
            };

            // Going down
            yield return new ResourceStateSpec<KegModel, KegState, int>(KegState.GoingDown)
            {
                Links =
                {
                    CreateLinkTemplate(LinkRelations.Kegs.GetSomeMore, TapSpec.UriTapAtOffice.Many, _ => _.Id)
                },
                Operations = new StateSpecOperationsSource<KegModel, int>()
                {
                    Get = ServiceOperations.Get,
                    InitialPost = ServiceOperations.Create,
                    Post = ServiceOperations.Update,
                    Put = ServiceOperations.Update,
                    Delete = ServiceOperations.Delete
                }
            };

            // Almost empty
            yield return new ResourceStateSpec<KegModel, KegState, int>(KegState.AlmostEmpty)
            {
                Links =
                {
                    CreateLinkTemplate(LinkRelations.Kegs.GetWhatIsLeft, TapSpec.UriTapAtOffice.Many, _ => _.Id)
                },
                Operations = new StateSpecOperationsSource<KegModel, int>()
                {
                    Get = ServiceOperations.Get,
                    InitialPost = ServiceOperations.Create,
                    Post = ServiceOperations.Update,
                    Put = ServiceOperations.Update,
                    Delete = ServiceOperations.Delete
                }
            };

            // Empty
            yield return new ResourceStateSpec<KegModel, KegState, int>(KegState.Empty)
            {
                Links =
                {
                    CreateLinkTemplate(LinkRelations.Kegs.ReplaceKeg, ReplaceKegSpec.UriReplaceKeg.Many, c => c.TapId, c => c.Id)
                },
                Operations = new StateSpecOperationsSource<KegModel, int>()
                {
                    Get = ServiceOperations.Get,
                    InitialPost = ServiceOperations.Create,
                    Post = ServiceOperations.Update,
                    Put = ServiceOperations.Update,
                    Delete = ServiceOperations.Delete
                }
            };
        }
    }
}