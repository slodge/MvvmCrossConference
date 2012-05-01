using System;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using Cirrious.Conference.Core.Models.Twitter;
using System.Drawing;

namespace Cirrious.Conference.UI.Touch.Views
{
    public class TwitterDetailsViewController_iPad : UIViewController
    {
        public UIPopoverController PopOver { get; set; }
        private Tweet tweet;
        private UILabel user;
        private UIToolbar toolBar;

        public TwitterDetailsViewController_iPad()
        {
            View.BackgroundColor = UIColor.White;
            user = new UILabel(new RectangleF(100,100,300,50));
            user.Text = "This is the Details Views";
            View.AddSubview(user);
            Update (1);

            toolBar = new UIToolbar(new RectangleF(0,0, View.Frame.Width, 30));
            toolBar.AutoresizingMask = UIViewAutoresizing.FlexibleWidth;
            View.AddSubview(toolBar);
        }

        public TwitterDetailsViewController_iPad(Tweet tweetToShow)
        {
            this.tweet = tweetToShow;
            this.DisplayTweet();
        }

        public void Update(int item)
        {
            this.user.Text = string.Format("item {0}", item);
        }

        public void AddToolBarButton(UIBarButtonItem button)
        {
            button.Title = "Contents";
            this.toolBar.SetItems( new UIBarButtonItem[] { button }, false );
        }

        public void RemoveToolBarButton()
        {
            this.toolBar.SetItems( new UIBarButtonItem[0], false );
        }

        private void DisplayTweet()
        {
            user = new UILabel(){
                TextAlignment = UITextAlignment.Left,
            };

            user.Text = this.tweet.Author;

            this.View.AddSubview(user);
        }

        public override bool ShouldAutorotateToInterfaceOrientation (UIInterfaceOrientation toInterfaceOrientation)
        {
            return true;
        }
    }
}

