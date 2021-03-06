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
	public partial class ListaPoslowController : BaseController
	{

		LoadingOverlay loadingOverlay;
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
			var bounds = UIScreen.MainScreen.Bounds;
			loadingOverlay = new LoadingOverlay (bounds);
			View.Add (loadingOverlay);
			GetPoselList ();

		}

		private async void GetPoselList()
		{
			IPeopleRepository repository = PeopleRepository.Instance;
			try {
				var list = await repository.GetPoselList();
				TableView.Source = new PoslowieTableSource(list, this);
				TableView.ReloadData();
				TableView.AllowsSelection = true;
				TableView.SectionIndexColor = UIColor.FromRGB(255, 0, 0);
				this.loadingOverlay.Hide();
			} catch (ApiRequestException ex){
				DisplayError(ex.Message);
				System.Diagnostics.Debug.WriteLine (ex.Message);
			} catch (Exception exc){
				System.Diagnostics.Debug.WriteLine (exc.Message);
			} finally {
				loadingOverlay.Hide();
			}
		}
	}
}
