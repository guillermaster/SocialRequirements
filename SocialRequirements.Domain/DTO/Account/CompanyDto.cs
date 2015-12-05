using System;

namespace SocialRequirements.Domain.DTO.Account
{
    [Serializable]
    public class CompanyDto
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public int TypeId { get; set; }

        public string Type { get; set; }

        public string Error { get; set; }
    }
}
