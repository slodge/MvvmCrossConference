using System.Collections.Generic;

namespace Cirrious.Conference.Core.Models.Raw
{
    public class Track
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public List<string> SessionIds { get; set; }
    }
}