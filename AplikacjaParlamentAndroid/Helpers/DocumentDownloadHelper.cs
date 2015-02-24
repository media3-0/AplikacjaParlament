//
//  DocumentDownloadHelper.cs
//
//  Author:
//       Jakub Syty <j.syty@media30.pl>
//
//  Copyright (c) 2015 
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
using Android.Database;
using Android.Widget;
using AplikacjaParlamentAndroid.Helpers;

namespace AplikacjaParlamentAndroid
{
	public class DocumentDownloadHelper
	{

		private int DocumentID;
		private Activity context;

		private MyBroadcastReceiver broadcastReceiver;

		public long downloadId = 0;

		public DownloadManager downloadManager { get; set; }

		public delegate void FileDownloaded();

		public event FileDownloaded DownloadedEvent;

		public DocumentDownloadHelper (int DocumentID, Activity context)
		{
			this.DocumentID = DocumentID;
			this.context = context;

			broadcastReceiver = new MyBroadcastReceiver(this);
		}

		public void StartDownloading(){
			var file = new Java.IO.File(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath + "/Parlament/" + DocumentID.ToString() + ".pdf");
			if (file.Exists()) {
				var targetUri = Android.Net.Uri.FromFile(file);

				var intent = new Intent(Intent.ActionView);
				intent.SetDataAndType(targetUri, "application/pdf");

				context.StartActivity(intent);
			} else {
				IntentFilter intentFilter = new IntentFilter(DownloadManager.ActionDownloadComplete);
				context.RegisterReceiver(broadcastReceiver, intentFilter);
				downloadManager = (DownloadManager)context.GetSystemService(Android.Content.Context.DownloadService);
				Toast.MakeText(context, "Pobieranie dokumentu pdf w tle. Poczekaj na zakończenie pobieranie i ponownie kliknij na dokument.", ToastLength.Long).Show();
				downloadId = FileDownloader.DownloadFile("http://mojepanstwo.pl/docs/" + DocumentID.ToString() + "/download", DocumentID.ToString() + ".pdf", downloadManager);
			}
		}

		private void DoneDownloading(){
			downloadId = 0;
			context.UnregisterReceiver (broadcastReceiver);
			DownloadedEvent();
		}

		public void Pause(){
			context.UnregisterReceiver(broadcastReceiver);
		}

		public void Resume(){
			if (downloadId > 0) {
				IntentFilter intentFilter = new IntentFilter(DownloadManager.ActionDownloadComplete);
				context.RegisterReceiver(broadcastReceiver, intentFilter);
			}
		}

		public class MyBroadcastReceiver : BroadcastReceiver
		{

			private DocumentDownloadHelper _helper;

			public MyBroadcastReceiver(DocumentDownloadHelper helper) {
				_helper = helper;
			}

			public override void OnReceive(Android.Content.Context context, Intent intent) {
				DownloadManager.Query query = new DownloadManager.Query();
				long id = _helper.downloadId;
				query.SetFilterById(id);
				query.SetFilterByStatus(DownloadStatus.Successful);
				ICursor cursor = _helper.downloadManager.InvokeQuery(query);
				if (cursor.MoveToFirst()) {
					_helper.DoneDownloading();
				}
			}
		}
	}
}

