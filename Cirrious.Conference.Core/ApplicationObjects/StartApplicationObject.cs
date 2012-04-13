using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Cirrious.Conference.Core.Models;
using Cirrious.Conference.Core.ViewModels;
using Cirrious.MvvmCross.Interfaces.ViewModels;
using Cirrious.MvvmCross.ViewModels;
using Cirrious.MvvmCross.Interfaces.ServiceProvider;
using Cirrious.Conference.Core.Interfaces;
using Cirrious.MvvmCross.ExtensionMethods;

namespace Cirrious.Conference.Core.ApplicationObjects
{
    public class StartApplicationObject
        : MvxApplicationObject
        , IMvxStartNavigation
        , IMvxServiceConsumer<IConferenceService>
        , IMvxServiceConsumer<IApplicationSettings>
        , IConferenceStart
    {
        private readonly bool _showSplashScreen;
        public StartApplicationObject(bool showSplashScreen)
        {
            _showSplashScreen = showSplashScreen;
        }

        public void Start()
        {
            if (DataNeedsUpdating())
            {
                RequestNavigate<UpdateViewModel>();
            }
            else
            {
                StartApp();
            }
        }

        public void StartApp()
        {
            var confService = this.GetService<IConferenceService>();
            if (_showSplashScreen)
            {
                confService.BeginAsyncLoad();
                RequestNavigate<SplashScreenViewModel>(true);
            }
            else
            {
                confService.DoSyncLoad();
                RequestNavigate<HomeViewModel>(true);
            }
        }

        private bool DataNeedsUpdating()
        {
            var whenDataUpdateUtc = this.GetService<IApplicationSettings>().DataLastUpdatedUtc;
            if (DateTime.UtcNow - whenDataUpdateUtc > Constants.MaxTimeBetweenUpdates)
            {
                return true;
            }

            return false;
        }

        public bool ApplicationCanOpenBookmarks
        {
            get { return true; }
        }
    }
}
