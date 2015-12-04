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
	public partial class ListaPoslowController : UIViewController
	{

		UIBarButtonItem loadingBtn;
		List<Posel> list;
		UITableView TableView;
		UISearchBar SearchBar;

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
			SearchBar = new UISearchBar () {
				Frame = new CoreGraphics.CGRect (0, 0, View.Bounds.Width, 44)
			};
			TableView = new UITableView () {
				Frame = new CoreGraphics.CGRect (0, 44, View.Bounds.Width, View.Bounds.Height - 108)
			};
			View.AddSubviews (new UIView[] { SearchBar, TableView });
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
				TableView.Source = new PoslowieTableSource(list, this);
				TableView.ReloadData();
				TableView.AllowsSelection = true;
				this.NavigationItem.LeftBarButtonItem = null;
			} catch (ApiRequestException ex){
				System.Diagnostics.Debug.WriteLine (ex.Message);
			}
		}
	}
}
