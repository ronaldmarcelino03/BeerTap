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
    public class PullBeerApiService : IPullBeerApiService
    {
        public Task<ResourceCreationResult<PullBeerModel, int>> CreateAsync(PullBeerModel resource, IRequestContext context, CancellationToken cancellation)
        {
            using (var kegRepository = new KegRepository())
            {
                var keg = kegRepository.GetKegById(resource.KegId);
                var newContent = GetNewContent(keg.Content, resource.Volume);
                var newKegState = GetKegState(newContent, keg.MaxContent);

                // Update keg record
                kegRepository.Update(new Keg()
                {
                    Id = keg.Id,
                    OfficeId = keg.OfficeId,
                    TapId = keg.TapId,
                    Name = keg.Name,
                    MaxContent = keg.MaxContent,
                    Content = newContent,
                    UnitOfMeasurement = keg.UnitOfMeasurement
                });
                kegRepository.Save();

                // Update tap record's keg state
                using (var tapRepository = new TapRepository())
                {
                    tapRepository.Update(new Tap()
                    {
                        Id = keg.TapId,
                        KegId = keg.Id,
                        Name = keg.Name,
                        OfficeId = keg.OfficeId,
                        KegState = newKegState.ToString()
                    });
                    tapRepository.Save();
                }
            }

            return Task.FromResult(new ResourceCreationResult<PullBeerModel, int>(resource));
        }

        private KegState GetKegState(int content, int maxContent)
        {
            var percentage = GetPercentage(content, maxContent);

            if (percentage <= 0)
            {
                return KegState.Empty;
            }
            else if (percentage <= 25)
            {
                return KegState.AlmostEmpty;
            }
            else if (percentage < 100)
            {
                return KegState.GoingDown;
            }
            else
            {
                return KegState.New;
            }
        }

        private float GetPercentage(float numerator, float denominator)
        {
            return numerator/denominator * 100;
        }

        private int GetNewContent(int content, int volume)
        {
            var newContent = content - volume;

            return newContent < 0 ? 0 : newContent;
        }
    }
}
