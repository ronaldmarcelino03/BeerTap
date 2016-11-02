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
    public class TapStateProvider : TapStateProvider<TapModel>
    {

    }

    public class TapStateProvider<TTapResource> : ResourceStateProviderBase<TTapResource, TapState>
        where TTapResource : IStatefulResource<TapState>, IStatefulTap
    {
        public override TapState GetFor(TTapResource resource)
        {
            return resource.TapState;
        }

        protected override IDictionary<TapState, IEnumerable<TapState>> GetTransitions()
        {
            return new Dictionary<TapState, IEnumerable<TapState>>
            {
                {
                    TapState.New, new[]
                    {
                        TapState.GoingDown
                    }
                },
                {
                    TapState.GoingDown, new []
                    {
                        TapState.AlmostEmpty
                    }
                },
                {
                    TapState.AlmostEmpty, new []
                    {
                        TapState.Empty
                    }
                }
            };
        }

        public override IEnumerable<TapState> All { get; }
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