using System.Collections.Generic;
using Cirrious.Conference.Core.ViewModels.HomeViewModels;
using Cirrious.MvvmCross.Binding.Touch.ExtensionMethods;
using Cirrious.MvvmCross.Binding.Touch.Views;
using Cirrious.MvvmCross.Views;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using Cirrious.Conference.Core.ViewModels;

namespace Cirrious.Conference.UI.Touch.Views
{
    public class SplashScreenView
        : MvxBindingTouchTableViewController<SplashScreenViewModel>
	{
        public SplashScreenView(MvxShowViewModelRequest request)
            : base(request)
        {
        }
		
		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
#warning TODO!			
			ViewModel.SplashScreenComplete = true;
		}
	}
	
    public class TwitterView
        : MvxBindingTouchTableViewController<TwitterViewModel>
    {		
		private UIActivityIndicatorView _activityView;
		
        public TwitterView(MvxShowViewModelRequest request)
            : base(request)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
			
			//this.View.BackgroundColor = UIColor.Black;
			
			_activityView = new UIActivityIndicatorView(this.View.Frame);
			Add(_activityView);
			View.BringSubviewToFront(_activityView);
			
            var source = new MvxActionBasedBindableTableViewSource(
                                TableView, 
                                UITableViewCellStyle.Default,
                                TweetCell.Identifier, 
                                TweetCell.CellBindingText,
								UITableViewCellAccessory.None);
			
			source.CellModifier = (cell) =>
				{
					cell.Image.DefaultImagePath = "Images/Icons/50_icon.png";
				};
			source.CellCreator = (tableView, indexPath, item) => 
			    {
					return TweetCell3.LoadFromNib();
				};
            this.AddBindings(new Dictionary<object, string>()
		                         {
		                             {source, "{'ItemsSource':{'Path':'Tweets'}}"},
									 {_activityView, "{'Hidden':{'Path':'IsSearching','Converter':'InvertedVisibility'}}"},
									 //{TableView, "{'Hidden':{'Path':'IsSearching','Converter':'Visibility'}}"},
		                         });
			TableView.RowHeight = 100;
            TableView.Source = source;
			TableView.ReloadData();
        }
    }
}