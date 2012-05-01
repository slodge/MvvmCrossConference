using System.Collections.Generic;
using Cirrious.Conference.Core.ViewModels.HomeViewModels;
using Cirrious.Conference.Core.ViewModels;
using Cirrious.MvvmCross.Binding.Touch.ExtensionMethods;
using Cirrious.MvvmCross.Binding.Touch.Views;
using Cirrious.MvvmCross.Interfaces.ServiceProvider;
using Cirrious.MvvmCross.Interfaces.Platform;
using Cirrious.MvvmCross.Views;
using Cirrious.Conference.Core.Interfaces;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using Cirrious.Conference.Core.Models.Twitter;
using MonoTouch.Dialog;
using System.Linq;
using System.Drawing;

namespace Cirrious.Conference.UI.Touch.Views
{
    public class TwitterSplitViewController_iPad : UISplitViewController
    {
        private TwitterListViewController_iPad twitterListViewController;
        private TwitterDetailsViewController_iPad twitterDetailsViewController;

        public TwitterSplitViewController_iPad () : base()
        {
            View.Bounds = new System.Drawing.RectangleF (0, 0, UIScreen.MainScreen.Bounds.Width, UIScreen.MainScreen.Bounds.Height);
            twitterListViewController = new TwitterListViewController_iPad ();
            twitterDetailsViewController = new TwitterDetailsViewController_iPad ();
            Delegate = new SplitViewDelegate (twitterDetailsViewController);
            //this.ViewControllers = new UIViewController[]{ twitterListViewController , twitterDetailsViewController };
            twitterListViewController.RowClicked += (sender, e) => {
                twitterDetailsViewController.Update (e.Item); };
        }

        public override void ViewDidLoad ()
        {
            base.ViewDidLoad ();
            this.Title = "Twitter";
        }

        public override bool ShouldAutorotateToInterfaceOrientation (UIInterfaceOrientation toInterfaceOrientation)
        {
            return true;
        }


        public class SplitViewDelegate : UISplitViewControllerDelegate
        {
            private TwitterDetailsViewController_iPad TwitterDetailsViewController { get; set; }

            public SplitViewDelegate (TwitterDetailsViewController_iPad twitterDetailsViewController)
            {
                this.TwitterDetailsViewController = twitterDetailsViewController;
            }

            public override bool ShouldHideViewController (UISplitViewController svc, UIViewController viewController, UIInterfaceOrientation inOrientation)
            {
                return false; //inOrientation == UIInterfaceOrientation.Portrait || inOrientation == UIInterfaceOrientation.PortraitUpsideDown;
            }

            public override void WillHideViewController (UISplitViewController svc, UIViewController aViewController, UIBarButtonItem barButtonItem, UIPopoverController pc)
            {
                this.TwitterDetailsViewController.PopOver = pc;
                this.TwitterDetailsViewController.AddToolBarButton (barButtonItem);
            }

            public override void WillPresentViewController (UISplitViewController svc, UIPopoverController pc, UIViewController aViewController)
            {
            }

            public override void WillShowViewController (UISplitViewController svc, UIViewController aViewController, UIBarButtonItem button)
            {
                this.TwitterDetailsViewController.PopOver = null;
                this.TwitterDetailsViewController.RemoveToolBarButton ();
            }
        }
    }
}

