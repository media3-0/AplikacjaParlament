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

namespace AplikacjaParlament
{
	[Activity (Label = "Detale")]			
	public class PersonDetailsActivity : BaseActivity
	{

		private PersonTypeEnumeration personType;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			personType = (PersonTypeEnumeration)Intent.GetIntExtra ("persontype", (int)PersonTypeEnumeration.Posel);
			string name = Intent.GetStringExtra ("name") ?? "data not passed";

			ActionBar.Title = name;

			//TODO : Zakładki ViewPagerIndication na bibliotece PagerSlidingTabStrip.Net
		}
	}
}

