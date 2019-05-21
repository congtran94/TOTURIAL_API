﻿using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TOTURIAL_API.Helpers
{
    public class AppSettings
    {
        public string Secret { get; set; }
        public AppSettings()
        {
            Secret = new Guid().ToString();
        }

    }
}