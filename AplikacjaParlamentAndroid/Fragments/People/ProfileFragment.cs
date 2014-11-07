//
//  ProfileFragment.cs
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

using Com.Lilarcor.Cheeseknife;

namespace AplikacjaParlamentAndroid
{
	public class ProfileFragment : BaseFragment
	{
		[InjectView(Resource.Id.textView1)]
		private TextView textView;

		private PersonDetailsActivity personDetailsActivity;

		private IPerson person;

		public override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

			personDetailsActivity = Activity as PersonDetailsActivity;
			person = personDetailsActivity.Person;
		}

		public override View OnCreateView (LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			View view = inflater.Inflate (Resource.Layout.PersonProfileFragmentLayout, container, false);
			Cheeseknife.Inject (this, view);
			return view;
		}

		public override void OnStart ()
		{
			base.OnStart ();
			//mock
			StringBuilder sb = new StringBuilder ();
			sb.Append ("Id: ").Append (person.Id);
			sb.Append ("\nImię: ").Append (person.Imie);
			sb.Append ("\nNazwisko: ").Append (person.Nazwisko);
			textView.Text = sb.ToString ();
			//mock
		}
	}
}

