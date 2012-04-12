namespace Cirrious.Conference.Core.ViewModels
{
    using System.Collections.Generic;
    using System.Linq;

    using Cirrious.Conference.Core.Models.Raw;
    using Cirrious.Conference.Core.ViewModels.Helpers;
    using Cirrious.MvvmCross.Commands;

    public class BaseTeamViewModel : BaseConferenceViewModel
    {
        protected void LoadFrom(IEnumerable<Team> source)
        {
            var team = source
                .ToList();
            team.Sort((a, b) => a.DisplayOrder.CompareTo(b.DisplayOrder));
            Team = team.Select(x => new WithCommand<Team>(x, null)).ToList();
        }

        public IList<WithCommand<Team>> Team { get; private set; }        
    }
}