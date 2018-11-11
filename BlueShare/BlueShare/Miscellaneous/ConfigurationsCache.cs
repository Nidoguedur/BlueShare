using System;
using System.Collections.Generic;
using System.Text;
using Android;
using Microsoft.Extensions.Caching.Memory;

namespace BlueShare.Miscellaneous
{
    public class ConfigurationsCache
    {
        private static readonly IMemoryCache Cache = new MemoryCache(new MemoryCacheOptions());
        
        public bool IsActiveBluetooth
        {
            get
            {
                if (Cache.TryGetValue("IsActiveBluetooth", out bool value))
                {
                    return value;
                }
                else
                {
                    return false;
                }
            }

            set => Cache.Set("IsActiveBluetooth", value, new DateTimeOffset());
        }

        public bool IsShare
        {
            get
            {
                if (Cache.TryGetValue("IsShare", out bool value))
                {
                    return value;
                }
                else
                {
                    return false;
                }
            }

            set => Cache.Set("IsShare", value, new DateTimeOffset());
        }

        public bool IsGroupRemember
        {
            get
            {
                if (Cache.TryGetValue("IsGroupRemember", out bool value))
                {
                    return value;
                }
                else
                {
                    return false;
                }
            }

            set => Cache.Set("IsGroupRemember", value, new DateTimeOffset());
        }

        public ConfigurationsCache()
        {

        }
    }
}
