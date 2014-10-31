//
//  PoslowieListsPagerAdapter.cs
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
using Android.Support.V4.View;
using Android.App;

namespace AplikacjaParlament
{
	public class PoslowieListsPagerAdapter : Android.Support.V13.App.FragmentPagerAdapter
	{

		int count;

		public override int Count 
		{
			get 
			{
				return count;   
			}   
		}

		public override Android.App.Fragment GetItem (int position)
		{

			switch(position) 
			{
			case 0:
				return new SejmListFragment ();
			case 1: 
				return new SenatListFragment ();
			default: 
				return new SejmListFragment ();
			}       
		}

		public PoslowieListsPagerAdapter (Android.App.FragmentManager fm) : base (fm)
		{
			count = 2;
		} 
	}

	public class ViewPageListenerForActionBar : ViewPager.SimpleOnPageChangeListener
	{
		private ActionBar _bar;
		public ViewPageListenerForActionBar(ActionBar bar)
		{
			_bar = bar;
		}
		public override void OnPageSelected(int position)
		{
			_bar.SetSelectedNavigationItem(position);
		}
	}
	public static class ViewPagerExtensions
	{
		public static ActionBar.Tab GetViewPageTab(this ViewPager viewPager, ActionBar actionBar, string name)
		{
			var tab = actionBar.NewTab();
			tab.SetText(name);
			tab.TabSelected += (o, e) =>
			{
				viewPager.SetCurrentItem(actionBar.SelectedNavigationIndex, false);
			};
			return tab;
		}
	}
}

