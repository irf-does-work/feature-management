﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FeatureToggle.Domain.Entity.FeatureManagementSchema;

namespace FeatureToggle.Application.DTOs
{
    public class LogDTO
    {
        public int LogId { get; set; }

        public string UserId { get; set; }

        public string UserName { get; set; }

        public int FeatureId { get; set; }

        public string FeatureName { get; set; }

        public int? BusinessId { get; set; }

        public string? BusinessName { get; set; }
        public DateTime Time {  get; set; }

        public Actions Action { get; set; }
    }
}
