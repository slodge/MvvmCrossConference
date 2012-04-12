// WARNING
//
// This file has been generated automatically by MonoDevelop to store outlets and
// actions made in the Xcode designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using MonoTouch.Foundation;

namespace Cirrious.Conference.UI.Touch
{
 [Register ("TeamCell")]
 partial class TeamCell
 {
    [Outlet]
    MonoTouch.UIKit.UIImageView TheImage { get; set; }

    [Outlet]
    MonoTouch.UIKit.UILabel FullNameLabel { get; set; }

    [Outlet]
    MonoTouch.UIKit.UILabel DescriptionLabel { get; set; }
    
    void ReleaseDesignerOutlets ()
    {
       if (TheImage != null) {
          TheImage.Dispose ();
          TheImage = null;
       }

       if (FullNameLabel != null) {
          FullNameLabel.Dispose ();
          FullNameLabel = null;
       }

       if (DescriptionLabel != null) {
          DescriptionLabel.Dispose ();
          DescriptionLabel = null;
       }
    }
 }
}
