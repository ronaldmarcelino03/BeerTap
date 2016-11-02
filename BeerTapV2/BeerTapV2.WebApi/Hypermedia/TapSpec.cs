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

        //public static ResourceUriTemplate UriTapAtOffice = ResourceUriTemplate.Create("Offices({OfficeId})/Taps({id})");
        public static ResourceUriTemplate UriTapAtOffice = ResourceUriTemplate.Create("Offices({OfficeId})/Taps");

        public override string EntrypointRelation
        {
            get { return LinkRelations.Tap; }
        }

        protected override IEnumerable<ResourceLinkTemplate<TapModel>> Links()
        {
            //yield return CreateLinkTemplate(CommonLinkRelations.Self, UriTapAtOffice, c => c.OfficeId, c => c.Id);
            yield return CreateLinkTemplate(CommonLinkRelations.Self, UriTapAtOffice, c => c.OfficeId);

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
                            CreateLinkTemplate(LinkRelations.Keg, KegSpec.UriTapOfKegAtOffice, c => c.Id, c => c.KegId)
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
        //            CreateLinkTemplate(LinkRelations.Kegs.GetAllYouWant, TapSpec.UriTapAtOffice.Many, _ => _.Id)
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
        //            CreateLinkTemplate(LinkRelations.Kegs.GetSomeMore, TapSpec.UriTapAtOffice.Many, _ => _.Id)
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
        //            CreateLinkTemplate(LinkRelations.Kegs.GetWhatIsLeft, TapSpec.UriTapAtOffice.Many, _ => _.Id)
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
        //            CreateLinkTemplate(LinkRelations.Kegs.ReplaceKeg, TapSpec.UriTapAtOffice.Many, _ => _.Id)
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