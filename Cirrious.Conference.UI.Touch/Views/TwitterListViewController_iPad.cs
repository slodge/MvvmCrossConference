using System;
using MonoTouch.Dialog;
using MonoTouch.UIKit;
using System.Linq;

using System.Collections.Generic;
using Cirrious.Conference.Core.ViewModels.HomeViewModels;
using Cirrious.Conference.Core.ViewModels;
using Cirrious.MvvmCross.Binding.Touch.ExtensionMethods;
using Cirrious.MvvmCross.Binding.Touch.Views;
using Cirrious.MvvmCross.Views;
using MonoTouch.Foundation;

namespace Cirrious.Conference.UI.Touch.Views
{
    public class TwitterListViewController_iPad //: MvxBindingTouchViewController<TwitterViewModel_iPad>
    {
        public event EventHandler<RowClickedEventArgs> RowClicked;
        public class RowClickedEventArgs : EventArgs
        {
            public int Item { get; set; }

            public RowClickedEventArgs (int item) : base()
            {
                this.Item = item;
            }
        }

        //public TwitterListViewController_iPad () : base(null)
        //{
        //}
    }
}

