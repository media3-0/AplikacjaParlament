//
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

namespace AplikacjaParlamentIOS
{
	public class PoselOswiadczeniaMajatkoweTableSource : UITableViewSource {

		string CellIdentifier = "PoselOswiadczeniaMajatkoweTableCell";

		private PoselOswiadczeniaMajatkoweController owner;
		List<PoselOswiadczeniaMajatkowe> items;


		public PoselOswiadczeniaMajatkoweTableSource (List<PoselOswiadczeniaMajatkowe> items, PoselOswiadczeniaMajatkoweController owner)
		{
			this.owner = owner;
			this.items = items;
		}

		public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
		{
			UITableViewCell cell = tableView.DequeueReusableCell (CellIdentifier);
			var oswiadczenie = items[indexPath.Row];

			if (cell == null) 
			{ 
				cell = new UITableViewCell (UITableViewCellStyle.Subtitle, CellIdentifier); 
			}

			cell.TextLabel.Text = oswiadczenie.Label;
			cell.DetailTextLabel.Text = oswiadczenie.Data;

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
			/* 
			PoselController poselController = owner.Storyboard.InstantiateViewController("PoselController") as PoselController;
			if (poselController != null)
			{
				var vote = items[indexPath.Row];
				poselController.PoselID = posel.Id;
				owner.NavigationController.PushViewController(poselController, true);
			}  
			*/
			tableView.DeselectRow (indexPath, true);
		}
	}
}

