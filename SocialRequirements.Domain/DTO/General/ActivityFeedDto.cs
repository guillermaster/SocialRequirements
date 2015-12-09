﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialRequirements.Domain.DTO.General
{
    public class ActivityFeedDto
    {
        public long Id { get; set; }

        public long CompanyId { get; set; }

        public long? ProjectId { get; set; }

        public int EntityId { get; set; }

        public string EntityName { get; set; }

        public long RecordId { get; set; }

        public DateTime Createdon { get; set; }

        public long CreatedbyId { get; set; }

        public string CreatedByName { get; set; }

        public string CreatedByLastname { get; set; }
    }
}
