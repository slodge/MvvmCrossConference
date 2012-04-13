using System;
using Cirrious.Conference.Core.Interfaces;
using Cirrious.MvvmCross.ExtensionMethods;
using Cirrious.MvvmCross.Interfaces.Platform;
using Cirrious.MvvmCross.Interfaces.ServiceProvider;
using Newtonsoft.Json;

namespace Cirrious.Conference.Core.Models
{
    public class ApplicationSettings
        : IMvxServiceConsumer<IMvxSimpleFileStoreService>, IApplicationSettings
    {
        private readonly ApplicationSettingsState _state;

        public ApplicationSettings()
        {
            _state = LoadOrDefault();
        }

        private ApplicationSettingsState LoadOrDefault()
        {
            try
            {
                string contents;
                if (this.GetService().TryReadTextFile(Constants.SettingsFileName, out contents))
                {
                    return JsonConvert.DeserializeObject<ApplicationSettingsState>(contents);
                }
            }
            catch (Exception exception)
            {
                Trace.Warn("Error seen while loading applicaiton settings {0}", exception.ToLongString());
            }
            return ApplicationSettingsState.Default();
        }

        private void Save()
        {
            var settingsText = JsonConvert.SerializeObject(_state);
            this.GetService().WriteFile(Constants.SettingsFileName, settingsText);
        }

        private DateTime _dataLastUpdatedUtc;
        public DateTime DataLastUpdatedUtc
        {
            get { return _state.DataLastUpdatedUtc; }
            set { _state.DataLastUpdatedUtc = value; Save();}
        }
    }
}