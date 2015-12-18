using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;
using AplikacjaParlamentShared.Repositories;
using AplikacjaParlamentShared.Api;
using AplikacjaParlamentShared.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AplikacjaParlamentIOS
{
	public partial class PoselInterpelationsController : BaseController, TableHandler
	{
		public LoadingOverlay loadingOverlay;
		public UITableView TableView { get; set; }

		public PoselInterpelationsController (IntPtr handle) : base (handle)
		{
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			TableView = new UITableView () {
				Frame = new CoreGraphics.CGRect (0, 0, View.Bounds.Width, View.Bounds.Height - 115)
			};
			View.AddSubviews (new UIView[] { TableView });

			ShowLoadingOverlay();

			GetData ();
		}

		async private void GetData()
		{

			IPeopleRepository repository = PeopleRepository.Instance;
			try {
				var list = await repository.GetPoselInterpellations ((ParentViewController as PoselController).PoselID);

				TableView.Source = new PoselInterpellationsTableSource(list, this);
				TableView.ReloadData();
			} catch (ApiRequestException ex){
				DisplayError(ex.Message);
				System.Diagnostics.Debug.WriteLine (ex.Message);
			} catch (Exception exc){
				System.Diagnostics.Debug.WriteLine (exc.Message);
			} finally {
				loadingOverlay.Hide();
			}
		}

		public void ShowLoadingOverlay(){
			EdgesForExtendedLayout = UIRectEdge.None;
			var bounds = UIScreen.MainScreen.Bounds;
			loadingOverlay = new LoadingOverlay (bounds);
			View.Add (loadingOverlay);
		}
	}
}
