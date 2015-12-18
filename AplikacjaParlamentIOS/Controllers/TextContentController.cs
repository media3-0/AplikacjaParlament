using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;
using AplikacjaParlamentShared.Repositories;
using AplikacjaParlamentShared.Api;
using System.Linq;
using System.Drawing;
using CoreGraphics;

namespace AplikacjaParlamentIOS
{
	public partial class TextContentController : BaseController
	{
		public string TextToView { get; set; }
		private UILabel TextContentLabel;

		public TextContentController (IntPtr handle) : base (handle)
		{
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			EdgesForExtendedLayout = UIRectEdge.None;

			UIScrollView scrollView = new UIScrollView();
			scrollView.TranslatesAutoresizingMaskIntoConstraints = false;
			View.AddSubview(scrollView);

			TextContentLabel = new UILabel();
			TextContentLabel.Lines = 0;
			TextContentLabel.TranslatesAutoresizingMaskIntoConstraints = false;
			scrollView.AddSubview(TextContentLabel);

			var attr = new NSAttributedStringDocumentAttributes();
			var nsError = new NSError();
			attr.DocumentType = NSDocumentType.HTML;

			TextContentLabel.AttributedText = new NSAttributedString(TextToView, attr, ref nsError);


			var viewsDictionary = NSDictionary.FromObjectsAndKeys(new NSObject[] { scrollView, TextContentLabel}, new NSObject[] { new NSString("scrollView"), new NSString("TextContentLabel")});
			scrollView.AddConstraints(NSLayoutConstraint.FromVisualFormat("H:|[TextContentLabel(scrollView)]|", 0, null, viewsDictionary));
			scrollView.AddConstraints(NSLayoutConstraint.FromVisualFormat("V:|[TextContentLabel]|", 0, null, viewsDictionary));
			View.AddConstraints(NSLayoutConstraint.FromVisualFormat("H:|-[scrollView]-|", 0, null, viewsDictionary));
			View.AddConstraints(NSLayoutConstraint.FromVisualFormat("V:|-[scrollView]-|", 0, null, viewsDictionary));
		}
	}
}
