using System;
using System.Collections;
using System.Collections.Generic;
using Cirrious.Conference.Core.Models;
using Cirrious.Conference.Core.ViewModels.Helpers;
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
        private UIActivityIndicatorView _activityView;

        public BaseSessionListView(MvxShowViewModelRequest request)
            : base(request)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            NavigationItem.SetRightBarButtonItem(new UIBarButtonItem("Tweet", UIBarButtonItemStyle.Bordered, (sender, e) => ViewModel.ShareGeneralCommand.Execute()), false);

            var source = new TableSource(TableView);
            this.AddBindings(new Dictionary<object, string>()
		                         {
		                             {source, "{'ItemsSource':{'Path':'GroupedList'}}"},
		                         });

            TableView.BackgroundColor = UIColor.Black;
            TableView.RowHeight = 126;
            TableView.Source = source;
            TableView.ReloadData();
        }

        private class TableSource : MvxBaseBindableTableViewSource
        {
            public TableSource(UITableView tableView)
                : base(tableView)
            {
            }

            private IList<BaseSessionListViewModel<TKey>.SessionGroup> _sessionGroups;
            public IList<BaseSessionListViewModel<TKey>.SessionGroup> ItemsSource
            {
                get
                {
                    return _sessionGroups;
                }
                set 
                { 
                    _sessionGroups = value;
                    ReloadTableData();
                }
            }
			
			public override string TitleForHeader(UITableView tableView, int section)
			{
		       if (_sessionGroups == null)
                    return string.Empty;

                return _sessionGroups[section].Key.ToString();
         	}

            public override float GetHeightForRow(UITableView tableView, NSIndexPath indexPath)
            {
                return 126;
            }

            public override int NumberOfSections(UITableView tableView)
            {
                if (_sessionGroups == null)
                    return 0;

                return _sessionGroups.Count;
            }

            public override int RowsInSection(UITableView tableview, int section)
            {
                if (_sessionGroups == null)
                    return 0;

                return _sessionGroups[section].Count;
            }

            protected override UITableViewCell GetOrCreateCellFor(UITableView tableView, NSIndexPath indexPath, object item)
            {
                var reuse = tableView.DequeueReusableCell(SessionCell2.Identifier);
                if (reuse != null)
                    return reuse;

                var cell = SessionCell2.LoadFromNib();
                return cell;
            }

            protected override object GetItemAt(NSIndexPath indexPath)
            {
                if (_sessionGroups == null)
                    return null;

                return _sessionGroups[indexPath.Section][indexPath.Row];
            }
        }
    }
}