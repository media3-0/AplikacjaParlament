using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace AplikacjaParlamentIOS
{
	partial class PoselDetailsController : UIViewController
	{

		public PoselDetailsController (IntPtr handle) : base (handle)
		{
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			NameLabel.Text = (ParentViewController as PoselController).PoselID.ToString ();
		}
	}
}
