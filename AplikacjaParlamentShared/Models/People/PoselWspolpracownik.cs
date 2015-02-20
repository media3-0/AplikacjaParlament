﻿//
//  PoselWspolpracownik.cs
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
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace AplikacjaParlamentShared.Models
{
	public class PoselWspolpracownik : IPoselWspolpracownik
	{
        public PoselWspolpracownik()
		{
		}

        [JsonProperty("poslowie_wspolpracownicy.data")]
        public string Data { get; set; }

        [JsonProperty("poslowie_wspolpracownicy.nazwa")]
        public string Nazwa { get; set; }

        [JsonProperty("poslowie_wspolpracownicy.funkcja")]
        public string Funkcja { get; set; }

        [JsonProperty("poslowie_wspolpracownicy.dokument_id")]
        public int DokumentID { get; set; }
	}
}

