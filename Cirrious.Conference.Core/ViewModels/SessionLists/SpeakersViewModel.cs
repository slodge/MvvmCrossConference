using System.Linq;

namespace Cirrious.Conference.Core.ViewModels.SessionLists
{
    public class SpeakersViewModel
        : BaseSessionListViewModel<string>
    {
        public SpeakersViewModel()
        {
            var grouped = Service.Sessions
                .Values
                .GroupBy(session => session.Session.Speaker)
                .OrderBy(slot => slot.Key)
                .Select(slot => new SessionGroup(
                                slot.Key,
                                slot.OrderBy(session => session.Session.Slot.Start()),
                                NavigateToSession));

            GroupedList = grouped.ToList();
        }
    }
}