using System;

namespace Cirrious.Conference.Core.Models
{
    public class ApplicationSettingsState
    {
        public DateTime DataLastUpdatedUtc { get; set; }

        public static ApplicationSettingsState Default()
        {
            return new ApplicationSettingsState()
                       {
                           DataLastUpdatedUtc = DateTime.MinValue
                       };
        }
    }
}