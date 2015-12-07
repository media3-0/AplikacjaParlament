using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;
using AplikacjaParlamentShared.Repositories;
using AplikacjaParlamentShared.Models;
using AplikacjaParlamentShared.Api;
using System.Drawing;
using CoreGraphics;
using SDWebImage;
using System.Linq;

namespace AplikacjaParlamentIOS
{
	partial class PoselDetailsController : UIViewController
	{

		private IPosel posel;
		LoadingOverlay loadingOverlay;

		public PoselDetailsController (IntPtr handle) : base (handle)
		{
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			var bounds = UIScreen.MainScreen.Bounds;
			loadingOverlay = new LoadingOverlay (bounds);
			View.Add (loadingOverlay);
		}

		public override void ViewDidAppear(bool animated)
		{
			base.ViewDidAppear(animated);
			GetPoselData();
		}

		private async void GetPoselData()
		{
			IPeopleRepository repository = PeopleRepository.Instance;
			try {
				posel = await repository.GetPosel((ParentViewController as PoselController).PoselID);



				BiuroPoselskie biuroGlowne = posel.Biura.Where(item => item.Podstawowe.Equals("1")).FirstOrDefault();

				if(biuroGlowne != null){
					String phone = "";
					try {
						String[] phones = biuroGlowne.Telefon.Split('f');
						String[] phone1 = phones[0].Split(' ');
						String firstpart = phones[1].Split('(')[1].Split(')')[0];
						phone = String.Concat(firstpart, " ", phone1[2].Replace('-',' '));
					}catch(Exception e){
						System.Diagnostics.Debug.WriteLine (e.Message);
						phone = biuroGlowne.Telefon;
					}
					Telephone.Text = phone;

					/*
					Telephone.Click += delegate {
						var uri = Android.Net.Uri.Parse ("tel:" + phone.Replace(" ", string.Empty));
						var intent = new Intent (Intent.ActionView, uri); 
						StartActivity (intent);    
					};
					*/ // TODO : Telephone click

					Email.Text = biuroGlowne.Email;
				}


				NameLabel.Text = posel.Imie + " " + posel.Nazwisko;
				BirthdayOccupation.Text = String.Concat(posel.DataUrodzenia, ". ", posel.Zawod);
				PartiaOkreg.Text = String.Concat(posel.SejmKlubyNazwa, ". Okręg nr: ", posel.OkregWyborczyNumer);
				UstawyUchwaly.Text = String.Concat(
					"Ustawy: ", posel.LiczbaProjektowUstaw.ToString(), 
					"   Uchwały: ", posel.LiczbaProjektowUchwal.ToString()
				);
				Frequency.Text = String.Concat("Frekwencja: ", posel.Frekwencja.ToString(), "%");
				FromWhere.Text = posel.MiejsceZamieszkania;

				ParentViewController.Title = NameLabel.Text;
				var imgUrl = posel.GetWebURL(); 
				PoselImage.SetImage (
					url: new NSUrl (imgUrl), 
					placeholder: UIImage.FromBundle ("ImageLoad")
				);

				loadingOverlay.Hide();

			} catch (ApiRequestException ex){
				//personDetailsActivity.ShowErrorDialog (ex.Message); // TODO : Dialog błędu
			} catch (Exception exc){
				//raportowanie błędów przy ładowaniu danych
				//Xamarin.Insights.Report (exc);  // TODO : Insights?
				System.Diagnostics.Debug.WriteLine (exc.Message);
			}
		}
	}
}
