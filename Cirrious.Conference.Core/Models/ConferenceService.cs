using System;
using System.ComponentModel;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net;
using Cirrious.Conference.Core.Interfaces;
using Cirrious.Conference.Core.Models.Helpers;
using Cirrious.Conference.Core.Models.Raw;
using Cirrious.MvvmCross.Core;
using Cirrious.MvvmCross.ExtensionMethods;
using Cirrious.MvvmCross.Interfaces.Localization;
using Cirrious.MvvmCross.Interfaces.Platform;
using Cirrious.MvvmCross.Interfaces.ServiceProvider;
using Cirrious.MvvmCross.Platform;
using Newtonsoft.Json;

namespace Cirrious.Conference.Core.Models
{
    public class ConferenceService 
        : IConferenceService
        , IMvxServiceConsumer<IMvxResourceLoader>
        , IMvxServiceConsumer<IMvxSimpleFileStoreService>
    {
        private readonly FavoritesSaver _favoritesSaver = new FavoritesSaver();

        // is loading setup
        private bool _isLoading;
        public bool IsLoading
        {
            get { return _isLoading; }
            private set 
            { 
                _isLoading = value;
                FireLoadingChanged();
            }
        }

        private void FireLoadingChanged()
        {
            var handler = LoadingChanged;
            if (handler != null)
                handler(this, EventArgs.Empty);
        }

        public event EventHandler LoadingChanged;

        // the basic lists
        public IDictionary<string, SessionWithFavoriteFlag> Sessions { get; private set; }
        public IDictionary<string, Sponsor> Exhibitors { get; private set; }
        public IDictionary<string, Sponsor> Sponsors { get; private set; }

        // a hashtable of favorites
        private IDictionary<string, SessionWithFavoriteFlag> _favoriteSessions;
        public IDictionary<string,SessionWithFavoriteFlag> GetCopyOfFavoriteSessions()
        {
            lock (this)
            {
				if (_favoriteSessions == null)
					return new Dictionary<string, SessionWithFavoriteFlag>();
				
                var toReturn = new Dictionary<string, SessionWithFavoriteFlag>(_favoriteSessions);
                return toReturn;
            }
        }

        public event EventHandler FavoritesSessionsChanged;

        private void FireFavoriteSessionsChanged()
        {
            var handler = FavoritesSessionsChanged;
            if (handler != null)
                handler(this, EventArgs.Empty);
        }

        public void BeginAsyncLoad()
        {
            IsLoading = true;
            MvxAsyncDispatcher.BeginAsync(Load);
        }
		
		public void DoSyncLoad()
        {
            IsLoading = true;
            Load();
        }

        private void Load()
        {
            LoadSessions();
            LoadFavorites();
            LoadSponsors();

            IsLoading = false;
        }		
		
        private void LoadSponsors()
        {
            var file = this.GetService<IMvxResourceLoader>().GetTextResource("ConfResources/Sponsors.txt");
            var items = JsonConvert.DeserializeObject<List<Sponsor>>(file);
            Sponsors = items.Where(x => x.Level != "Exhibitor").ToDictionary(x => x.Name);
            Exhibitors = items.Where(x => x.Level == "Exhibitor").ToDictionary(x => x.Name);
        }

        private void LoadFavorites()
        {
            lock (this)
            {
                _favoriteSessions = new Dictionary<string, SessionWithFavoriteFlag>();
            }
            FireFavoriteSessionsChanged();

            var files = this.GetService<IMvxSimpleFileStoreService>();
            string json;
            if (!files.TryReadTextFile(Constants.FavoritesFileName, out json))
                return;

            var parsedKeys = JsonConvert.DeserializeObject<List<string>>(json);
            if (parsedKeys != null)
            {
                foreach (var key in parsedKeys)
                {
                    SessionWithFavoriteFlag session;
                    if (Sessions.TryGetValue(key, out session))
                        session.IsFavorite = true;
                }
            }
        }

        private void LoadSessions()
        {
            var file = this.GetService<IMvxResourceLoader>().GetTextResource("ConfResources/Sessions.txt");
            var conferenceModel = JsonConvert.DeserializeObject<PocketConferenceModel>(file);           
            // patch up the sessions with slots
            foreach (var session in conferenceModel.Sessions.Values)
            {
                Slot slot;
                if (conferenceModel.Slots.TryGetValue(session.SlotId, out slot))
                {
                    session.Slot = slot;
                }
            }

            Sessions = conferenceModel.Sessions.Select(x => new SessionWithFavoriteFlag()
                                                  {
                                                      Session = x.Value,
                                                      IsFavorite = false
                                                  })
                .ToDictionary(x => x.Session.Id, x => x);

            foreach (var sessionWithFavoriteFlag in Sessions.Values)
            {
                sessionWithFavoriteFlag.PropertyChanged += SessionWithFavoriteFlagOnPropertyChanged;            
            }
        }

        private void SessionWithFavoriteFlagOnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            if (propertyChangedEventArgs.PropertyName != "IsFavorite")
                return;

            var session = (SessionWithFavoriteFlag)sender;
            lock (this)
            {
                if (_favoriteSessions == null)
                    return;

                if (session.IsFavorite)
                {
                    _favoriteSessions[session.Session.Id] = session;
                }
                else
                {
                    if (_favoriteSessions.ContainsKey(session.Session.Id))
                        _favoriteSessions.Remove(session.Session.Id);
                }

                _favoritesSaver.RequestAsyncSave(_favoriteSessions.Keys.ToList());
            }

            FireFavoriteSessionsChanged();
        }
    }
}
