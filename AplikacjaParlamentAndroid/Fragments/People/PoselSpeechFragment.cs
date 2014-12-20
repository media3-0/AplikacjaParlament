//
//  PoselProfileFragment.cs
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
using System.Net.Http;
using Android.Text;

namespace AplikacjaParlamentAndroid
{
	public class PoselSpeechFragment : BaseFragment
	{
		[InjectView(Resource.Id.textView1)]
		private TextView textView;

		[InjectView(Resource.Id.progressLayout)]
		private RelativeLayout progressLayout;

		[InjectView(Resource.Id.detailsContent)]
		private LinearLayout contentLayout;

		[InjectView(Resource.Id.viewSwitcher)]
		private ViewSwitcher viewSwitcher;

		private int id;
		private string poselName;
		private ISpeech speech;

		public override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

			id = Activity.Intent.GetIntExtra ("id", 0);
			poselName = Activity.Intent.GetStringExtra ("name");

			if(id == 0 || poselName == null)
			{
				(Activity as BaseActivity).ShowErrorDialog ("Nieprawidłowe informacje o wystąpieniu");
				Activity.Finish ();
			}
		}

		public override View OnCreateView (LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			View view = inflater.Inflate (Resource.Layout.PoselSpeechFragmentLayout, container, false);
			Cheeseknife.Inject (this, view);

			(Activity as BaseActivity).SupportActionBar.Title = String.Concat (poselName, " - wystąpienie");

			return view;
		}

		public override void OnStart ()
		{
			base.OnStart ();
			if (viewSwitcher.CurrentView != progressLayout){
				viewSwitcher.ShowNext(); 
			}
			GetSpeechData ();
		}

		private async void GetSpeechData()
		{
			IPeopleRepository repository = PeopleRepository.Instance;
			try {
				speech = await repository.GetPoselSpeech (id);
				textView.TextFormatted = Html.FromHtml(speech.Tresc);

				if (viewSwitcher.CurrentView != contentLayout){
					viewSwitcher.ShowPrevious(); 
				}

			} catch (ApiRequestException ex){
				(Activity as BaseActivity).ShowErrorDialog (ex.Message);
			}
		}
	}
}

