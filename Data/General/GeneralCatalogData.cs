using System.Collections.Generic;
using System.Linq;
using SocialRequirements.Context;
using SocialRequirements.Context.Entities;
using SocialRequirements.Domain.DTO.General;
using SocialRequirements.Domain.Repository.General;

namespace SocialRequirements.Data.General
{
    public class GeneralCatalogData : IGeneralCatalogData
    {
        private readonly ContextModel _context;

        public GeneralCatalogData(ContextModel context)
        {
            _context = context;
        }

        public List<GeneralCatalogDetailDto> Get(int generalCatalogHeaderId)
        {
            var catalogDetails =
                _context.GeneralCatalogDetails.Where(c => c.generalcatalog_id == generalCatalogHeaderId).ToList();
            return catalogDetails.Select(GetDtoFromEntity).ToList();
        }

        private static GeneralCatalogDetailDto GetDtoFromEntity(GeneralCatalogDetail catalogDetail)
        {
            var catalogDetailDto = new GeneralCatalogDetailDto
            {
                Id = catalogDetail.id,
                Name = catalogDetail.name,
                CatalogId = catalogDetail.generalcatalog_id
            };
            return catalogDetailDto;
        }
    }
}
