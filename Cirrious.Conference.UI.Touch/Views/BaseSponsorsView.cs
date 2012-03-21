using System.Collections.Generic;
using Cirrious.Conference.Core.ViewModels;
using Cirrious.Conference.Core.ViewModels.HomeViewModels;
using Cirrious.MvvmCross.Binding.Touch.ExtensionMethods;
using Cirrious.MvvmCross.Binding.Touch.Views;
using Cirrious.MvvmCross.Views;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace Cirrious.Conference.UI.Touch.Views
{
    public class BaseSponsorsView
        : MvxBindingTouchTableViewController<BaseSponsorsViewModel>
    {		
		private UIActivityIndicatorView _activityView;

        public BaseSponsorsView(MvxShowViewModelRequest request)
            : base(request)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
			
			//this.View.BackgroundColor = UIColor.Black;
			
			//_activityView = new UIActivityIndicatorView(this.View.Frame);
			//Add(_activityView);
			//View.BringSubviewToFront(_activityView);
			
            var source = new MvxActionBasedBindableTableViewSource(
                                TableView, 
                                UITableViewCellStyle.Default,
                                SponsorCell.Identifier,
                                SponsorCell.BindingText,
								UITableViewCellAccessory.None);
			
			source.CellModifier = (cell) =>
				{
					cell.Image.DefaultImagePath = "Images/Icons/50_icon.png";
				};
			source.CellCreator = (tableView, indexPath, item) => 
			    {
					return SponsorCell.LoadFromNib();
				};
            this.AddBindings(new Dictionary<object, string>()
		                         {
		                             {source, "{'ItemsSource':{'Path':'Sponsors'}}"},
		                         });
			TableView.RowHeight = 90;
            TableView.Source = source;
			TableView.ReloadData();
        }
    }
}