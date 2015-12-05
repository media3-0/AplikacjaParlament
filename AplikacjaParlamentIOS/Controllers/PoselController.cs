using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace AplikacjaParlamentIOS
{
	partial class PoselController : UITabBarController
	{

		public int PoselID { get; set; }

		public PoselController (IntPtr handle) : base (handle)
		{
		}
	}
}
