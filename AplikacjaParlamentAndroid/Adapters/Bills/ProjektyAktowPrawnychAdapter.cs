//
//  ProjektyAktowPrawnychAdapter.cs
//
//  Author:
//       Jakub Syty <j.syty@media30.pl>
//
//  Copyright (c) 2014 Fundacja Media 3.0
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
using Android.Widget;
using AplikacjaParlamentShared.Models;
using Android.App;
using System.Collections.Generic;
using Android.Views;
using Android.Text;

namespace AplikacjaParlamentAndroid.Adapters
{
	public class ProjektyAktowPrawnychAdapter : BaseAdapter<ProjektAktuPrawnego>
	{

		private class Wrapper : Java.Lang.Object
		{
			public TextView tvData { get; set; }
			public TextView tvTytul { get; set; }
			public TextView tvAutorzy { get; set; }
			public LinearLayout llDodatkowe { get; set; }
			public TextView tvSkrot { get; set; }
			public TextView tvStatus { get; set; }
			public TextView tvNavigation { get; set; }
		}

		private Activity context;
		private List<ProjektAktuPrawnego> list; 

		public ProjektyAktowPrawnychAdapter (Activity context, List<ProjektAktuPrawnego> list)
		{
			this.context = context;
			this.list = list;
		}

		public override View GetView(int position, View convertView, ViewGroup parent)
		{
			Wrapper wrapper = null;
			var view = convertView;
			if (convertView == null)
			{
				view = context.LayoutInflater.Inflate(Resource.Layout.ProjektAktuPrawnegoListElement, null);
				wrapper = new Wrapper();
				wrapper.tvData = view.FindViewById<TextView>(Resource.Id.tvData);
				wrapper.tvTytul = view.FindViewById<TextView>(Resource.Id.tvTytul);
				wrapper.tvAutorzy = view.FindViewById<TextView> (Resource.Id.tvAutorzy);
				wrapper.llDodatkowe = view.FindViewById<LinearLayout> (Resource.Id.dodatkowe);
				wrapper.tvSkrot = view.FindViewById<TextView> (Resource.Id.tvSkrot);
				wrapper.tvStatus = view.FindViewById<TextView> (Resource.Id.tvStatus);
				wrapper.tvNavigation = view.FindViewById<TextView> (Resource.Id.tvNavigate);
				view.Tag = wrapper;
			}
			else
			{
				wrapper = convertView.Tag as Wrapper;
			}

			var projekt = list[position];
			wrapper.tvData.Text = projekt.DataStatus;
			wrapper.tvTytul.Text = projekt.Tytul;
			wrapper.tvAutorzy.Text = projekt.AutorzyString;
			string opis = projekt.OpisSkrocony;
			if (projekt.OpisSkrocony.Equals (""))
				opis = "Brak opisu";
			wrapper.tvSkrot.TextFormatted = Html.FromHtml(opis);
			wrapper.tvStatus.TextFormatted = Html.FromHtml(projekt.StatusString);

			view.Click += delegate {
				if(wrapper.llDodatkowe.Visibility == ViewStates.Gone){
					wrapper.llDodatkowe.Visibility = ViewStates.Visible;
					wrapper.tvNavigation.Text = "Zwiń";
				}else{
					wrapper.llDodatkowe.Visibility = ViewStates.Gone;
					wrapper.tvNavigation.Text = "Rozwiń";
				}
			};

			return view;
		}

		public override ProjektAktuPrawnego this[int position]
		{
			get { return list[position]; }
		}

		public override int Count
		{
			get { return list.Count; }
		}

		public override long GetItemId(int position)
		{
			return position;
		}
	}
}

