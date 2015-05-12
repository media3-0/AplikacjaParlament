//
//  BaseActivity.cs
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
using Android.Support.V4.App;

using AlertDialog = Android.Support.V7.App.AlertDialog;

using Com.Lilarcor.Cheeseknife;

namespace AplikacjaParlamentAndroid
{
	[Activity (Label = "BaseActivity")]
	public class BaseActivity : Android.Support.V7.App.AppCompatActivity
	{

		static Boolean active = false;

		protected Android.Support.V7.Widget.Toolbar Toolbar;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
		}

		protected void PrepareViews ()
		{
			Cheeseknife.Inject (this);

			Toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);

			SetSupportActionBar(Toolbar);

			SupportActionBar.SetHomeButtonEnabled(true);
			SupportActionBar.SetDisplayHomeAsUpEnabled(true);
		}

		protected override void OnStart ()
		{
			base.OnStart ();
			active = true;
		}

		protected override void OnStop ()
		{
			base.OnStop ();
			active = false;
		}

		public override bool OnOptionsItemSelected(IMenuItem item)
		{
			switch (item.ItemId)
			{
			case Android.Resource.Id.Home:
				Finish();
				return true;

			default:
				return base.OnOptionsItemSelected(item);
			}
		}

		public void ShowErrorDialog(string message){
			if(!active) return;
			AlertDialog.Builder alert = new AlertDialog.Builder (this);

			alert.SetTitle ("Błąd:");
			alert.SetMessage (message);
			alert.SetPositiveButton ("Ok", (senderAlert, args) => {
				//
			} );
			alert.Create().Show ();
		}
	}
}

