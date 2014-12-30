﻿//
//  AboutActivity.cs
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

namespace AplikacjaParlamentAndroid
{
	[Activity (Label = "")]			
	public class AboutActivity : BaseActivity
	{

		protected override void OnCreate (Bundle bundle)
		{
            SetContentView(Resource.Layout.SimpleActivityLayout);
			base.OnCreate (bundle);

			ListView mDrawerList = FindViewById<ListView>(Resource.Id.left_drawer);
			mDrawerList.Adapter = new LeftDrawerAdapter (this);

			FragmentTransaction ft = FragmentManager.BeginTransaction ();
			Fragment fragmentToView = new AboutFragment();

			if(fragmentToView != null)
				ft.Add (Resource.Id.FragmentContainer, fragmentToView);
			ft.Commit ();
		}
	}
}

