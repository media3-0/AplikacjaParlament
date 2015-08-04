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
using AplikacjaParlamentAndroid.Adapters;

namespace AplikacjaParlamentAndroid
{
	[Activity (Label = "Detale")]			
	public class PersonDetailsActivity : BaseActivity
	{

		public PersonTypeEnumeration PersonType {
			get;
			set;
		}

		public int PersonId {
			get;
			set;
		}

		public string PersonName { get; set; }

		private com.refractored.PagerSlidingTabStrip tabs;
		private ViewPager pager;

		private GenericOrderedDictionary<String, Fragment> fragmentsTabs = new GenericOrderedDictionary<String, Fragment> ();

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate(bundle);	
            SetContentView(Resource.Layout.PersonDetailsLayout);
			this.PrepareViews ();
             
			ListView mDrawerList = FindViewById<ListView>(Resource.Id.left_drawer);
			mDrawerList.Adapter = new LeftDrawerAdapter (this);		

			PersonType = (PersonTypeEnumeration)Intent.GetIntExtra ("persontype", (int)PersonTypeEnumeration.Posel);
			PersonId = Intent.GetIntExtra ("id", -1);
			if (PersonId == -1)
				IncorrectId ();
			PersonName = Intent.GetStringExtra ("name");

			SupportActionBar.Title = String.Concat("Poseł: ", PersonName);

			switch (PersonType) {
			case PersonTypeEnumeration.Posel:
				{
					fragmentsTabs.Add ("Profil", new PoselProfileFragment ());
					fragmentsTabs.Add ("Głosowania", new PersonVotesFragment ());
					fragmentsTabs.Add ("Wystąpienia", new PersonSpeechesFragment ());
					fragmentsTabs.Add ("Interpelacje", new PersonInterpellationsFragment ());
                    fragmentsTabs.Add ("Współpracownicy", new PoselWspolpracownicyFragment());
					fragmentsTabs.Add ("Oświadczenia majątkowe", new PoselOswiadczeniaMajatkoweFragment ());
					fragmentsTabs.Add ("Rejestr Korzyści", new PoselRejestrKorzysciFragment ());
					fragmentsTabs.Add ("Aktywność", new PersonNewestFragment ());
                    
					break;
				}
			}

			if (fragmentsTabs.Count == 0) {
				Toast.MakeText (this, "Nieprawidłowy typ osoby", ToastLength.Long);
				this.Finish ();
			}

			tabs = FindViewById<com.refractored.PagerSlidingTabStrip> (Resource.Id.tabs);
			pager = FindViewById<ViewPager> (Resource.Id.pager);

			tabs.ShouldExpand = false;

			pager.Adapter = new UniversalFragmentPagerAdapter (FragmentManager, fragmentsTabs);
			tabs.SetViewPager (pager);
		}

		public void IncorrectId(){
			Toast.MakeText (this, "Nieprawidłowe id", ToastLength.Long);
			this.Finish ();
		}

		public override bool OnCreateOptionsMenu (IMenu menu)
		{
			base.OnCreateOptionsMenu (menu);
			for (int i = 0; i < fragmentsTabs.Count; i++) {
				menu.Add(0,100 + i,i,new Java.Lang.String(fragmentsTabs.GetItem(i).Key));
			}
			return true;
		}

		public override bool OnOptionsItemSelected (IMenuItem item)
		{
			if (item.ItemId >= 100 && item.ItemId <= (100 + fragmentsTabs.Count)) {
				pager.SetCurrentItem (item.ItemId - 100, true);
				return true;
			}
			return base.OnOptionsItemSelected (item);
		}
	}
}

