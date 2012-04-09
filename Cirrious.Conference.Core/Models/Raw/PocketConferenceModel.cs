using System.Collections.Generic;

namespace Cirrious.Conference.Core.Models.Raw
{
    public class PocketConferenceModel
    {
        public List<string> TrackIds { get; set; }
        public Dictionary<string, Track> Tracks { get; set; }
        public List<string> SlotIds { get; set; }
        public Dictionary<string, Slot> Slots { get; set; }
        public List<string> SessionIds { get; set; }
        public Dictionary<string, Session> Sessions { get; set; }
        public ConferenceDetails DDDEvent { get; set; }
        public List<string> SessionIdsSortedBySpeakerName { get; set; }
    }
}