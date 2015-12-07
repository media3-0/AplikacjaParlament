//
//  PoslowieTableSource.cs
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
using System.Collections.Generic;
using AplikacjaParlamentShared.Models;
using SDWebImage;

namespace AplikacjaParlamentIOS
{
	public class PoslowieTableSource : UITableViewSource {

		string CellIdentifier = "TableCell";


		private Dictionary<string, List<Posel>> indexedTableItems;
		private string[] sections;
		private ListaPoslowController owner;


		public PoslowieTableSource (List<Posel> items, ListaPoslowController owner)
		{
			this.owner = owner;
			var poslowie = items;

			poslowie.Sort(delegate(Posel p1, Posel p2) {
				if (p1.Nazwisko == null && p2.Nazwisko == null) return 0;
				else if (p1.Nazwisko == null) return -1;
				else if (p2.Nazwisko == null) return 1;
				else return p1.Nazwisko.CompareTo(p2.Nazwisko);
			});



			indexedTableItems = new Dictionary<string, List<Posel>>();
			foreach (var t in items) {
				if (indexedTableItems.ContainsKey (t.Nazwisko[0].ToString ())) {
					indexedTableItems[t.Nazwisko[0].ToString ()].Add(t);
				} else {
					indexedTableItems.Add (t.Nazwisko[0].ToString (), new List<Posel>() {t});
				}
			}

			sections = new string[indexedTableItems.Keys.Count];
			indexedTableItems.Keys.CopyTo(sections, 0);
		}

		public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
		{
			UITableViewCell cell = tableView.DequeueReusableCell (CellIdentifier);
			//string item = poslowie[indexPath.Row].Imie + ' ' + poslowie[indexPath.Row].Nazwisko;
			var section = sections[indexPath.Section];
			var posel = indexedTableItems [section] [indexPath.Row];
			var item = posel.Imie + " " + posel.Nazwisko;

			if (cell == null) 
			{ 
				cell = new UITableViewCell (UITableViewCellStyle.Subtitle, CellIdentifier); 
			}

			cell.DetailTextLabel.Text = posel.SejmKlubyNazwa + " Okręg nr: " + posel.OkregWyborczyNumer.ToString ();
			cell.TextLabel.Text = item;

			string imgUrl = posel.GetWebURL ();

			cell.ImageView.SetImage (
				url: new NSUrl (imgUrl), 
				placeholder: UIImage.FromBundle ("ImageLoad")
			);
			// TODO : <div>Icon made by <a href="http://www.freepik.com" title="Freepik">Freepik</a> from <a href="http://www.flaticon.com" title="Flaticon">www.flaticon.com</a> is licensed under <a href="http://creativecommons.org/licenses/by/3.0/" title="Creative Commons BY 3.0">CC BY 3.0</a></div>
			return cell;
		}



		public override string[] SectionIndexTitles (UITableView tableView)
		{
			return sections;
		}

		public override nint NumberOfSections (UITableView tableView)
		{
			return sections.Length;
		}

		public override nint SectionFor (UIKit.UITableView tableView, string title, nint atIndex)
		{
			return Array.IndexOf(sections, title);
		}

		public override nint RowsInSection (UITableView tableview, nint section)
		{
			return indexedTableItems[sections[section]].Count;
		}

		public override string TitleForHeader (UITableView tableView, nint section)
		{
			return sections [section];
		}

		public override void RowSelected (UITableView tableView, NSIndexPath indexPath)
		{
			PoselController poselController = owner.Storyboard.InstantiateViewController("PoselController") as PoselController;
			if (poselController != null)
			{
				var section = sections[indexPath.Section];
				var posel = indexedTableItems [section] [indexPath.Row];
				poselController.PoselID = posel.Id;
				owner.NavigationController.PushViewController(poselController, true);
			}  
			tableView.DeselectRow (indexPath, true);
		}
	}
}

