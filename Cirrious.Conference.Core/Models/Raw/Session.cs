using System;
using Newtonsoft.Json;

namespace Cirrious.Conference.Core.Models.Raw
{
    public class Session
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Speaker { get; set; }
        public string SpeakerTwitterName { get; set; }
        public string SpeakerWebsiteURL { get; set; }
        public string Sypnopsis { get; set; }
        public string RoomName { get; set; }
        public string TrackName { get; set; }
        public string SlotId { get; set; }

        [JsonIgnore]
        public Slot Slot { get; set; }
        [JsonIgnore]
        public DateTime When
        {
            get { return Slot == null ? HackyConstants.BaseDateTimeLocal : Slot.Start(); }
        }
    }
}
