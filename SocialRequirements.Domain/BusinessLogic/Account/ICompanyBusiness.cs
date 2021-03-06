﻿using System.Collections.Generic;
using SocialRequirements.Domain.DTO.Account;
using SocialRequirements.Domain.DTO.General;

namespace SocialRequirements.Domain.BusinessLogic.Account
{
    public interface ICompanyBusiness
    {
        List<CompanyDto> GetCompaniesByUser(string username);

        List<GeneralCatalogDetailDto> GetCompanyTypes();

        void Add(string companyName, int companyType, string username);

        void Set(long companyId, string username);

        List<CompanyDto> GetAll();

        List<CompanyDto> GetCompaniesByProject(long projectId);
    }
}
