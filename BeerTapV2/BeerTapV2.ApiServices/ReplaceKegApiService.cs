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
        private ITapRepository _tapRepository;

        public ReplaceKegApiService(IExtractDataFromARequestContext requestContextExtractor, ITapRepository tapRepository)
        {
            _requestContextExtractor = requestContextExtractor;
            _tapRepository = tapRepository;
        }

        public Task<ResourceCreationResult<ReplaceKegModel, int>> CreateAsync(ReplaceKegModel resource, IRequestContext context, CancellationToken cancellation)
        {
            _requestContextExtractor.ExtractOfficeId<TapModel>(context);
            _requestContextExtractor.ExtractTapId<TapModel>(context);

            var tapOption = context.UriParameters.GetByName<int>("tapId");
            var tapId = tapOption.EnsureValue(() => context.CreateHttpResponseException<TapModel>("Cannot find tap identifier in the uri", HttpStatusCode.BadRequest));
            var tap = _tapRepository.GetTapById(tapId);

	        if (tap.KegState == KegState.Empty.ToString())
	        {
		        var beerName = resource.BeerName ?? tap.BeerName; // Take from resource if not null

		        // Update tap record
		        _tapRepository.Update(new Tap()
		        {
			        Id = tap.Id,
			        OfficeId = tap.OfficeId,
			        BeerName = beerName,
			        Content = tap.MaxContent, // Refill the keg to full
			        MaxContent = tap.MaxContent,
			        UnitOfMeasurement = tap.UnitOfMeasurement,
			        KegState = KegState.New.ToString(), // Set the state to new
		        });
		        _tapRepository.Save();

				// Update resource before returning
		        resource.BeerName = beerName;
	        }

			return Task.FromResult(new ResourceCreationResult<ReplaceKegModel, int>(resource));
        }
    }
}
