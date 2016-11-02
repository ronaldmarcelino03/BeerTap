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
                    KegState = (KegState) Enum.Parse(typeof(KegState), keg.KegState, true),
                    Content = keg.Content,
                    MaxContent = keg.MaxContent,
                    UnitOfMeasurement = keg.UnitOfMeasurement,
                    Name = keg.Name
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
                        KegState = (KegState)Enum.Parse(typeof(KegState), keg.KegState, true),
                        Content = keg.Content,
                        MaxContent = keg.MaxContent,
                        UnitOfMeasurement = keg.UnitOfMeasurement,
                        Name = keg.Name
                    });
                }

                return Task.FromResult(kegModels.AsEnumerable());
            }
        }

        //public Task<ResourceCreationResult<KegModel, int>> CreateAsync(KegModel resource, IRequestContext context, CancellationToken cancellation)
        //{
        //    throw new NotImplementedException();

            // Put to replace keg api
            //// Replace keg
            //using (var repository = new KegRepository())
            //{
            //    var keg = new Keg()
            //    {
            //        Id = resource.Id,
            //        KegState = KegState.New.ToString(), // Replaced keg shall have a state of New
            //        Content = resource.MaxContent, // Refill fully
            //        MaxContent = resource.MaxContent,
            //        UnitOfMeasurement = resource.UnitOfMeasurement,
            //        Name = resource.Name // Type/name of beer
            //    };

            //    repository.Update(keg);
            //    repository.Save();

            //    // Update resource based on changes above
            //    resource.KegState = keg.KegState;
            //    resource.Content = keg.Content;
            //    resource.UnitOfMeasurement 
            //}
        //}
    }
}
