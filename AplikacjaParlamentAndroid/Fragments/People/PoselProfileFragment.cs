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

namespace AplikacjaParlamentAndroid
{
	public class PoselProfileFragment : BaseFragment
	{
		[InjectView(Resource.Id.textView1)]
		private TextView textView;

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
				textView.Text = posel.ToString ();

				if (viewSwitcher.CurrentView != contentLayout){
					viewSwitcher.ShowPrevious(); 
				}

			} catch (ApiRequestException ex){
				personDetailsActivity.ShowErrorDialog (ex.Message);
			}
		}
	}
}

