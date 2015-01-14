//
//  VotesFragment.cs
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
using AplikacjaParlamentShared.Repositories;
using AplikacjaParlamentShared.Api;
using AplikacjaParlamentAndroid.Adapters;

namespace AplikacjaParlamentAndroid
{
	public class VotesFragment : BaseListFragment
	{

		private List<Voting> list;

		public override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);
		}

		public override void OnStart ()
		{
			base.OnStart ();

			(Activity as SimpleContainerActivity).SupportActionBar.Title = "Głosowania";

			if (list == null) {
				this.loading ();
				GetData ();
			}
		}

		async private void GetData()
		{

			IPeopleRepository repository = PeopleRepository.Instance;
			try {
				list = await repository.GetAllVotes ();

				ListAdapter = new AllVotesAdapter(Activity, list);
				this.loading (true);
			} catch (ApiRequestException ex){
				(Activity as SimpleContainerActivity).ShowErrorDialog (ex.Message);
			}
		}

		public override void OnListItemClick(ListView l, View v, int index, long id)
		{
			// We can display everything in place with fragments.
			// Have the list highlight this item and show the data.
			ListView.SetItemChecked(index, true);

			var voting = list.ElementAt (index);

			var votingActivity = new Intent (Activity, typeof(SimpleContainerActivity));
			votingActivity.PutExtra ("type", SimpleContainerActivity.VIEW_SEJM_VOTING);
			votingActivity.PutExtra ("id", voting.Id);
			StartActivity (votingActivity);
		}
	}
}

