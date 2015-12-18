//
//  Interpellation.cs
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
using System.Collections.Generic;

namespace AplikacjaParlamentShared.Models
{
	public class Interpellation : IInterpellation
	{
		public Interpellation ()
		{
			Teksty = new List<string> ();
		}

		[JsonProperty("sejm_interpelacje.id")]
		public int Id { get; set; }

		[JsonProperty("sejm_interpelacje.tytul_skrocony")]
		public string TytulSkrocony { get; set; }

		[JsonProperty("sejm_interpelacje.tytul")]
		public string Tytul { get; set; }

		[JsonProperty("sejm_interpelacje.data_wplywu")]
		public string DataWplywu { get; set; }

		[JsonProperty("sejm_interpelacje.adresaci_str")]
		public string Adresat { get; set; }


		//warstwy
		public int DokumentId { get; set; }

		public List<string> Teksty { get; set; }
	}
}

