//
//  Voting.cs
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
using Newtonsoft.Json;

namespace AplikacjaParlamentShared.Models
{
	public class Voting : IVoting
	{
		public Voting ()
		{
			Glosy = new List<IVotingEntry> ();
		}

		[JsonProperty("sejm_glosowania.id")]
		public int Id { set; get; }

		[JsonProperty("sejm_glosowania.wynik_id")]
		public int Wynik { set; get; }

		[JsonProperty("sejm_glosowania.tytul")]
		public string Tytul { set; get; }

		[JsonProperty("sejm_glosowania.czas")]
		public string Czas { set; get; }

		public List<IVotingEntry> Glosy { set; get; }
	}
}

