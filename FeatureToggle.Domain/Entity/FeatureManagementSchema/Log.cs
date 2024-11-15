﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeatureToggle.Domain.Entity.FeatureManagementSchema
{
    public class Log
    {
        public int Id { get; private set; }
        public User User { get; private set; }
        public string UserId { get; private set; }
        //public BusinessFeatureFlag BusinessFeature { get; private set; }
        public int BusinessFeatureId { get; private set; }
        public DateTime Time { get; private set; }
        public Actions Action { get; private set; }

        public enum Actions
        {
            Enabled, Disabled
        }
    }
}
