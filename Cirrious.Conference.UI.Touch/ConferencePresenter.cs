using Cirrious.Conference.UI.Touch.Views;
using Cirrious.MvvmCross.Touch.Views.Presenters;
using MonoTouch.UIKit;

namespace Cirrious.Conference.UI.Touch
{
    public class ConferencePresenter 
        : MvxModalSupportTouchViewPresenter
        , ITabBarPresenterHost
	{
        public ConferencePresenter(UIApplicationDelegate applicationDelegate, UIWindow window)
			: base(applicationDelegate, window)
		{
		}
		
		protected override UINavigationController CreateNavigationController (UIViewController viewController)
		{
			var toReturn = base.CreateNavigationController (viewController);
			toReturn.NavigationBarHidden = false;
			return toReturn;
		}

        public ITabBarPresenter TabBarPresenter{ get; set; }

        public override bool ShowView(MvvmCross.Touch.Interfaces.IMvxTouchView view)
        {
            if (TabBarPresenter != null)
                return TabBarPresenter.ShowView(view);

            return base.ShowView(view);
        }
	}
}

