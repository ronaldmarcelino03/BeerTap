using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BeerTapV2.Model;
using DataAccess.Entities;
using DataAccess.Repostitories;
using IQ.Platform.Framework.WebApi;

namespace BeerTapV2.ApiServices
{
    public class KegApiService : IKegApiService
    {
        public Task<KegModel> GetAsync(int id, IRequestContext context, CancellationToken cancellation)
        {
            using (var repository = new KegRepository())
            {
                var keg = repository.GetKegById(id);

                return Task.FromResult(new KegModel()
                {
                    Id = keg.Id,
                    Content = keg.Content,
                    MaxContent = keg.MaxContent,
                    UnitOfMeasurement = keg.UnitOfMeasurement,
                    Name = keg.Name,
                    TapId = keg.TapId,
                    OfficeId = keg.OfficeId
                });
            }
        }

        public Task<IEnumerable<KegModel>> GetManyAsync(IRequestContext context, CancellationToken cancellation)
        {
            using (var repository = new KegRepository())
            {
                var kegs = repository.GetKegs();
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
                        TapId = keg.TapId,
                        OfficeId = keg.OfficeId
                    });
                }

                return Task.FromResult(kegModels.AsEnumerable());
            }
        }
    }
}
