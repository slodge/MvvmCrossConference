using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    {
        public void Start()
        {
			this.GetService<IConferenceService>().BeginAsyncLoad();
            RequestNavigate<SplashScreenViewModel>();
        }

        public bool ApplicationCanOpenBookmarks
        {
            get { return true; }
        }
    }
}
