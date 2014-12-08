//
//  BaseListFragment.cs
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

namespace AplikacjaParlamentAndroid
{
	public class BaseListFragment : ListFragment
	{

		private TextView emptyView;

		public override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

			// Create your fragment here
		}

		public override View OnCreateView (LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			View view = base.OnCreateView (inflater, container, savedInstanceState);
			LinearLayout.LayoutParams layoutParams = new LinearLayout.LayoutParams (ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.WrapContent);
			emptyView = new TextView (Activity) {
				LayoutParameters = layoutParams,
				Gravity = GravityFlags.CenterHorizontal,
				Text = "Pusta lista!",
				TextSize = 20.0f
			};
			emptyView.SetPadding (0, 200, 0, 0);

			FrameLayout frame = view as FrameLayout;
			frame.AddView (emptyView, 0);
			ListView list = view.FindViewById<ListView> (Android.Resource.Id.List);
			list.EmptyView = emptyView;
			return view;
		}

		public void loading(bool done = false)
		{
			if (!done) {
				this.SetListShown (false);
				emptyView.Visibility = ViewStates.Gone;
			} else {
				if(this.ListAdapter.IsEmpty)
					emptyView.Visibility = ViewStates.Visible;
			}
		}
	}
}

