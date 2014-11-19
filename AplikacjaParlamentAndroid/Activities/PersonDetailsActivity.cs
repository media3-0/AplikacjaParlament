//
//  PersonDetailsActivity.cs
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
using Android.Support.V4.View;
using Android.Util;

using AplikacjaParlamentShared.Collections;
using AplikacjaParlamentShared.Models;
using AplikacjaParlamentShared.Repositories;

namespace AplikacjaParlamentAndroid
{
	[Activity (Label = "Detale")]			
	public class PersonDetailsActivity : BaseActivity
	{

		private PersonTypeEnumeration personType;

		private int personId;

		public int PersonId {
			get {
				return personId;
			}
		}

		private GenericOrderedDictionary<String, Fragment> fragmentsTabs = new GenericOrderedDictionary<String, Fragment> ();

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			SetContentView (Resource.Layout.PersonDetailsLayout);

			personType = (PersonTypeEnumeration)Intent.GetIntExtra ("persontype", (int)PersonTypeEnumeration.Posel);
			personId = Intent.GetIntExtra ("id", -1);
			if (personId == -1)
				IncorrectId ();

			// TODO : Id, imię oraz nazwisko należy pobrać przez intent jako że dane będą wczytywane bezpośrednio w fragmentach!

			//ActionBar.Title = id + " " + person.Imie + " " + person.Nazwisko;

			switch (personType) {
			case PersonTypeEnumeration.Posel:
				{
					fragmentsTabs.Add ("Profil", new PoselProfileFragment ());
					fragmentsTabs.Add ("Głosowania", new PersonVotesFragment ());
					fragmentsTabs.Add ("Wystąpienia", new PersonSpeechesFragment ());
					fragmentsTabs.Add ("Interpelacje", new PersonInterpellationsFragment ());
					break;
				}
			}

			if (fragmentsTabs.Count == 0) {
				Toast.MakeText (this, "Nieprawidłowy typ osoby", ToastLength.Long);
				this.Finish ();
			}

			var tabs = FindViewById<PagerSlidingTabStrip.PagerSlidingTabStrip> (Resource.Id.tabs);
			var pager = FindViewById<ViewPager> (Resource.Id.pager);

			tabs.ShouldExpand = false;

			pager.Adapter = new UniversalFragmentPagerAdapter (FragmentManager, fragmentsTabs);
			tabs.SetViewPager (pager);
		}

		public void IncorrectId(){
			Toast.MakeText (this, "Nieprawidłowe id", ToastLength.Long);
			this.Finish ();
		}
	}
}

