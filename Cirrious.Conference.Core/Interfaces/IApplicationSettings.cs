using System;

namespace Cirrious.Conference.Core.Interfaces
{
    public interface IApplicationSettings
    {
        DateTime DataLastUpdatedUtc { get; set; }
    }
}