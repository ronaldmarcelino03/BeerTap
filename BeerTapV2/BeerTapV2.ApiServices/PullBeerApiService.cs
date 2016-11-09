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
		private ITapRepository _tapRepository;

		public PullBeerApiService(IExtractDataFromARequestContext requestContextExtractor, ITapRepository tapRepository)
		{
			_requestContextExtractor = requestContextExtractor;
			_tapRepository = tapRepository;
		}

		public Task<ResourceCreationResult<PullBeerModel, int>> CreateAsync(PullBeerModel resource, IRequestContext context, CancellationToken cancellation)
		{
			_requestContextExtractor.ExtractTapId<TapModel>(context);

			// To be refactored/auto-mapped
			var officeOption = context.UriParameters.GetByName<int>("officeId");
			var officeId = officeOption.EnsureValue(() => context.CreateHttpResponseException<OfficeModel>("Cannot find tap identifier in the uri", HttpStatusCode.BadRequest));
			var tapOption = context.UriParameters.GetByName<int>("tapId");
			var tapId = tapOption.EnsureValue(() => context.CreateHttpResponseException<TapModel>("The tapId must be supplied in the URI", HttpStatusCode.BadRequest));
			var tap = _tapRepository.GetTapByOfficeAndTapIds(officeId, tapId);

			if (tap != null)
			{
				if (tap.KegState != KegState.Empty.ToString())
				{
					int volumePulled;
					var newContent = GetNewContent(tap.Content, resource.Volume, out volumePulled);
					var newKegState = GetKegState(newContent, tap.MaxContent);

					// Update tap record
					_tapRepository.Update(new Tap()
					{
						Id = tap.Id,
						OfficeId = tap.OfficeId,
						BeerName = tap.BeerName,
						Content = newContent,
						MaxContent = tap.MaxContent,
						UnitOfMeasurement = tap.UnitOfMeasurement,
						KegState = newKegState.ToString()
					});
					_tapRepository.Save();

					// Update resource before returning
					resource.Volume = volumePulled;
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
			return numerator / denominator * 100;
		}

		private int GetNewContent(int content, int volume, out int volumePulled)
		{
			var newContent = content - volume;

			if (newContent < 0)
			{
				volumePulled = volume - (newContent*-1);
				newContent = 0;
			}
			else
				volumePulled = volume;

			return newContent;
		}
	}
}
