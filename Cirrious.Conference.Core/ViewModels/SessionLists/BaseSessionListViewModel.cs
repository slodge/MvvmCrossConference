using System;
using System.Collections.Generic;
using System.Linq;
using Cirrious.Conference.Core.Models;
using Cirrious.Conference.Core.Models.Raw;
using Cirrious.Conference.Core.ViewModels.Helpers;
using Cirrious.MvvmCross.Commands;

namespace Cirrious.Conference.Core.ViewModels.SessionLists
{
    public class BaseSessionListViewModel<TKey>
        : BaseConferenceViewModel
    {
        public class SessionGroup : List<WithCommand<SessionWithFavoriteFlag>>
        {
            public TKey Key { get; set; }

            public SessionGroup(TKey key, IEnumerable<SessionWithFavoriteFlag> items, Action<Session> tapAction)
                : base((IEnumerable<WithCommand<SessionWithFavoriteFlag>>) items.Select(x => new WithCommand<SessionWithFavoriteFlag>(x, new MvxRelayCommand(() => tapAction(x.Session)))))
            {
                Key = key;
            }
        }

        private List<SessionGroup> _groupedList;
        public List<SessionGroup> GroupedList
        {
            get { return _groupedList; }
            protected set { _groupedList = value; FirePropertyChanged("GroupedList"); RecreateFlattenedList(); }
        }

        private void RecreateFlattenedList()
        {
            if (_groupedList == null)
            {
                FlattenedList = null;
                return;
            }

            var flattened = new List<object>();
            foreach (var group in _groupedList)
            {
                flattened.Add(group.Key);
                flattened.AddRange(group.Select(x => (object)x));
            }

            FlattenedList = flattened;
        }

        private List<object> _flattenedList;
        public List<object> FlattenedList
        {
            get { return _flattenedList; }
            private set { _flattenedList = value; FirePropertyChanged("FlattenedList"); }
        }

        protected void NavigateToSession(Session session)
        {
            RequestNavigate<SessionViewModel>(new { key = session.Key });
        }
    }
}