//
//  PoselWspolpracownicyListAdapter.cs
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
using Android.Views;
using Android.Widget;
using AplikacjaParlamentShared.Models;

namespace AplikacjaParlamentAndroid.Adapters
{
	public class PoselWspolpracownicyListAdapter : BaseAdapter<PoselWspolpracownik>
	{

		private class Wrapper : Java.Lang.Object
		{
			public TextView tvData { get; set; }
			public TextView tvNazwa { get; set; }
			public TextView tvFunkcja { get; set; }
		}

		private Activity context;
        private List<PoselWspolpracownik> list;

        public PoselWspolpracownicyListAdapter(Activity context, List<PoselWspolpracownik> list)
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
				view = context.LayoutInflater.Inflate(Resource.Layout.PoselPdfDownloadElement, null);
				wrapper = new Wrapper();
				wrapper.tvData = view.FindViewById<TextView>(Resource.Id.tvData);
                wrapper.tvNazwa = view.FindViewById<TextView>(Resource.Id.tvNazwa);
                wrapper.tvFunkcja = view.FindViewById<TextView>(Resource.Id.tvFunkcja);
				view.Tag = wrapper;
			}
			else
			{
				wrapper = convertView.Tag as Wrapper;
			}

			var wspolpracownik = list[position];
            wrapper.tvData.Text = wspolpracownik.Data;
            wrapper.tvNazwa.Text = wspolpracownik.Nazwa;
            wrapper.tvFunkcja.Text = wspolpracownik.Funkcja;

			return view;
		}

        public override PoselWspolpracownik this[int position]
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