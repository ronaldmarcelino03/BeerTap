using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BeerTapV2.Model;
using DataAccess;
using DataAccess.Entities;
using DataAccess.Repostitories;
using IQ.Platform.Framework.WebApi;

namespace BeerTapV2.ApiServices
{
    public class ReplaceKegApiService : IReplaceKegApiService
    {
        public Task<ResourceCreationResult<ReplaceKegModel, int>> CreateAsync(ReplaceKegModel resource, IRequestContext context, CancellationToken cancellation)
        {
            // Update keg record
            using (var kegRepository = new KegRepository())
            {
                // Get keg to retrieve other info
                var keg = kegRepository.GetKegById(resource.KegId);

                kegRepository.Update(new Keg()
                {
                    Id = resource.KegId,
                    Name = resource.Name,
                    OfficeId = keg.OfficeId,
                    TapId = keg.TapId,
                    Content = keg.MaxContent, // Refill the keg to full
                    MaxContent = keg.MaxContent,
                    UnitOfMeasurement = keg.UnitOfMeasurement
                });
                kegRepository.Save();

                // Update Tap's keg state record
                using (var tapRepository = new TapRepository())
                {
                    tapRepository.Update(new Tap()
                    {
                        Id = resource.TapId,
                        KegState = KegState.New.ToString(), // Set the state to new
                        OfficeId = resource.OfficeId,
                        Name = resource.Name,
                        KegId = resource.KegId
                    });
                    tapRepository.Save();
                }
            }

            return Task.FromResult(new ResourceCreationResult<ReplaceKegModel, int>(resource));
        }
    }
}
