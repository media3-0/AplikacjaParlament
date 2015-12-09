//
//  DownloadHelper.cs
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
using UIKit;
using Foundation;
using System.IO;
using System.Drawing;
using System.Net;
using ToastIOS;

namespace AplikacjaParlamentIOS
{
	public class DownloadHelper
	{

		int docId;
		UIViewController owner;

		public DownloadHelper (int id, UIViewController owner)
		{
			this.docId = id;
			this.owner = owner;
		}

		public void DownloadOrOpen(){
			if (IsDownloaded ()) {
				OpenFile ();
			} else {
				DownloadFile ();
			}
		}

		public bool IsDownloaded(){
			return File.Exists(PdfPath());
		}

		private void DownloadFile(){
			var webClient = new WebClient();
			webClient.DownloadDataCompleted += (s, e) => {
				var bytes = e.Result;
				string localPath = PdfPath ();
				File.WriteAllBytes (localPath, bytes); 

				owner.InvokeOnMainThread (() => {
					(owner as TableHandler).TableView.ReloadData();
					OpenFile ();
				});
			};
			var url = new Uri("http://mojepanstwo.pl/docs/" + docId.ToString() + "/download");
			Toast.MakeText("Trwa pobieranie dokumentu. Poczekaj chwilę").Show();
			webClient.DownloadDataAsync(url);
		}

		private void OpenFile(){
			string filePath = PdfPath();
			var viewer = UIDocumentInteractionController.FromUrl(NSUrl.FromFilename(filePath));
			viewer.PresentOpenInMenu(new RectangleF(0,-260,320,320),this.owner.View, true);
		}

		private string PdfPath(){
			string path = Environment.GetFolderPath (Environment.SpecialFolder.Personal);
			return Path.Combine (path, docId.ToString () + ".pdf");
		}
	}
}

