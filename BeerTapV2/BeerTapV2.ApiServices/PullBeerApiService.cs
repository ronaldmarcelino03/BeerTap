using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BeerTapV2.ApiServices.RequestContext;
using BeerTapV2.Model;
using DataAccess;
using DataAccess.Entities;
using DataAccess.Repostitories;
using IQ.Platform.Framework.Common;
using IQ.Platform.Framework.WebApi;

namespace BeerTapV2.ApiServices
{
    public class PullBeerApiService : IPullBeerApiService
    {
        private readonly IExtractDataFromARequestContext _requestContextExtractor;
        private IKegRepository _kegRepository;
        private ITapRepository _tapRepository;

        public PullBeerApiService(IExtractDataFromARequestContext requestContextExtractor, ITapRepository tapRepository, IKegRepository kegRepository)
        {
            _requestContextExtractor = requestContextExtractor;
            _kegRepository = kegRepository;
            _tapRepository = tapRepository;
        }

        public Task<ResourceCreationResult<PullBeerModel, int>> CreateAsync(PullBeerModel resource, IRequestContext context, CancellationToken cancellation)
        {
            _requestContextExtractor.ExtractTapId<TapModel>(context);

            // To be refactored/auto-mapped
            var tapOption = context.UriParameters.GetByName<int>("tapId");
            var tapId = tapOption.EnsureValue(() => context.CreateHttpResponseException<TapModel>("The tapId must be supplied in the URI", HttpStatusCode.BadRequest));
            var tap = _tapRepository.GetTapById(tapId);
            var keg = _kegRepository.GetKegById(tap.KegId);
            var newContent = GetNewContent(keg.Content, resource.Volume);
            var newKegState = GetKegState(newContent, keg.MaxContent);

            // Update keg record
            _kegRepository.Update(new Keg()
            {
                Id = keg.Id,
                TapId = keg.TapId,
                Name = keg.Name,
                MaxContent = keg.MaxContent,
                Content = newContent,
                UnitOfMeasurement = keg.UnitOfMeasurement
            });
            _kegRepository.Save();

            // Update tap record's keg state
            _tapRepository.Update(new Tap()
            {
                Id = tap.Id,
                KegId = tap.KegId,
                Name = tap.Name,
                KegState = newKegState.ToString()
            });
            _tapRepository.Save();

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
            return numerator / denominator * 100;
        }

        private int GetNewContent(int content, int volume)
        {
            var newContent = content - volume;

            return newContent < 0 ? 0 : newContent;
        }
    }
}
