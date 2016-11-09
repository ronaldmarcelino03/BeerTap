using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BeerTapV2.ApiServices.RequestContext;
using BeerTapV2.Model;
using DataAccess.Repostitories;
using IQ.Platform.Framework.Common;
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

			var officeOption = context.UriParameters.GetByName<int>("officeId");
			var officeId = officeOption.EnsureValue(() => context.CreateHttpResponseException<OfficeModel>("Cannot find tap identifier in the uri", HttpStatusCode.BadRequest));
			var tap = _repository.GetTapByOfficeAndTapIds(officeId, id);
			var tapModel = new TapModel();

			if (tap != null)
			{
				// To do: Use AutoMapper for models and entities
				tapModel = new TapModel()
				{
					Id = tap.Id,
					OfficeId = tap.OfficeId,
					BeerName = tap.BeerName,
					Content = tap.Content,
					MaxContent = tap.MaxContent,
					UnitOfMeasurement = tap.UnitOfMeasurement,
					KegState = (KegState) Enum.Parse(typeof(KegState), tap.KegState, true)
				};
			}

			return Task.FromResult(tapModel);
		}

		public Task<IEnumerable<TapModel>> GetManyAsync(IRequestContext context, CancellationToken cancellation)
		{
			_requestContextExtractor.ExtractOfficeId<TapModel>(context);

			var officeOption = context.UriParameters.GetByName<int>("officeId");
			var officeId = officeOption.EnsureValue(() => context.CreateHttpResponseException<OfficeModel>("Cannot find tap identifier in the uri", HttpStatusCode.BadRequest));
			var taps = _repository.GetTapsByOfficeId(officeId);
			var tapModels = new List<TapModel>();

			if (taps != null)
			{

				foreach (var tap in taps)
				{
					tapModels.Add(new TapModel()
					{
						Id = tap.Id,
						OfficeId = tap.OfficeId,
						BeerName = tap.BeerName,
						Content = tap.Content,
						MaxContent = tap.MaxContent,
						UnitOfMeasurement = tap.UnitOfMeasurement,
						KegState = (KegState) Enum.Parse(typeof(KegState), tap.KegState, true)
					});
				}
			}

			return Task.FromResult(tapModels.AsEnumerable());
		}
	}
}
