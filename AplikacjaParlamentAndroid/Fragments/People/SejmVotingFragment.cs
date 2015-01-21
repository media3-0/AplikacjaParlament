﻿//
//  SejmVotingFragment.cs
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
using AplikacjaParlamentShared.Collections;
using Android.Support.V4.View;
using AplikacjaParlamentAndroid.Adapters;

namespace AplikacjaParlamentAndroid
{
	public class SejmVotingFragment : BaseFragment
	{

		[InjectView(Resource.Id.viewSwitcher)]
		private ViewSwitcher viewSwitcher;

		[InjectView(Resource.Id.progressLayout)]
		private RelativeLayout progressLayout;

		[InjectView(Resource.Id.tvTytul)]
		private TextView tvTytul;

		[InjectView(Resource.Id.tvData)]
		private TextView tvCzas;

		[InjectView(Resource.Id.tvWynik)]
		private TextView tvWynik;

		[InjectView(Resource.Id.detailsContent)]
		private LinearLayout contentLayout;

		private int id;

		private GenericOrderedDictionary<String, Fragment> fragmentsTabs = new GenericOrderedDictionary<String, Fragment> ();

		public override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

			id = Activity.Intent.GetIntExtra ("id", 0);

			if(id == 0)
			{
				(Activity as BaseActivity).ShowErrorDialog ("Nieprawidłowe informacje o głosowaniu");
				Activity.Finish ();
			}
		}

		public override View OnCreateView (LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			View view = inflater.Inflate (Resource.Layout.SejmVotingFragmentLayout, container, false);
			Cheeseknife.Inject (this, view);

            (Activity as BaseActivity).SupportActionBar.Title = String.Concat("Głosowanie");

			return view;
		}

		public override void OnStart ()
		{
			base.OnStart ();
			if (fragmentsTabs.Count > 0)
				return;
			if (viewSwitcher.CurrentView != progressLayout){
				viewSwitcher.ShowNext(); 
			}
			GetData ();
		}

		private async void GetData()
		{
			IPeopleRepository repository = PeopleRepository.Instance;
			try {
				IVoting glosowanie = await repository.GetSejmVoting (id);
				tvTytul.Text = glosowanie.Tytul;
				tvCzas.Text = glosowanie.Czas;

				if(glosowanie.Wynik == 2){
					tvWynik.Text = "Ustawa Odrzucona";
					tvWynik.SetTextColor(Android.Graphics.Color.ParseColor("#E00909"));
				}

				var glosy = glosowanie.Glosy;
				var glosyZa = glosy.Where(item => item.Glos == 1).ToList();
				var glosyPrzeciw = glosy.Where(item => item.Glos == 2).ToList();
				var glosyWstrzymalSie = glosy.Where(item => item.Glos == 3).ToList();
				var glosyNieobecny = glosy.Where(item => item.Glos == 4).ToList();


				fragmentsTabs.Add ("Za (" + glosyZa.Count +")", new VotingPeopleListFragment (glosyZa));
				fragmentsTabs.Add ("Przeciw (" + glosyPrzeciw.Count +")", new VotingPeopleListFragment (glosyPrzeciw));
				fragmentsTabs.Add ("Wstrzymał się (" + glosyWstrzymalSie.Count +")", new VotingPeopleListFragment (glosyWstrzymalSie));
				fragmentsTabs.Add ("Nieobecny (" + glosyNieobecny.Count +")", new VotingPeopleListFragment (glosyNieobecny));

				var tabs = Activity.FindViewById<PagerSlidingTabStrip.PagerSlidingTabStrip> (Resource.Id.tabs);
				var pager = Activity.FindViewById<ViewPager> (Resource.Id.pager);

				tabs.ShouldExpand = false;

				pager.Adapter = new UniversalFragmentPagerAdapter (FragmentManager, fragmentsTabs);
				tabs.SetViewPager (pager);

				if (viewSwitcher.CurrentView != contentLayout){
					viewSwitcher.ShowPrevious(); 
				}

			} catch (ApiRequestException ex){
				(Activity as BaseActivity).ShowErrorDialog (ex.Message);
			} catch (Exception exc){
				//raportowanie błędów przy ładowaniu danych
				Xamarin.Insights.Report (exc); 
			}
		}
	}
}

