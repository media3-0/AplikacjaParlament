//
//  LeftDrawerAdapter.cs
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
using Android.Widget;
using Android.App;
using Android.Views;
using Android.Content;

namespace AplikacjaParlamentAndroid
{
	public class LeftDrawerAdapter : BaseAdapter<String>
	{

		private Activity context;
		private String[] items = new String[]{
			"Ekran główny",
			"Izby"
		};

		public LeftDrawerAdapter (Activity context) : base()
		{
			this.context = context;
		}

		public override long GetItemId(int position)
		{
			return position;
		}

		public override string this[int position] {  
			get { return items[position]; }
		}

		public override int Count {
			get { return items.Length; }
		}

		public override View GetView(int position, View convertView, ViewGroup parent)
		{
			View view = convertView; // re-use an existing view, if one is available
			if (view == null) // otherwise create a new one
				view = context.LayoutInflater.Inflate(Android.Resource.Layout.SimpleListItem1, null);
			view.FindViewById<TextView>(Android.Resource.Id.Text1).Text = items[position];

			view.Click += delegate {
				System.Type classType = null;

				switch(position){
				case 0:
					classType = typeof(MainActivity);
					break;
				case 1:
					classType = typeof(PeopleActivity);
					break;
				};

				if(!classType.Equals(context.GetType())){
					var activity = new Intent (context, classType);
					context.StartActivity (activity);
				}
			};

			return view;
		}
	}
}

