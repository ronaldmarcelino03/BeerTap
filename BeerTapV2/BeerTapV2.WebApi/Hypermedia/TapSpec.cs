using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BeerTapV2.Model;
using IQ.Platform.Framework.WebApi.Hypermedia;
using IQ.Platform.Framework.WebApi.Hypermedia.Specs;

namespace BeerTapV2.WebApi.Hypermedia
{
    public class TapSpec : ResourceSpec<TapModel, TapState, int>
    {

        public static ResourceUriTemplate UriTapAtOffice = ResourceUriTemplate.Create("Offices({OfficeId})/Taps({id})");

        public override string EntrypointRelation
        {
            get { return LinkRelations.Tap; }
        }

        protected override IEnumerable<IResourceStateSpec<TapModel, TapState, int>> GetStateSpecs()
        {
            // To be refactored
            // New Keg
            yield return new ResourceStateSpec<TapModel, TapState, int>(TapState.New)
            {
                Links =
                {
                    CreateLinkTemplate(LinkRelations.Taps.GetAllYouWant, TapSpec.UriTapAtOffice.Many, _ => _.Id)
                },
                Operations = new StateSpecOperationsSource<TapModel, int>()
                {
                    Get = ServiceOperations.Get,
                    InitialPost = ServiceOperations.Create,
                    Post = ServiceOperations.Update,
                    Put = ServiceOperations.Update,
                    Delete = ServiceOperations.Delete
                }
            };

            // Going down
            yield return new ResourceStateSpec<TapModel, TapState, int>(TapState.GoingDown)
            {
                Links =
                {
                    CreateLinkTemplate(LinkRelations.Taps.GetSomeMore, TapSpec.UriTapAtOffice.Many, _ => _.Id)
                },
                Operations = new StateSpecOperationsSource<TapModel, int>()
                {
                    Get = ServiceOperations.Get,
                    InitialPost = ServiceOperations.Create,
                    Post = ServiceOperations.Update,
                    Put = ServiceOperations.Update,
                    Delete = ServiceOperations.Delete
                }
            };

            // Almost empty
            yield return new ResourceStateSpec<TapModel, TapState, int>(TapState.AlmostEmpty)
            {
                Links =
                {
                    CreateLinkTemplate(LinkRelations.Taps.GetWhatIsLeft, TapSpec.UriTapAtOffice.Many, _ => _.Id)
                },
                Operations = new StateSpecOperationsSource<TapModel, int>()
                {
                    Get = ServiceOperations.Get,
                    InitialPost = ServiceOperations.Create,
                    Post = ServiceOperations.Update,
                    Put = ServiceOperations.Update,
                    Delete = ServiceOperations.Delete
                }
            };

            // Empty
            yield return new ResourceStateSpec<TapModel, TapState, int>(TapState.Empty)
            {
                Links =
                {
                    CreateLinkTemplate(LinkRelations.Taps.ReplaceKeg, TapSpec.UriTapAtOffice.Many, _ => _.Id)
                },
                Operations = new StateSpecOperationsSource<TapModel, int>()
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