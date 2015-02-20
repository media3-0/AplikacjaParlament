//
//  PoselWspolpracownicyFragment.cs
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
using AplikacjaParlamentShared.Repositories;
using AplikacjaParlamentShared.Api;
using AplikacjaParlamentAndroid.Adapters;
using AplikacjaParlamentAndroid.Helpers;
using Android.Database;

namespace AplikacjaParlamentAndroid
{
    public class PoselWspolpracownicyFragment : BaseListFragment
	{
		private PersonDetailsActivity personDetailsActivity;

		private List<PoselWspolpracownik> list;

        public long downloadId = 0;

        private MyBroadcastReceiver broadcastReceiver;
        
        public DownloadManager downloadManager;

		public override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

			personDetailsActivity = Activity as PersonDetailsActivity;

            broadcastReceiver = new MyBroadcastReceiver(this);

		}

		public override void OnStart ()
		{
			base.OnStart ();

			if (list == null) {
				this.loading ();
				GetSpeechesList ();
			}
		}

		async private void GetSpeechesList()
		{

			IPeopleRepository repository = PeopleRepository.Instance;
			try {
				switch (personDetailsActivity.PersonType) {
				case PersonTypeEnumeration.Posel:
					{
						list = await repository.GetPoselWspolpracownicy (personDetailsActivity.PersonId);
						break;
					}
				}

                ListAdapter = new PoselWspolpracownicyListAdapter(personDetailsActivity, list);
				this.loading (true);
			} catch (ApiRequestException ex){
				personDetailsActivity.ShowErrorDialog (ex.Message);
			}
		}

        public override void OnPause() {
            base.OnPause();
            Activity.UnregisterReceiver(broadcastReceiver);
        }

        public override void OnResume() {
            base.OnResume();
            if (downloadId > 0) {
                IntentFilter intentFilter = new IntentFilter(DownloadManager.ActionDownloadComplete);
                Activity.RegisterReceiver(broadcastReceiver, intentFilter);
            }
        }

		public override void OnListItemClick(ListView l, View v, int index, long id)
		{
			// We can display everything in place with fragments.
			// Have the list highlight this item and show the data.
			ListView.SetItemChecked(index, true);

			var wspolpracownik = list.ElementAt (index);

            var file = new Java.IO.File(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath + "/Parlament/" + wspolpracownik.DokumentID.ToString() + ".pdf");
            if (file.Exists()) {
                var targetUri = Android.Net.Uri.FromFile(file);

                var intent = new Intent(Intent.ActionView);
                intent.SetDataAndType(targetUri, "application/pdf");

                Activity.StartActivity(intent);
            } else {
                IntentFilter intentFilter = new IntentFilter(DownloadManager.ActionDownloadComplete);
                Activity.RegisterReceiver(broadcastReceiver, intentFilter);
                downloadManager = (DownloadManager)Activity.GetSystemService(Android.Content.Context.DownloadService);
                Toast.MakeText(Activity, "Pobieranie dokumentu pdf w tle. Poczekaj na zakończenie pobieranie i ponownie kliknij na dokument.", ToastLength.Long).Show();
                downloadId = FileDownloader.DownloadFile("http://mojepanstwo.pl/docs/" + wspolpracownik.DokumentID.ToString() + "/download", wspolpracownik.DokumentID.ToString() + ".pdf", downloadManager);
            }
		}

        public void DoneDownloading() {
            Toast.MakeText(Activity, "Pobieranie zakończone pomyślnie. Kliknij na dokument jeszcze raz aby go otworzyć.", ToastLength.Long).Show();
        }

        public class MyBroadcastReceiver : BroadcastReceiver
        {

            private PoselWspolpracownicyFragment _fragment;

            public MyBroadcastReceiver(PoselWspolpracownicyFragment fragment) {
                _fragment = fragment;
            }

            public override void OnReceive(Android.Content.Context context, Intent intent) {
                DownloadManager.Query query = new DownloadManager.Query();
                long id = _fragment.downloadId;
                query.SetFilterById(id);
                query.SetFilterByStatus(DownloadStatus.Successful);
                ICursor cursor = _fragment.downloadManager.InvokeQuery(query);
                if (cursor.MoveToFirst()) {
                    _fragment.DoneDownloading();
                }
            }
        }
	}
}

