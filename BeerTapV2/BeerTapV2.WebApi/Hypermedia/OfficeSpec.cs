﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BeerTapV2.ApiServices;
using BeerTapV2.Model;
using IQ.Platform.Framework.WebApi.Hypermedia;
using IQ.Platform.Framework.WebApi.Hypermedia.Specs;
using IQ.Platform.Framework.WebApi.Model.Hypermedia;

namespace BeerTapV2.WebApi.Hypermedia
{
    public class OfficeSpec : SingleStateResourceSpec<OfficeModel, int>
    {
        public static ResourceUriTemplate Uri = ResourceUriTemplate.Create("Offices({id})");

        public override string EntrypointRelation
        {
            get { return LinkRelations.Office; }
        }

        protected override IEnumerable<ResourceLinkTemplate<OfficeModel>> Links()
        {
            yield return CreateLinkTemplate(CommonLinkRelations.Self, Uri, c => c.Id);
        }

        public override IResourceStateSpec<OfficeModel, NullState, int> StateSpec
        {
            get
            {
                return
                    new SingleStateSpec<OfficeModel, int>
                    {
                        Links =
                        {
                            CreateLinkTemplate(LinkRelations.Tap, TapSpec.Uri.Many, c => c.Id)
                        },
                        Operations = new StateSpecOperationsSource<OfficeModel, int>
                        {
                            Get = ServiceOperations.Get
                        }
                    };
            }
        }
    }
}