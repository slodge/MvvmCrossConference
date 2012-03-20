using System;
using System.Collections.Generic;
using System.Drawing;
using Cirrious.MvvmCross.Binding.Touch.ExtensionMethods;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using Cirrious.Conference.Core.ViewModels;
using Cirrious.MvvmCross.Binding.Touch.Views;
using Cirrious.MvvmCross.Views;

namespace Cirrious.Conference.UI.Touch
{
	public partial class MapView : MvxBindingTouchViewController<MapViewModel>
	{
		public MapView (MvxShowViewModelRequest request) : base (request, "MapView", null)
		{
		}
		
		public override void DidReceiveMemoryWarning ()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning ();
			
			// Release any cached data, images, etc that aren't in use.
		}
		
		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

            Button1.SetImage(UIImage.FromFile("ConfResources/Images/appbar.link.png"), UIControlState.Normal);
            Button2.SetImage(UIImage.FromFile("ConfResources/Images/appbar.phone.png"), UIControlState.Normal);
            Button3.SetImage(UIImage.FromFile("ConfResources/Images/appbar.feature.email.rest.png"), UIControlState.Normal);

            this.AddBindings(new Dictionary<object, string>()
		                         {
                                     {Label1,"{'Text':{'Path':'Name'}}"}, 
                                     {Button1,"{'Title':{'Path':'Address'}}"},
                                     {Button2,"{'Title':{'Path':'Phone'}}"},
                                     {Button3,"{'Title':{'Path':'Email'}}"},
		                         });

            this.AddBindings(new Dictionary<object, string>()
		                         {
                                     {Button1,"{'TouchDown':{'Path':'WebPageCommand'}}"},
                                     {Button2,"{'TouchDown':{'Path':'PhoneCommand'}}"},
                                     {Button3,"{'TouchDown':{'Path':'EmailCommand'}}"},
		                         });

#warning TODO - map setup!
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

