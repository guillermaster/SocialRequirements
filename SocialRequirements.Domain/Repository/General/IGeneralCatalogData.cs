using System.Collections.Generic;
using SocialRequirements.Domain.DTO;
using SocialRequirements.Domain.DTO.General;

namespace SocialRequirements.Domain.Repository.General
{
    public interface IGeneralCatalogData
    {
        List<GeneralCatalogDetailDto> Get(int generalCatalogHeaderId);
    }
}
