//
//  SejmGlosowanieLayer.cs
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
using Newtonsoft.Json.Linq;
using AplikacjaParlamentShared.Models;
using System.Collections.Generic;

namespace AplikacjaParlamentShared.Api
{
	public class SejmGlosowanieLayer : Layer
	{
		public SejmGlosowanieLayer (string name) : base(name)
		{
		}

		public override void ParseJObject (Object obj)
		{
			Voting voting = (obj as Voting);
			JArray arr = (this.JsonObject as JArray);
			voting.Glosy.Clear ();

			foreach (JObject item in arr) {
				IVotingEntry votingEntry = new VotingEntry ();
				votingEntry.Glos = item.Value<JObject> ("glosy").Value<int> ("glos_id");
				votingEntry.Glosujacy = item.Value<JObject> ("poslowie").Value<int> ("id");
				votingEntry.GlosujacyImieNazwisko = item.Value<JObject> ("poslowie").Value<string> ("nazwa");
				votingEntry.MowcaId = item.Value<JObject> ("mowcy").Value<int> ("mowca_id");
				voting.Glosy.Add (votingEntry);
			}
		}
	}
}

