using BlueShare.Miscellaneous;
using BlueShare.Views;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace BlueShare.ViewModels
{
    public class ConfigurationsViewModel : INotifyPropertyChanged
    {
        private static readonly IMemoryCache Cache = new MemoryCache(new MemoryCacheOptions());

        private bool _IsActiveBluetooth;
        public bool IsActiveBluetooth
        {
            get
            {
                if (Cache.TryGetValue("IsActiveBluetooth", out bool value))
                {
                    _IsActiveBluetooth = value;
                    return _IsActiveBluetooth;
                }
                else
                {
                    return false;
                }
            }

            set
            {
                _IsActiveBluetooth = value;
                Cache.Set("IsActiveBluetooth", _IsActiveBluetooth, new DateTimeOffset());
                OnPropertyChanged("IsActiveBluetooth");
            } 
        }

        private bool _IsShare;
        public bool IsShare
        {
            get
            {
                if (Cache.TryGetValue("IsShare", out bool value))
                {
                    _IsShare = value;
                    return _IsShare;
                }
                else
                {
                    return false;
                }
            }

            set
            {
                _IsShare = value;
                Cache.Set("IsShare", _IsShare, new DateTimeOffset());
                OnPropertyChanged("IsShare");
            }
        }

        private bool _IsGroupRemember;
        public bool IsGroupRemember
        {
            get
            {
                if (Cache.TryGetValue("IsGroupRemember", out bool value))
                {
                    _IsGroupRemember = value;
                    return _IsGroupRemember;
                }
                else
                {
                    return false;
                }
            }

            set
            {
                _IsGroupRemember = value;
                Cache.Set("IsGroupRemember", _IsGroupRemember, new DateTimeOffset());
                OnPropertyChanged("IsGroupRemember");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string NameProperty)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(NameProperty));
        }
    }
}
