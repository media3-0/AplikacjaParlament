//
//  PersonSpeechesFragment.cs
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
using AplikacjaParlamentShared.Repositories;
using AplikacjaParlamentShared.Api;
using AplikacjaParlamentAndroid.Adapters;

namespace AplikacjaParlamentAndroid
{
	public class PersonSpeechesFragment : ListFragment
	{
		private PersonDetailsActivity personDetailsActivity;

		//private ISpeechPerson person;

		private List<Speech> list;

		public override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

			personDetailsActivity = Activity as PersonDetailsActivity;

		}

		public override void OnStart ()
		{
			base.OnStart ();

			if (list == null) {
				this.SetListShown (false);
				GetSpeechesList ();
			}
		}

		async private void GetSpeechesList()
		{
			IPeopleRepository repository = PeopleRepository.Instance;
			try {
				list = await repository.GetPoselSpeeches (personDetailsActivity.PersonId);
				ListAdapter = new SpeechListAdapter(personDetailsActivity, list);
			} catch (ApiRequestException ex){
				personDetailsActivity.ShowErrorDialog (ex.Message);
			}
		}

		public override void OnListItemClick(ListView l, View v, int index, long id)
		{
			// We can display everything in place with fragments.
			// Have the list highlight this item and show the data.
			ListView.SetItemChecked(index, true);

			var speech = list.ElementAt (index);
			Toast.MakeText (personDetailsActivity, String.Concat ("Id: ", speech.Id), ToastLength.Long);

		}
	}
}

