using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BeerTapV2.Model;
using IQ.Platform.Framework.WebApi.Hypermedia;
using IQ.Platform.Framework.WebApi.Model.Hypermedia;

namespace BeerTapV2.WebApi.Hypermedia
{
    public class KegStateProvider : KegStateProvider<TapModel>
    {

    }

    public class KegStateProvider<TTapResource> : ResourceStateProviderBase<TTapResource, KegState>
        where TTapResource : IStatefulResource<KegState>, IStatefulKeg
    {
        public override KegState GetFor(TTapResource resource)
        {
            return resource.KegState;
        }

        protected override IDictionary<KegState, IEnumerable<KegState>> GetTransitions()
        {
            return new Dictionary<KegState, IEnumerable<KegState>>
            {
                {
                    KegState.New, new[]
                    {
                        KegState.GoingDown
                    }
                },
                {
                    KegState.GoingDown, new []
                    {
                        KegState.AlmostEmpty
                    }
                },
                {
                    KegState.AlmostEmpty, new []
                    {
                        KegState.Empty
                    }
                }
            };
        }

        public override IEnumerable<KegState> All { get; }
    }

    //public class PlaceStateProvider : PlaceStateProvider<Place>
    //{
    //}
    //public abstract class PlaceStateProvider<TPlaceResource> : ResourceStateProviderBase<TPlaceResource, PlaceState>
    //where TPlaceResource : IStatefulResource<PlaceState>, IStatefulPlace
    //{
    //    public override PlaceState GetFor(TPlaceResource resource)
    //    {
    //        return resource.PlaceState;
    //    }
    //    protected override IDictionary<PlaceState, IEnumerable<PlaceState>> GetTransitions()
    //    {
    //        return new Dictionary<PlaceState, IEnumerable<PlaceState>>
    //        {
    //            // from, to
    //            {
    //                PlaceState.Created, new[]
    //                {
    //                    PlaceState.Full
    //                }
    //            },
    //        };
    //    }
    //    public override IEnumerable<PlaceState> All
    //    {
    //        get { return EnumEx.GetValuesFor<PlaceState>(); }
    //    }
    //}
}