using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;
using AplikacjaParlamentShared.Repositories;
using AplikacjaParlamentShared.Models;
using AplikacjaParlamentShared.Api;
using System.Drawing;
using CoreGraphics;

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
				NSThread.SleepFor(2);
				posel = await repository.GetPosel((ParentViewController as PoselController).PoselID);

				/*
				tvImie.Text = posel.Imie;
				tvNazwisko.Text = posel.Nazwisko;
				tvDataZawod.Text = String.Concat(posel.DataUrodzenia, ". ", posel.Zawod);
				tvPartiaOkreg.Text = String.Concat(posel.SejmKlubyNazwa, ". Okręg nr: ", posel.OkregWyborczyNumer);
				tvUstawy.Text = posel.LiczbaProjektowUstaw.ToString();
				tvUchwaly.Text = posel.LiczbaProjektowUchwal.ToString();
				tvFrekwencja.Text = String.Concat(posel.Frekwencja.ToString(), "%");
				tvZamieszkanie.Text = posel.MiejsceZamieszkania;

				string imgUrl = String.Concat ("http://images.weserv.nl/?w=220&h=220&t=square&trim=255&circle&a=t&url=", System.Net.WebUtility.UrlEncode("resources.sejmometr.pl/mowcy/a/0/" + posel.MowcaId + ".jpg"));

				AQuery aq = new AQuery(Activity);

				Bitmap imgLoading = aq.GetCachedImage(Android.Resource.Drawable.IcMenuGallery);

				((AQuery)aq.Id(Resource.Id.miniature)).Image(imgUrl, true, true, 0, 0, imgLoading, 0, 1f);

				//loadImage (ivMiniature, );

				BiuroPoselskie biuroGlowne = posel.Biura.Where(item => item.Podstawowe.Equals("1")).FirstOrDefault();

				if(biuroGlowne != null){
					String phone = "";
					try {
						String[] phones = biuroGlowne.Telefon.Split('f');
						String[] phone1 = phones[0].Split(' ');
						String firstpart = phones[1].Split('(')[1].Split(')')[0];
						phone = String.Concat(firstpart, " ", phone1[2].Replace('-',' '));
					}catch(Exception e){
						Log.Error("PhoneParse", e.Message);
						phone = biuroGlowne.Telefon;
					}
					tvTelefon.Text = phone;

					tvTelefon.Click += delegate {
						var uri = Android.Net.Uri.Parse ("tel:" + phone.Replace(" ", string.Empty));
						var intent = new Intent (Intent.ActionView, uri); 
						StartActivity (intent);    
					};

					tvEmail.Text = biuroGlowne.Email;
				}
				*/

				NameLabel.Text = posel.Imie + " " + posel.Nazwisko;

				loadingOverlay.Hide();

			} catch (ApiRequestException ex){
				//personDetailsActivity.ShowErrorDialog (ex.Message);
			} catch (Exception exc){
				//raportowanie błędów przy ładowaniu danych
				//Xamarin.Insights.Report (exc);  // TODO : Insights?
				System.Diagnostics.Debug.WriteLine (exc.Message);
			}
		}
	}
}
