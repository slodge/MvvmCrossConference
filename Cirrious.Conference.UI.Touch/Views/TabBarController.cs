using System;
using System.Collections.Generic;
using Cirrious.Conference.Core.ViewModels;
using Cirrious.Conference.Core.ViewModels.HomeViewModels;
using Cirrious.Conference.Core.ViewModels.SessionLists;
using Cirrious.MvvmCross.Interfaces.ViewModels;
using Cirrious.MvvmCross.Touch.ExtensionMethods;
using Cirrious.MvvmCross.Touch.Interfaces;
using MonoTouch.UIKit;
using Cirrious.MvvmCross.Touch.Views;
using Cirrious.MvvmCross.Views;
using Cirrious.MvvmCross.ExtensionMethods;
using Cirrious.MvvmCross.Interfaces.ServiceProvider;

namespace Cirrious.Conference.UI.Touch.Views 
{
	public class TabBarController
        : MvxTouchTabBarViewController<HomeViewModel>
        , ITabBarPresenter
		, IMvxServiceConsumer<ITabBarPresenterHost>
	{
        private readonly Dictionary<Type, UINavigationController> _defaultNavigationControllers = new Dictionary<Type, UINavigationController>();

		public TabBarController (MvxShowViewModelRequest request)
			: base(request)
		{
			this.GetService<ITabBarPresenterHost>().TabBarPresenter = this;
		}

	    private int _createdSoFarCount = 0;

        private UIViewController CreateTabFor<TPrimaryType>(string title, string imageName, object creationParameters = null, params Type[] alsoSupports)
            where TPrimaryType : class, IMvxViewModel
        {
            var controller = new UINavigationController();

            if (!_defaultNavigationControllers.ContainsKey(typeof(TPrimaryType)))
                _defaultNavigationControllers[typeof(TPrimaryType)] = controller;

            var screen = this.CreateViewControllerFor<TPrimaryType>(creationParameters) as UIViewController;
            SetTitleAndTabBarItem(screen, title, imageName);
            controller.PushViewController(screen, false);
            return controller;
        }

	    private void SetTitleAndTabBarItem(UIViewController screen, string title, string imageName)
	    {
	        screen.Title = ViewModel.TextSource.GetText(title);
	        screen.TabBarItem = new UITabBarItem(title, UIImage.FromBundle("Images/Tabs/" + imageName + ".png"),
	                                             _createdSoFarCount);
            _createdSoFarCount++;
        }

	    private UIViewController CreateSplittableTabFor<TPrimaryType>(string title, string imageName, object creationParameters = null, params Type[] alsoSupports)
            where TPrimaryType : class, IMvxViewModel
        {
            if (AppDelegate.IsPhone)
                return CreateTabFor<TPrimaryType>(title, imageName, creationParameters, alsoSupports);

            throw new NotImplementedException();
            /*
            var screen = this.CreateViewControllerFor<TPrimaryType>(creationParameters) as UIViewController;
            GeneralSplitView splitView;
            if (typeof(TPrimaryType) == typeof(SessionListViewModel))
                splitView = new SessionSplitView(screen, alsoSupports);
            else
                splitView = new GeneralSplitView(screen, null, alsoSupports);

	        SetTitleAndTabBarItem(splitView, title, imageName);

            if (!_defaultSplitViews.ContainsKey(typeof(TPrimaryType)))
                _defaultSplitViews[typeof(TPrimaryType)] = splitView;

            return splitView;
            */
        }

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

            ViewControllers = new UIViewController[]
                                  {
                                    CreateTabFor<WelcomeViewModel>("Welcome", "home"),
                                    CreateTabFor<SessionsViewModel>("Sessions", "sessions"),
                                    CreateTabFor<FavoritesViewModel>("Favorites", "favorites"),
                                    CreateTabFor<TwitterViewModel>("Twitter", "twitter"),
                                  };			

            CustomizableViewControllers = new UIViewController[] { };
            SelectedViewController = ViewControllers[0];
		}

		/// <summary>
		/// Only allow iPad application to rotate, iPhone is always portrait
		/// </summary>
		public override bool ShouldAutorotateToInterfaceOrientation (UIInterfaceOrientation toInterfaceOrientation)
        {
			if (AppDelegate.IsPad)
	            return true;
			else
				return toInterfaceOrientation == UIInterfaceOrientation.Portrait;
        }

	    public bool GoBack()
	    {
	        var subNavigation = this.SelectedViewController as UINavigationController;
            if (subNavigation == null)
	            return false;

            if (subNavigation.ViewControllers.Length <= 1)
                return false;

	        subNavigation.PopViewControllerAnimated(true);
	        return true;
	    }

	    public bool ShowView(IMvxTouchView view)
	    {
	        if (TryShowViewInCurrentTab(view)) 
                return true;

	        return false;
        }

	    private bool TryShowViewInCurrentTab(IMvxTouchView view)
	    {
            var navigationController = (UINavigationController)this.SelectedViewController;
            navigationController.PushViewController((UIViewController)view, true);
            return true;
	    }
	}

    public interface ITabBarPresenterHost
    {
        ITabBarPresenter TabBarPresenter { get; set; }
    }

    public interface ITabBarPresenter
    {
        bool ShowView(IMvxTouchView view);
    }
}