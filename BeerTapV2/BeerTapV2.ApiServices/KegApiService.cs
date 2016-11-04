using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BeerTapV2.ApiServices.RequestContext;
using BeerTapV2.Model;
using DataAccess.Entities;
using DataAccess.Repostitories;
using IQ.Platform.Framework.WebApi;

namespace BeerTapV2.ApiServices
{
    public class KegApiService : IKegApiService
    {
        private readonly IExtractDataFromARequestContext _requestContextExtractor;
        private IKegRepository _repository;

        public KegApiService(IExtractDataFromARequestContext requestContextExtractor, IKegRepository repository)
        {
            _requestContextExtractor = requestContextExtractor;
            _repository = repository;
        }

        public Task<KegModel> GetAsync(int id, IRequestContext context, CancellationToken cancellation)
        {
            _requestContextExtractor.ExtractOfficeId<TapModel>(context);
            _requestContextExtractor.ExtractTapId<TapModel>(context);

            var keg = _repository.GetKegById(id);

            return Task.FromResult(new KegModel()
            {
                Id = keg.Id,
                Content = keg.Content,
                MaxContent = keg.MaxContent,
                UnitOfMeasurement = keg.UnitOfMeasurement,
                Name = keg.Name,
                TapId = keg.TapId
            });
        }

        public Task<IEnumerable<KegModel>> GetManyAsync(IRequestContext context, CancellationToken cancellation)
        {
            _requestContextExtractor.ExtractOfficeId<TapModel>(context);
            _requestContextExtractor.ExtractTapId<TapModel>(context);

            var kegs = _repository.GetKegs();
            var kegModels = new List<KegModel>();

            foreach (var keg in kegs)
            {
                kegModels.Add(new KegModel()
                {
                    Id = keg.Id,
                    Content = keg.Content,
                    MaxContent = keg.MaxContent,
                    UnitOfMeasurement = keg.UnitOfMeasurement,
                    Name = keg.Name,
                    TapId = keg.TapId
                });
            }

            return Task.FromResult(kegModels.AsEnumerable());
        }
    }
}
