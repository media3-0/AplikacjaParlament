//
//  MainActivity.cs
//
//  Author:
//       Jakub Syty <j.syty@media30.pl>
//
//  Copyright (c) 2014 Fundacja Media 3.0
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

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

using Com.Lilarcor.Cheeseknife;
using Android.Net;
using Android.Support.V4.App;
using AplikacjaParlamentShared.Repositories;
using AplikacjaParlamentShared.Models;

namespace AplikacjaParlamentAndroid
{
	[Activity (Label = "Aplikacja Parlament", MainLauncher = true)]
	public class MainActivity : Android.Support.V7.App.ActionBarActivity
	{

		[InjectView(Resource.Id.myButton)]
		Button button;

		[InjectView(Resource.Id.voteButton)]
		Button voteButton;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			SetContentView (Resource.Layout.Main);

			Cheeseknife.Inject (this);

            var toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);

            SetSupportActionBar(toolbar);
			
			button.Click += delegate {
				StartActivity(typeof(PeopleActivity));
			};

			voteButton.Click += delegate {
				//dla testów, wyświetla głosowanie o id 1
				var votingActivity = new Intent (this, typeof(SimpleContainerActivity));
				votingActivity.PutExtra ("type", SimpleContainerActivity.VIEW_SEJM_VOTING);
				votingActivity.PutExtra ("id", 1);
				StartActivity (votingActivity);
			};
		}

		protected override void OnStart ()
		{
			base.OnStart ();
			var connectivityManager = (ConnectivityManager)GetSystemService(ConnectivityService);
			var activeConnection = connectivityManager.ActiveNetworkInfo;
			if ((activeConnection == null)  || !activeConnection.IsConnected)
			{
				// brak połączenia z siecią
				AlertDialog.Builder alert = new AlertDialog.Builder (this);

				alert.SetTitle ("Błąd:");
				alert.SetMessage ("Brak połączenia z internetem!");
				alert.SetPositiveButton ("Ok", (senderAlert, args) => {
					//
				} );
				alert.Show ();
			}

		}
	}
}


