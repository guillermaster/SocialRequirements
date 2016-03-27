using System.Collections.Generic;
using SocialRequirements.Domain.BusinessLogic.General;
using SocialRequirements.Domain.DTO.General;
using SocialRequirements.Domain.Repository.General;

namespace SocialRequirements.Business.General
{
    public class GeneralCatalogBusiness : IGeneralCatalogBusiness
    {
        private readonly IGeneralCatalogData _generalCatalogData;

        public GeneralCatalogBusiness(IGeneralCatalogData generalCatalogData)
        {
            _generalCatalogData = generalCatalogData;
        }

        public List<GeneralCatalogDetailDto> Get(int generalCatalogHeaderId)
        {
            return _generalCatalogData.Get(generalCatalogHeaderId);
        }
    }
}
