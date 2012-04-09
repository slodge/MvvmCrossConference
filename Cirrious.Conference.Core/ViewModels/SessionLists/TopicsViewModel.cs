using System.Collections.Generic;
using System.Linq;
using Cirrious.Conference.Core.Models;
using Cirrious.Conference.Core.Models.Raw;
using Cirrious.Conference.Core.ViewModels.Helpers;
using Cirrious.MvvmCross.Commands;

namespace Cirrious.Conference.Core.ViewModels.SessionLists
{
    public class TopicsViewModel
        : BaseSessionListViewModel<string>
    {
        public TopicsViewModel()
        {
            var grouped = Service.Sessions
                .Values
                .GroupBy(slot => slot.Session.TrackName)
                .OrderBy(slot => slot.Key.ToString())
                .Select(slot => new SessionGroup(
                                slot.Key,
                                slot.OrderBy(session => session.Session.When),
                                NavigateToSession));

            GroupedList = grouped.ToList();
        }
    }
}