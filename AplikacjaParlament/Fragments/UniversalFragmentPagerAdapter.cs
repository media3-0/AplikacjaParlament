//
//  UniversalFragmentPagerAdapter.cs
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
using AplikacjaParlament.Collections;
using Android.App;

namespace AplikacjaParlament
{
	public class UniversalFragmentPagerAdapter : Android.Support.V13.App.FragmentPagerAdapter
	{

		public UniversalFragmentPagerAdapter (FragmentManager fm, GenericOrderedDictionary<String, Fragment> _fragmentsTabs) : base (fm)
		{
			fragmentsTabs = _fragmentsTabs;
		} 

		public override int Count
		{
			get { return fragmentsTabs.Count; }
		}

		private GenericOrderedDictionary<String, Fragment> fragmentsTabs;

		public override Java.Lang.ICharSequence GetPageTitleFormatted(int position)
		{
			return new Java.Lang.String(fragmentsTabs.GetItem(position).Key);
		}

		public override Fragment GetItem(int position)
		{
			return fragmentsTabs [position];
		}
	}
}