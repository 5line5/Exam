using System;
using System.Text;
using static Refactoring.Commands;
using static Refactoring.DefaultValues;

namespace Refactoring
{
    public static class DefaultValues
    {
        public const int OffTvDefaultVolume = OnTvDefaultVolume;
        public const int OffTvDefaultBrightness = 0;
        public const int OffTvDefaultContrast = 0;

        public const int OnTvDefaultVolume = 0;
        public const int OnTvDefaultBrightness = 20;
        public const int OnTvDefaultContrast = 20;
        
        public const int VolumeChange = 1;
        public const int BrightnessChange = 10;
        public const int ContrastChange = 10;
        public const bool TvOnState = true;
        public const bool TvOffState = false;
    }

    public static class Commands
    {
        public const string TvOn = "Tv On";
        public const string TvOff = "Tv Off";
        public const string VolumeUp = "Volume Up";
        public const string VolumeDown = "Volume Down";
        public const string ShowOptions = "Options show";
        public const string BrightnessUp = "Options change brightness up";
        public const string BrightnessDown = "Options change brightness down";
        public const string ContrastUp = "Options change contrast up";
        public const string ContrastDown = "Options change contrast down";
    }
    
    public class TvOptions
    {
        private bool _isOnline;
        private int _volume;
        private int _brightness;
        private int _contrast;

        private void SetOffTvDefaultSettings()
        {
            _isOnline = TvOffState;
            _volume = OffTvDefaultVolume;
            _brightness = OffTvDefaultBrightness;
            _contrast = OffTvDefaultContrast;
        }

        private void SetOnTvDefaultSettings()
        {
            _isOnline = TvOnState;
            _volume = OnTvDefaultVolume;
            _brightness = OnTvDefaultBrightness;
            _contrast = OnTvDefaultContrast;
        }

        public TvOptions() => SetOffTvDefaultSettings();
        
        public void ChangeVolume(int value) => _volume += value;

        public void ChangeBrightness(int value) => _brightness += value;

        public void ChangeContrast(int value) => _contrast += value;

        public void ChangeTvOnlineState(bool newState)
        {
            _isOnline = newState;
            
            if (_isOnline) SetOnTvDefaultSettings();
            else SetOffTvDefaultSettings();
        }

        public string ShowOptions()
        {
            var sb = new StringBuilder();
            sb.AppendLine("Options:");
            sb.AppendLine($"Volume {_volume}");
            sb.AppendLine($"IsOnline {_isOnline}");
            sb.AppendLine($"Brightness {_brightness}");
            sb.AppendLine($"Contrast {_contrast}");
            
            return sb.ToString();
        }
    }

    public class RemoteController
    {
        private readonly TvOptions _tvOptions = new TvOptions();

        public string Call(string command)
        {
            var optionsState = string.Empty;
            
            switch (command)
            {
                case TvOn:
                    _tvOptions.ChangeTvOnlineState(TvOnState);
                    break;
                case TvOff:
                    _tvOptions.ChangeTvOnlineState(TvOffState);
                    break;
                case VolumeUp:
                    _tvOptions.ChangeVolume(VolumeChange);
                    break;
                case VolumeDown:
                    _tvOptions.ChangeVolume(-VolumeChange);
                    break;
                case ShowOptions:
                    optionsState = _tvOptions.ShowOptions();
                    break;
                case BrightnessUp:
                    _tvOptions.ChangeBrightness(BrightnessChange);
                    break;
                case BrightnessDown:
                    _tvOptions.ChangeBrightness(-BrightnessChange);
                    break;
                case ContrastUp:
                    _tvOptions.ChangeContrast(ContrastChange);
                    break;
                case ContrastDown:
                    _tvOptions.ChangeContrast(-ContrastChange);
                    break;
                default:
                    Console.WriteLine($"Unknown command: {command}.");
                    break;
            }

            return optionsState;
        }
    }
}