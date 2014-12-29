//
//  ProjektAktuPrawnego.cs
//
//  Author:
//       Jakub Syty <j.syty@media30.pl>
//
//  Copyright (c) 2014 Fundacja Media 3.0
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
	public class ProjektAktuPrawnego : IProjektAktuPrawnego
	{
		public ProjektAktuPrawnego ()
		{
		}

		[JsonProperty("prawo_projekty.id")]
		public int Id { set; get; }

		[JsonProperty("prawo_projekty.data_status")]
		public string DataStatus { set; get; }

		[JsonProperty("prawo_projekty.status_str")]
		public string StatusString { set; get; }

		[JsonProperty("prawo_projekty.tytul")]
		public string Tytul { set; get; }

		[JsonProperty("prawo_projekty.autorzy_str")]
		public string AutorzyString { set; get; }

		[JsonProperty("prawo_projekty.opis_skrocony")]
		public string OpisSkrocony { set; get; }
	}
}

