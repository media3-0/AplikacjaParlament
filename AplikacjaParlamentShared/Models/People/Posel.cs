//
//  Posel.cs
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
using System.Text;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace AplikacjaParlamentShared.Models
{
	public class Posel : IPerson, IPosel
	{

		public Posel () {
			Biura = new List<BiuroPoselskie>();
		}

		[JsonProperty("poslowie.id")]
		public int Id { get; set; }

		[JsonProperty("poslowie.imie_pierwsze")]
		public string Imie { get; set; }

		[JsonProperty("poslowie.nazwisko")]
		public string Nazwisko { get; set; }

		[JsonProperty("poslowie.okreg_wyborczy_numer")]
		public int OkregWyborczyNumer { get; set; }

		[JsonProperty("sejm_kluby.nazwa")]
		public string SejmKlubyNazwa {get; set; }

		[JsonProperty("poslowie.liczba_projektow_uchwal")]
		public int LiczbaProjektowUchwal { get; set; }

		[JsonProperty("poslowie.liczba_projektow_ustaw")]
		public int LiczbaProjektowUstaw { get; set; }

		[JsonProperty("poslowie.data_urodzenia")]
		public string DataUrodzenia { get; set; }

		[JsonProperty("poslowie.frekwencja")]
		public float Frekwencja { get; set; }

		[JsonProperty("poslowie.mowca_id")]
		public int MowcaId { get; set; }

		[JsonProperty("poslowie.zawod")]
		public string Zawod { get; set; }

		[JsonProperty("poslowie.miejsce_zamieszkania")]
		public string MiejsceZamieszkania { get; set; }

		public List<BiuroPoselskie> Biura { get; set; }
		
		public override string ToString ()
		{
			return string.Format ("[Posel: Id={0}, Imie={1}, Nazwisko={2}, OkregWyborczyNumer={3}, SejmKlubyNazwa={4}, LiczbaProjektowUchwal={5}, LiczbaProjektowUstaw={6}, DataUrodzenia={7}, Frekwencja={8}, MowcaId={9}, Zawod={10}, Biura={11}]", 
				Id, Imie, Nazwisko, OkregWyborczyNumer, SejmKlubyNazwa, LiczbaProjektowUchwal, LiczbaProjektowUstaw, DataUrodzenia, Frekwencja, MowcaId, Zawod, Biura.Count);
		}
		
		public string GetWebURL(){
			return System.String.Concat("https://images.weserv.nl/?w=100&h=100&t=square&trim=255&circle&a=t&url=", System.Net.WebUtility.UrlEncode("resources.sejmometr.pl/mowcy/a/0/" + this.MowcaId + ".jpg"));
		}
	}
}

