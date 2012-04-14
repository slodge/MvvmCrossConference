// WARNING
//
// This file has been generated automatically by MonoDevelop to store outlets and
// actions made in the Xcode designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using MonoTouch.Foundation;

namespace Cirrious.Conference.UI.Touch
{
 [Register ("UpdateView")]
 partial class UpdateView
 {
    [Outlet]
    MonoTouch.UIKit.UIActivityIndicatorView loadingActivityInidicatorView { get; set; }

    [Outlet]
    MonoTouch.UIKit.UILabel loadingLabel { get; set; }

    [Outlet]
    MonoTouch.UIKit.UIButton loadingSkipButton { get; set; }
    
    void ReleaseDesignerOutlets ()
    {
       if (loadingActivityInidicatorView != null) {
          loadingActivityInidicatorView.Dispose ();
          loadingActivityInidicatorView = null;
       }

       if (loadingLabel != null) {
          loadingLabel.Dispose ();
          loadingLabel = null;
       }

       if (loadingSkipButton != null) {
          loadingSkipButton.Dispose ();
          loadingSkipButton = null;
       }
    }
 }
}
