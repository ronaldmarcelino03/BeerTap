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
    public class TapSpec : SingleStateResourceSpec<TapModel, int>
    {

        public static ResourceUriTemplate UriTapAtOffice = ResourceUriTemplate.Create("Offices({OfficeId})/Taps({id})");

        public override string EntrypointRelation
        {
            get { return LinkRelations.Tap; }
        }

        public override IResourceStateSpec<TapModel, NullState, int> StateSpec
        {
            get
            {
                return
                    new SingleStateSpec<TapModel, int>
                    {
                        Links =
                        {
                            CreateLinkTemplate(LinkRelations.Office, OfficeSpec.Uri, c => c.OfficeId)
                        },
                        Operations = new StateSpecOperationsSource<TapModel, int>
                        {
                            Get = ServiceOperations.Get,
                            InitialPost = ServiceOperations.Create,
                            Post = ServiceOperations.Update,
                            Delete = ServiceOperations.Delete
                        }
                    };
            }
        }

        //protected override IEnumerable<IResourceStateSpec<TapModel, KegState, int>> GetStateSpecs()
        //{
        //    // To be refactored
        //    // New Keg
        //    yield return new ResourceStateSpec<TapModel, KegState, int>(KegState.New)
        //    {
        //        Links =
        //        {
        //            CreateLinkTemplate(LinkRelations.Taps.GetAllYouWant, TapSpec.UriTapAtOffice.Many, _ => _.Id)
        //        },
        //        Operations = new StateSpecOperationsSource<TapModel, int>()
        //        {
        //            Get = ServiceOperations.Get,
        //            InitialPost = ServiceOperations.Create,
        //            Post = ServiceOperations.Update,
        //            Put = ServiceOperations.Update,
        //            Delete = ServiceOperations.Delete
        //        }
        //    };

        //    // Going down
        //    yield return new ResourceStateSpec<TapModel, KegState, int>(KegState.GoingDown)
        //    {
        //        Links =
        //        {
        //            CreateLinkTemplate(LinkRelations.Taps.GetSomeMore, TapSpec.UriTapAtOffice.Many, _ => _.Id)
        //        },
        //        Operations = new StateSpecOperationsSource<TapModel, int>()
        //        {
        //            Get = ServiceOperations.Get,
        //            InitialPost = ServiceOperations.Create,
        //            Post = ServiceOperations.Update,
        //            Put = ServiceOperations.Update,
        //            Delete = ServiceOperations.Delete
        //        }
        //    };

        //    // Almost empty
        //    yield return new ResourceStateSpec<TapModel, KegState, int>(KegState.AlmostEmpty)
        //    {
        //        Links =
        //        {
        //            CreateLinkTemplate(LinkRelations.Taps.GetWhatIsLeft, TapSpec.UriTapAtOffice.Many, _ => _.Id)
        //        },
        //        Operations = new StateSpecOperationsSource<TapModel, int>()
        //        {
        //            Get = ServiceOperations.Get,
        //            InitialPost = ServiceOperations.Create,
        //            Post = ServiceOperations.Update,
        //            Put = ServiceOperations.Update,
        //            Delete = ServiceOperations.Delete
        //        }
        //    };

        //    // Empty
        //    yield return new ResourceStateSpec<TapModel, KegState, int>(KegState.Empty)
        //    {
        //        Links =
        //        {
        //            CreateLinkTemplate(LinkRelations.Taps.ReplaceKeg, TapSpec.UriTapAtOffice.Many, _ => _.Id)
        //        },
        //        Operations = new StateSpecOperationsSource<TapModel, int>()
        //        {
        //            Get = ServiceOperations.Get,
        //            InitialPost = ServiceOperations.Create,
        //            Post = ServiceOperations.Update,
        //            Put = ServiceOperations.Update,
        //            Delete = ServiceOperations.Delete
        //        }
        //    };
        //}
    }
}