using System.Collections.Generic;
using SocialRequirements.Domain.DTO.General;

namespace SocialRequirements.Domain.BusinessLogic.General
{
    public interface IGeneralCatalogBusiness
    {
        List<GeneralCatalogDetailDto> Get(int generalCatalogHeaderId);
    }
}
