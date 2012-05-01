using System;
using System.Collections.Generic;
using System.Drawing;
using Cirrious.MvvmCross.Binding.Touch.ExtensionMethods;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using Cirrious.Conference.Core.ViewModels;
using Cirrious.MvvmCross.Binding.Touch.Views;
using Cirrious.MvvmCross.Views;
using System.Threading;

namespace Cirrious.Conference.UI.Touch
{
   public partial class UpdateView : MvxBindingTouchViewController<UpdateViewModel>
   {
       public UpdateView(MvxShowViewModelRequest request)
            : base(request, AppDelegate.IsPad ? "UpdateView_iPad" : "UpdateView", null)
        {
        }
      
      public override void ViewDidLoad ()
      {
         base.ViewDidLoad ();

         this.loadingActivityInidicatorView.StartAnimating();

         this.AddBindings(new Dictionary<object, string>()
                              {
                                  {loadingLabel, "{'Text':{'Path':'TextSource','Converter':'Language','ConverterParameter':'CheckingForUpdates'}}"},
                                  {loadingSkipButton, "{'Title':{'Path':'TextSource','Converter':'Language','ConverterParameter':'Skip'}}"}
                              });

         this.AddBindings(new Dictionary<object, string>()
                             {
                               { loadingSkipButton, "{'TouchDown':{'Path':'SkipCommand'}}" }
                             });
      }



      public override void ViewDidUnload ()
      {
         base.ViewDidUnload ();
         
         // Clear any references to subviews of the main view in order to
         // allow the Garbage Collector to collect them sooner.
         //
         // e.g. myOutlet.Dispose (); myOutlet = null;
         
         ReleaseDesignerOutlets ();
      }
      
      public override bool ShouldAutorotateToInterfaceOrientation (UIInterfaceOrientation toInterfaceOrientation)
      {
         // Return true for supported orientations
         return (toInterfaceOrientation != UIInterfaceOrientation.PortraitUpsideDown);
      }
   }
}

