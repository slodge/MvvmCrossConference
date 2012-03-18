using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cirrious.Conference.Core.ViewModels;
using Cirrious.MvvmCross.Interfaces.ViewModels;
using Cirrious.MvvmCross.ViewModels;

namespace Cirrious.Conference.Core.ApplicationObjects
{
    public class StartApplicationObject
        : MvxApplicationObject
        , IMvxStartNavigation
    {
        public void Start()
        {
            RequestNavigate<SplashScreenViewModel>();
        }

        public bool ApplicationCanOpenBookmarks
        {
            get { return true; }
        }
    }
}
