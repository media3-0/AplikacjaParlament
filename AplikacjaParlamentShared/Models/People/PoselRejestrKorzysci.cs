//
//  PoselRejestrKorzysci.cs
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
using Newtonsoft.Json;

namespace AplikacjaParlamentShared.Models
{
	public class PoselRejestrKorzysci
	{
		public PoselRejestrKorzysci ()
		{
		}

		[JsonProperty("poslowie_rejestr_korzysci.data")]
		public string Data { get; set; }

		[JsonProperty("poslowie_rejestr_korzysci.label")]
		public string Label { get; set; }

		[JsonProperty("poslowie_rejestr_korzysci.dokument_id")]
		public int DokumentID { get; set; }
	}
}

