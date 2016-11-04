using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using IQ.Platform.Framework.WebApi.Services.Security;
using BeerTapV2.ApiServices.Security;
using BeerTapV2.Model;
using DataAccess.Repostitories;
using IQ.Platform.Framework.WebApi;

namespace BeerTapV2.ApiServices
{
    public class OfficeApiService : IOfficeApiService
    {

        readonly IApiUserProvider<BeerTapV2ApiUser> _userProvider;
        private IOfficeRepository _repository;

        public OfficeApiService(IApiUserProvider<BeerTapV2ApiUser> userProvider, IOfficeRepository repository)
        {
            if (userProvider == null)
                throw new ArgumentNullException("userProvider");
            _userProvider = userProvider;
            _repository = repository;
        }

        public Task<OfficeModel> GetAsync(int id, IRequestContext context, CancellationToken cancellation)
        {
            var office = _repository.GetOfficeModelById(id);

            return Task.FromResult(new OfficeModel()
            {
                Id = office.Id,
                Location = office.Location
            });
        }

        public Task<IEnumerable<OfficeModel>> GetManyAsync(IRequestContext context, CancellationToken cancellation)
        {
            var offices = _repository.GetOffices();
            var officeModels = new List<OfficeModel>();

            foreach (var office in offices)
            {
                officeModels.Add(new OfficeModel()
                {
                    Id = office.Id,
                    Location = office.Location
                });
            }

            return Task.FromResult(officeModels.AsEnumerable());
        }
    }
}
