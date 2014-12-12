//
//  Vote.cs
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
using Newtonsoft.Json;

namespace AplikacjaParlamentShared.Models
{
	public class Vote : IVote
	{
		public Vote ()
		{
		}

		[JsonProperty("poslowie_glosy.glosowanie_id")]
		public int GlosowanieId { get; set; }

		[JsonProperty("sejm_glosowania.posiedzenie_id")]
		public int PosiedzenieId { get; set; }

		[JsonProperty("sejm_glosowania.tytul")]
		public string Tytul { get; set; }

		[JsonProperty("sejm_glosowania.czas")]
		public string Data { get; set; }

		[JsonProperty("poslowie_glosy.glos_id")]
		public int GlosId { get; set; }

		[JsonProperty("sejm_glosowania.typ_id")]
		public int Type { get; set; }
	}
}

