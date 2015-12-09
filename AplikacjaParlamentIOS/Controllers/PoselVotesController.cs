using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;
using AplikacjaParlamentShared.Repositories;
using AplikacjaParlamentShared.Api;
using AplikacjaParlamentShared.Models;
using System.Collections.Generic;

namespace AplikacjaParlamentIOS
{
	public partial class PoselVotesController : BaseController
	{
		LoadingOverlay loadingOverlay;
		UITableView TableView;

		public PoselVotesController (IntPtr handle) : base (handle)
		{
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			TableView = new UITableView () {
				Frame = new CoreGraphics.CGRect (0, 0, View.Bounds.Width, View.Bounds.Height - 115)
			};
			View.AddSubviews (new UIView[] { TableView });
			EdgesForExtendedLayout = UIRectEdge.None;
			var bounds = UIScreen.MainScreen.Bounds;
			loadingOverlay = new LoadingOverlay (bounds);
			View.Add (loadingOverlay);
			GetData ();
		}

		async private void GetData()
		{

			IPeopleRepository repository = PeopleRepository.Instance;
			try {
				var list = await repository.GetPoselVotes ((ParentViewController as PoselController).PoselID);

				TableView.Source = new PoselVotesTableSource(list, this);
				TableView.ReloadData();
				TableView.RowHeight = UITableView.AutomaticDimension;
				TableView.EstimatedRowHeight = 50;
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
