using System.Collections.Generic;
using Cirrious.Conference.Core.ViewModels.SessionLists;
using Cirrious.MvvmCross.Binding.Touch.ExtensionMethods;
using Cirrious.MvvmCross.Binding.Touch.Views;
using Cirrious.MvvmCross.Views;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace Cirrious.Conference.UI.Touch.Views.SessionLists
{
    public class BaseSessionListView<TViewModel, TKey>
        : MvxBindingTouchTableViewController<TViewModel>
        where TViewModel : BaseSessionListViewModel<TKey>
    {
        const string CellBindingText = "{'TitleText':{'Path':'Item.Session.SpeakerKey'},'DetailText':{'Path':'Item.Session.Title'},'RoomText':{'Path':'Item.Session','Converter':'SessionSmallDetails'}}";
		
		private UIActivityIndicatorView _activityView;
		
        public BaseSessionListView(MvxShowViewModelRequest request)
            : base(request)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
			
			_activityView = new UIActivityIndicatorView(this.View.Frame);
			Add(_activityView);
			View.BringSubviewToFront(_activityView);
			
            var source = new MvxActionBasedBindableTableViewSource(
                                TableView, 
                                UITableViewCellStyle.Subtitle,
                                new NSString("FlattenedList"), 
                                CellBindingText,
								UITableViewCellAccessory.None);
			
            this.AddBindings(new Dictionary<object, string>()
		                         {
		                             {source, "{'ItemsSource':{'Path':'FlattenedList'}}"},
									 {_activityView, "{'Hidden':{'Path':'IsSearching','Converter':'InvertedVisibility'}}"},
		                         });
            TableView.Source = source;
			TableView.ReloadData();
        }
    }
}