﻿//
//  PoselVotesTableSource.cs
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
using AplikacjaParlamentShared.Repositories;
using System.Linq;
using AplikacjaParlamentShared.Api;

namespace AplikacjaParlamentIOS
{
	public class PoselSpeechesTableSource : UITableViewSource {

		string CellIdentifier = "PoselSpeechesTableCell";

		private PoselSpeechController owner;
		List<Speech> items;


		public PoselSpeechesTableSource (List<Speech> items, PoselSpeechController owner)
		{
			this.owner = owner;
			this.items = items;
		}

		public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
		{
			UITableViewCell cell = tableView.DequeueReusableCell (CellIdentifier);
			var speech = items[indexPath.Row];

			if (cell == null) 
			{ 
				cell = new UITableViewCell (UITableViewCellStyle.Subtitle, CellIdentifier); 
			}

			cell.TextLabel.Text = speech.Tytul;
			cell.DetailTextLabel.Text = speech.Skrot;

			return cell;
		}

		public override nint NumberOfSections (UITableView tableView)
		{
			return 1;
		}

		public override nint RowsInSection (UITableView tableview, nint section)
		{
			return items.Count;
		}

		public override void RowSelected (UITableView tableView, NSIndexPath indexPath)
		{
			owner.ShowLoadingOverlay();
			GetData(items[indexPath.Row].Id);
			tableView.DeselectRow (indexPath, true);
		}

		private async void GetData(int id)
		{
			IPeopleRepository repository = PeopleRepository.Instance;
			try {
				var speech = await repository.GetPoselSpeech (id);
				string text = speech.Tresc;

				TextContentController textContentController = owner.Storyboard.InstantiateViewController("TextContentController") as TextContentController;
				if (textContentController != null)
				{
					textContentController.TextToView = text;
					owner.NavigationController.PushViewController(textContentController, true);
				}  

			} catch (ApiRequestException ex){
				owner.DisplayError (ex.Message);
			} finally {
				owner.loadingOverlay.Hide();
			}
		}
	}
}

