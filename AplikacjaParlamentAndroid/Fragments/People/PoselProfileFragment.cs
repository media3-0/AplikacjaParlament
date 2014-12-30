//
//  PoselProfileFragment.cs
//
//  Author:
//       Jakub Syty <j.syty@media30.pl>
//
//  Copyright (c) 2014 
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;

using AplikacjaParlamentShared.Models;
using AplikacjaParlamentShared.Api;
using AplikacjaParlamentShared.Repositories;

using Com.Lilarcor.Cheeseknife;
using System.Net.Http;
using com.refractored.monodroidtoolkit.imageloader;

namespace AplikacjaParlamentAndroid
{
	public class PoselProfileFragment : BaseFragment
	{
		[InjectView(Resource.Id.tvImie)]
		private TextView tvImie;

		[InjectView(Resource.Id.tvNazwisko)]
		private TextView tvNazwisko;

		[InjectView(Resource.Id.tvDataZawod)]
		private TextView tvDataZawod;

		[InjectView(Resource.Id.tvPartiaOkreg)]
		private TextView tvPartiaOkreg;

		[InjectView(Resource.Id.tvTelefon)]
		private TextView tvTelefon;

		[InjectView(Resource.Id.tvEmail)]
		private TextView tvEmail;

		[InjectView(Resource.Id.tvUstawy)]
		private TextView tvUstawy;

		[InjectView(Resource.Id.tvUchwaly)]
		private TextView tvUchwaly;

		[InjectView(Resource.Id.tvFrekwencja)]
		private TextView tvFrekwencja;

		[InjectView(Resource.Id.tvZamieszkanie)]
		private TextView tvZamieszkanie;

		[InjectView(Resource.Id.miniature)]
		private ImageView ivMiniature;

		[InjectView(Resource.Id.progressLayout)]
		private RelativeLayout progressLayout;

		[InjectView(Resource.Id.detailsContent)]
		private LinearLayout contentLayout;

		[InjectView(Resource.Id.viewSwitcher)]
		private ViewSwitcher viewSwitcher;

		private PersonDetailsActivity personDetailsActivity;

		private IPosel posel = null;
		private int id;

		public override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

			personDetailsActivity = Activity as PersonDetailsActivity;
			id = personDetailsActivity.PersonId;

		}

		public override View OnCreateView (LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			View view = inflater.Inflate (Resource.Layout.PoselProfileFragmentLayout, container, false);
			Cheeseknife.Inject (this, view);
			return view;
		}

		public override void OnStart ()
		{
			base.OnStart ();

			if (viewSwitcher.CurrentView != progressLayout){
				viewSwitcher.ShowNext(); 
			}
			GetPoselData ();
		}

		private async void GetPoselData()
		{
			IPeopleRepository repository = PeopleRepository.Instance;
			try {
				posel = await repository.GetPosel(id);

				tvImie.Text = posel.Imie;
				tvNazwisko.Text = posel.Nazwisko;
				tvDataZawod.Text = String.Concat(posel.DataUrodzenia, ". ", posel.Zawod);
				tvPartiaOkreg.Text = String.Concat(posel.SejmKlubyNazwa, ". Okręg nr: ", posel.OkregWyborczyNumer);
				tvUstawy.Text = posel.LiczbaProjektowUstaw.ToString();
				tvUchwaly.Text = posel.LiczbaProjektowUchwal.ToString();
				tvFrekwencja.Text = String.Concat(posel.Frekwencja.ToString(), "%");
				tvZamieszkanie.Text = posel.MiejsceZamieszkania;
				loadImage (ivMiniature, String.Concat ("http://resources.sejmometr.pl/mowcy/a/0/", posel.MowcaId, ".jpg"));

				BiuroPoselskie biuroGlowne = posel.Biura.Where(item => item.Podstawowe.Equals("1")).FirstOrDefault();

				if(biuroGlowne != null){
					String[] phones = biuroGlowne.Telefon.Split('f');
					String[] phone1 = phones[0].Split(' ');
					String firstpart = phones[1].Split('(')[1].Split(')')[0];
					String phone = String.Concat(firstpart, " ", phone1[2].Replace('-',' '));

					tvTelefon.Text = phone;

					tvTelefon.Click += delegate {
						var uri = Android.Net.Uri.Parse ("tel:" + phone.Replace(" ", string.Empty));
						var intent = new Intent (Intent.ActionView, uri); 
						StartActivity (intent);    
					};

					tvEmail.Text = biuroGlowne.Email;
				}

				if (viewSwitcher.CurrentView != contentLayout){
					viewSwitcher.ShowPrevious(); 
				}

			} catch (ApiRequestException ex){
				personDetailsActivity.ShowErrorDialog (ex.Message);
			}
		}

		async private void loadImage(ImageView imageView, string url){
			await ImagesHelper.SetImageFromUrlAsync(imageView,url, Activity);
		}
	}
}

