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
    public class ReplaceKegApiService : IReplaceKegApiService
    {
        private readonly IExtractDataFromARequestContext _requestContextExtractor;
        private IKegRepository _kegRepository;
        private ITapRepository _tapRepository;

        public ReplaceKegApiService(IExtractDataFromARequestContext requestContextExtractor, IKegRepository kegRepository, ITapRepository tapRepository)
        {
            _requestContextExtractor = requestContextExtractor;
            _kegRepository = kegRepository;
            _tapRepository = tapRepository;
        }

        public Task<ResourceCreationResult<ReplaceKegModel, int>> CreateAsync(ReplaceKegModel resource, IRequestContext context, CancellationToken cancellation)
        {
            _requestContextExtractor.ExtractOfficeId<TapModel>(context);
            _requestContextExtractor.ExtractTapId<TapModel>(context);

            var tapOption = context.UriParameters.GetByName<int>("tapId");
            var tapId = tapOption.EnsureValue(() => context.CreateHttpResponseException<TapModel>("Cannot find tap identifier in the uri", HttpStatusCode.BadRequest));
            var tap = _tapRepository.GetTapById(tapId);

            // Get keg to retrieve other info then update
            var keg = _kegRepository.GetKegById(tap.KegId);

            _kegRepository.Update(new Keg()
            {
                Id = keg.Id,
                Name = resource.Name, // Take from resource
                TapId = keg.TapId,
                Content = keg.MaxContent, // Refill the keg to full
                MaxContent = keg.MaxContent,
                UnitOfMeasurement = keg.UnitOfMeasurement
            });
            _kegRepository.Save();

            // Update Tap's keg state record
            _tapRepository.Update(new Tap()
            {
                Id = tap.Id,
                KegState = KegState.New.ToString(), // Set the state to new
                OfficeId = tap.OfficeId,
                Name = tap.Name,
                KegId = tap.KegId
            });
            _tapRepository.Save();

            return Task.FromResult(new ResourceCreationResult<ReplaceKegModel, int>(resource));
        }
    }
}
