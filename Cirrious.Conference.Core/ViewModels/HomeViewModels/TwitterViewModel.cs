using System;
using System.Collections.Generic;
using System.Linq;
using Cirrious.Conference.Core.Interfaces;
using Cirrious.Conference.Core.Models.Twitter;
using Cirrious.MvvmCross.Commands;
using Cirrious.MvvmCross.ExtensionMethods;
using Cirrious.MvvmCross.Interfaces.Commands;
using Cirrious.MvvmCross.Interfaces.ServiceProvider;

namespace Cirrious.Conference.Core.ViewModels.HomeViewModels
{
    public class TwitterViewModel
        : BaseViewModel
        , IMvxServiceConsumer<ITwitterSearchProvider>
    {
        private const string SearchTerm = "SQLBits";

        private ITwitterSearchProvider TwitterSearchProvider
        {
            get { return this.GetService<ITwitterSearchProvider>(); }
        }

        private IEnumerable<Tweet> _tweets;
        public IEnumerable<Tweet> Tweets
        {
            get { return _tweets; }
            set { _tweets = value; FirePropertyChanged("Tweets"); }
        }

        private bool _isSearching;
        public bool IsSearching
        {
            get { return _isSearching; }
            set { _isSearching = value; FirePropertyChanged("IsSearching"); }
        }

        public IMvxCommand SearchCommand
        {
            get
            {
                return new MvxRelayCommand(StartSearch);
            }
        }

        private void StartSearch()
        {
            if (IsSearching)
                return;

            IsSearching = true;
            TwitterSearchProvider.StartAsyncSearch(SearchTerm, Success, Error);
        }

        public TwitterViewModel()
        {
            StartSearch();
        }

        private void Error(Exception exception)
        {
            // TODO...
            IsSearching = false;
        }

        private void Success(IEnumerable<Tweet> enumerable)
        {
            InvokeOnMainThread(() => DisplayTweets(enumerable));
        }

        private void DisplayTweets(IEnumerable<Tweet> enumerable)
        {
            IsSearching = false;
            Tweets = enumerable.ToList();
        }
    }
}