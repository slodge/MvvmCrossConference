using System;
using System.Linq;

namespace Cirrious.Conference.Core.ViewModels.SessionLists
{
    public class SessionListViewModel
        : BaseSessionListViewModel<DateTime>
    {       
        public SessionListViewModel()
        {
            var grouped = Service.Sessions
                .Values
                .GroupBy(slot => slot.Session.When)
                .OrderBy(slot => slot.Key)
                .Select(slot => new SessionGroup(
                                    slot.Key,
                                    slot.OrderBy(session => session.Session.Title),
                                    NavigateToSession));

            Title = TextSource.GetText("Title");
            GroupedList = grouped.ToList();
        }

        private string _title;
        public string Title
        {
            get { return _title; }
            set { _title = value; FirePropertyChanged("Title"); }
        }
    }
}