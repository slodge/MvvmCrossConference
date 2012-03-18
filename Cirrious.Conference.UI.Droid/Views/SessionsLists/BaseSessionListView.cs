using System;
using Android.App;
using Android.Content;
using Android.OS;
using Cirrious.Conference.Core.Models;
using Cirrious.Conference.Core.ViewModels;
using Cirrious.Conference.Core.ViewModels.Helpers;
using Cirrious.Conference.Core.ViewModels.SessionLists;
using Cirrious.MvvmCross.Binding.Android.Views;

namespace Cirrious.Conference.UI.Droid.Views.SessionsLists
{
    public class BaseSessionListView<TViewModel, TKeyType>
        : BaseView<TViewModel>
        where TViewModel : BaseSessionListViewModel<TKeyType>
    {
        protected sealed override void OnViewModelSet()
        {
            SetContentView(Resource.Layout.Page_SessionList);

            //Find our list and set its adapter
            var sessionListView = FindViewById<MvxBindableListView>(Resource.Id.SessionList);
            sessionListView.Adapter = new SessionListAdapter(this);
        }

        public class SessionListAdapter : MvxBindableListAdapter
        {
            public SessionListAdapter(Context context) 
                : base(context)
            {
            }

            protected override global::Android.Views.View GetBindableView(global::Android.Views.View convertView, object source)
            {
                if (source is WithCommand<SessionWithFavoriteFlag>)
                    return base.GetBindableView(convertView, source, Resource.Layout.ListItem_Session);
                else if (source is DateTime)
                    return base.GetBindableView(convertView, source, Resource.Layout.ListItem_SeparatorSessionDateTime);
                else if (source is string)
                    return base.GetBindableView(convertView, source, Resource.Layout.ListItem_SeparatorToString);
                else if (source is TopicsViewModel.TopicAndLevel)
                    return base.GetBindableView(convertView, source, Resource.Layout.ListItem_SeparatorToString);
                else
                    throw new ArgumentException("Source unknown: " + source.GetType().ToString());
            }
        }
    }
}