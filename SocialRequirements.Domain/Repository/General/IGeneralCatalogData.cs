using System.Collections.Generic;
using SocialRequirements.Domain.DTO;

namespace SocialRequirements.Domain.Repository.General
{
    public interface IGeneralCatalogData
    {
        List<GeneralCatalogDetailDto> Get(int generalCatalogHeaderId);
    }
}
