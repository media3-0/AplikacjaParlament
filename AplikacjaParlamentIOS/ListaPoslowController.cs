using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;
using System.Drawing;
using AplikacjaParlamentShared.Repositories;
using System.Collections.Generic;
using AplikacjaParlamentShared.Models;
using AplikacjaParlamentShared.Api;

namespace AplikacjaParlamentIOS
{
	partial class ListaPoslowController : UIViewController
	{

		UIBarButtonItem loadingBtn;
		private List<Posel> list;

		public ListaPoslowController (IntPtr handle) : base (handle)
		{
		}

		public override void DidReceiveMemoryWarning ()
		{
			base.DidReceiveMemoryWarning ();
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			EdgesForExtendedLayout = UIRectEdge.None;
			UIActivityIndicatorView spinner = new UIActivityIndicatorView (new RectangleF (0, 0, 22, 22));
			spinner.StartAnimating ();
			loadingBtn = new UIBarButtonItem (spinner);
			this.NavigationItem.LeftBarButtonItem = loadingBtn;
			GetPoselList ();

		}

		private async void GetPoselList()
		{
			IPeopleRepository repository = PeopleRepository.Instance;
			try {
				list = await repository.GetPoselList();
				TableView.Source = new PoslowieTableSource(list);
				TableView.ReloadData();
				this.NavigationItem.LeftBarButtonItem = null;
			} catch (ApiRequestException ex){
				System.Diagnostics.Debug.WriteLine (ex.Message);
			}
		}
	}
}
