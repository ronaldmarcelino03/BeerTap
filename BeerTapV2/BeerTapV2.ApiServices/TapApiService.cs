using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BeerTapV2.Model;
using DataAccess.Repostitories;
using IQ.Platform.Framework.WebApi;

namespace BeerTapV2.ApiServices
{
    public class TapApiService : ITapApiService
    {
        public Task<TapModel> GetAsync(int id, IRequestContext context, CancellationToken cancellation)
        {
            var tap = new TapRepository().GetTapById(id);

            // To do: Use AutoMapper for models and entities
            return Task.FromResult(new TapModel()
            {
                Id = tap.Id,
                Name = tap.Name,
                OfficeId = tap.OfficeId
            });
        }

        public Task<IEnumerable<TapModel>> GetManyAsync(IRequestContext context, CancellationToken cancellation)
        {
            var taps = new TapRepository().GetTaps();
            var tapModels = new List<TapModel>();

            foreach (var tap in taps)
            {
                tapModels.Add(new TapModel()
                {
                    Id = tap.Id,
                    Name = tap.Name,
                    OfficeId = tap.OfficeId
                });
            }

            return Task.FromResult(tapModels.AsEnumerable());
        }
    }
}
