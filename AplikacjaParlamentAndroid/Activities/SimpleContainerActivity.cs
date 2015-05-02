//
//  SimpleContainerActivity.cs
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
	public class SimpleContainerActivity : BaseActivity
	{

		public const int VIEW_POSEL_SPEECH = 1;
		public const int VIEW_INTERPELLATION = 2;
		public const int VIEW_SEJM_VOTING = 3;
		public const int VIEW_ALL_VOTES = 4;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
            SetContentView(Resource.Layout.SimpleActivityLayout);
			this.PrepareViews ();

			ListView mDrawerList = FindViewById<ListView>(Resource.Id.left_drawer);
			mDrawerList.Adapter = new LeftDrawerAdapter (this);

			int type = Intent.GetIntExtra ("type", 0);

			if (type == 0) {
				Toast.MakeText (this, "Nieprawidłowy typ fragmentu", ToastLength.Long);
				this.Finish ();
				return;
			}

			FragmentTransaction ft = FragmentManager.BeginTransaction ();
			Fragment fragmentToView = null;

			switch (type) {
			case VIEW_POSEL_SPEECH:
				{
					fragmentToView = new PoselSpeechFragment ();
					break;
				}

			case VIEW_INTERPELLATION:
				{
					fragmentToView = new InterpellationFragment ();
					break;
				}

			case VIEW_SEJM_VOTING:
				{
					fragmentToView = new SejmVotingFragment ();
					break;
				}

			case VIEW_ALL_VOTES:
				{
					fragmentToView = new VotesFragment ();
					break;
				}
		}

			if(fragmentToView != null)
				ft.Add (Resource.Id.FragmentContainer, fragmentToView);
			ft.Commit ();
		}
	}
}

