using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BeerTapV2.ApiServices.RequestContext;
using BeerTapV2.Model;
using DataAccess.Repostitories;
using IQ.Platform.Framework.WebApi;

namespace BeerTapV2.ApiServices
{
    public class TapApiService : ITapApiService
    {
        private readonly IExtractDataFromARequestContext _requestContextExtractor;
        private ITapRepository _repository;

        public TapApiService(IExtractDataFromARequestContext requestContextExtractor, ITapRepository tapRepository)
        {
            _requestContextExtractor = requestContextExtractor;
            _repository = tapRepository;
        }

        public Task<TapModel> GetAsync(int id, IRequestContext context, CancellationToken cancellation)
        {
            _requestContextExtractor.ExtractOfficeId<TapModel>(context);

            var tap = _repository.GetTapById(id);

            // To do: Use AutoMapper for models and entities
            return Task.FromResult(new TapModel()
            {
                Id = tap.Id,
                Name = tap.Name,
                OfficeId = tap.OfficeId,
                KegId = tap.KegId,
                KegState = (KegState)Enum.Parse(typeof(KegState), tap.KegState, true)
            });
        }

        public Task<IEnumerable<TapModel>> GetManyAsync(IRequestContext context, CancellationToken cancellation)
        {
            _requestContextExtractor.ExtractOfficeId<TapModel>(context);

            var taps = _repository.GetTaps();
            var tapModels = new List<TapModel>();

            foreach (var tap in taps)
            {
                tapModels.Add(new TapModel()
                {
                    Id = tap.Id,
                    Name = tap.Name,
                    OfficeId = tap.OfficeId,
                    KegId = tap.KegId,
                    KegState = (KegState)Enum.Parse(typeof(KegState), tap.KegState, true)
                });
            }

            return Task.FromResult(tapModels.AsEnumerable());
        }
    }
}
