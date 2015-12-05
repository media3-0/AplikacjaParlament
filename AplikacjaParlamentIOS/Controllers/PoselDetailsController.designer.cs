// WARNING
//
// This file has been generated automatically by Xamarin Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace AplikacjaParlamentIOS
{
	[Register ("PoselDetailsController")]
	partial class PoselDetailsController
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel NameLabel { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (NameLabel != null) {
				NameLabel.Dispose ();
				NameLabel = null;
			}
		}
	}
}
