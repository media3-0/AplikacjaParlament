//
//  SejmListFragment.cs
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

using Android.Support.V4.View;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;

using AplikacjaParlamentShared.Models;
using AplikacjaParlamentShared.Repositories;
using AplikacjaParlamentShared.Api;
using AplikacjaParlamentAndroid.Adapters;

namespace AplikacjaParlamentAndroid
{
	public class SejmListFragment : BaseListFragment
	{

		private List<Posel> list;
		private BaseActivity parentActivity;
        private SearchView _searchView;
        private IMenuItem searchViewMenuItem;

		public override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);
            SetHasOptionsMenu(true);
			parentActivity = Activity as BaseActivity;
		}

		public override void OnStart ()
		{
			base.OnStart ();

			if (list == null) {
				this.loading ();
				GetPoselList ();
			}
		}

        public override void OnCreateOptionsMenu(IMenu menu, MenuInflater inflater) {
            
            inflater.Inflate(Resource.Menu.search, menu);

            searchViewMenuItem = menu.FindItem(Resource.Id.action_search);

            var searchView = searchViewMenuItem.ActionView;
            _searchView = searchView.JavaCast<SearchView>();

            searchViewMenuItem.SetEnabled(false);

            _searchView.QueryTextChange += (s, e) => (ListAdapter as IFilterable).Filter.InvokeFilter(e.NewText);

            _searchView.QueryTextSubmit += (s, e) => {
                e.Handled = true;
            };

            MenuItemCompat.SetOnActionExpandListener(searchViewMenuItem, new SearchViewExpandListener(ListAdapter as IFilterable));
            //item.SetOnActionExpandListener(new SearchViewExpandListener(ListAdapter as IFilterable));

            base.OnCreateOptionsMenu(menu, inflater);
        }

		public override void OnListItemClick(ListView l, View v, int index, long id)
		{
			// We can display everything in place with fragments.
			// Have the list highlight this item and show the data.
			ListView.SetItemChecked(index, true);

			var posel = list.ElementAt (index);

			var detailsActivity = new Intent (Activity, typeof(PersonDetailsActivity));
			detailsActivity.PutExtra ("persontype", (int)PersonTypeEnumeration.Posel);
			detailsActivity.PutExtra ("id", posel.Id);
			detailsActivity.PutExtra ("name", String.Concat(posel.Imie, " ", posel.Nazwisko));
			StartActivity (detailsActivity);
		}

		private async void GetPoselList()
		{
			IPeopleRepository repository = PeopleRepository.Instance;
			try {
				list = await repository.GetPoselList();
				ListAdapter = new SejmListAdapter(parentActivity, list);
				this.loading (true);
                this.ListView.FastScrollEnabled = true;

                searchViewMenuItem.SetEnabled(true);
			} catch (ApiRequestException ex){
				parentActivity.ShowErrorDialog (ex.Message);
			}
		}

        private class SearchViewExpandListener
            : Java.Lang.Object, MenuItemCompat.IOnActionExpandListener
        {
            private readonly IFilterable _adapter;

            public SearchViewExpandListener(IFilterable adapter) {
                _adapter = adapter;
            }

            public bool OnMenuItemActionCollapse(IMenuItem item) {
                if(_adapter != null)
                    _adapter.Filter.InvokeFilter("");
                return true;
            }

            public bool OnMenuItemActionExpand(IMenuItem item) {
                return true;
            }
        }
	}
}

