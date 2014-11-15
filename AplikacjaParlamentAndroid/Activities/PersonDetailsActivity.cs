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

		private IPerson person;

		public IPerson Person {
			get {
				return person;
			}
		}

		private GenericOrderedDictionary<String, Fragment> fragmentsTabs = new GenericOrderedDictionary<String, Fragment> (){
			{ "Profil", new PoselProfileFragment() },
			{ "Głosowania", new PersonVotesFragment() },
			{ "Wystąpienia", new PersonSpeechesFragment() }
		};

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			SetContentView (Resource.Layout.PersonDetailsLayout);

			personType = (PersonTypeEnumeration)Intent.GetIntExtra ("persontype", (int)PersonTypeEnumeration.Posel);
			int id = Intent.GetIntExtra ("id", -1);

			// FIXME : zamienic if na Exception dla nieistniejącego id!
			if (id > -1)
				person = PeopleRepository.Instance.GetPosel (id);

			ActionBar.Title = id + " " + person.Imie + " " + person.Nazwisko;

			//jeżeli jest posłem do dodaj zakładkę interpelacje
			if(personType.Equals(PersonTypeEnumeration.Posel))
				fragmentsTabs.Add ("Interpelacje", new PersonInterpellationsFragment ());

			var tabs = FindViewById<PagerSlidingTabStrip.PagerSlidingTabStrip> (Resource.Id.tabs);
			var pager = FindViewById<ViewPager> (Resource.Id.pager);

			tabs.ShouldExpand = false;

			pager.Adapter = new UniversalFragmentPagerAdapter (FragmentManager, fragmentsTabs);
			tabs.SetViewPager (pager);
		}
	}
}

