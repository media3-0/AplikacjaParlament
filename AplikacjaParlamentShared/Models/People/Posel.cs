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

namespace AplikacjaParlamentShared.Models
{
	public class Posel : IPerson, IPosel
	{
		public readonly static string NO_DATA_STRING = "Brak danych";

		[JsonProperty("poslowie.id")]
		private int _id;

		[JsonProperty("poslowie.imie_pierwsze")]
		private string _imie;

		[JsonProperty("poslowie.nazwisko")]
		private string _nazwisko;

		[JsonProperty("poslowie.biuro_html")]
		private string _biuraTabelka;

		[JsonProperty("poslowie.okreg_wyborczy_numer")]
		private int _okregWyborczyNumer;

		[JsonProperty("sejm_kluby.nazwa")]
		private string _sejmKlubyNazwa;

		[JsonProperty("poslowie.liczba_projektow_uchwal")]
		private int _liczbaProjektowUchwal;

		[JsonProperty("poslowie.liczba_projektow_ustaw")]
		private int _liczbaProjektowUstaw;

		[JsonProperty("poslowie.data_urodzenia")]
		private string _dataUrodzenia;

		[JsonProperty("poslowie.frekwencja")]
		private float _frekwencja;

		[JsonProperty("poslowie.mowca_id")]
		private int _mowca_id;

		public Posel () {}

		public int Id {
			get {
				return _id;
			}
		}

		public string Imie {
			get {
				return _imie ?? NO_DATA_STRING;
			}
		}

		public string Nazwisko {
			get {
				return _nazwisko ?? NO_DATA_STRING;
			}
		}

		public string BiuraTabelka {
			get {
				return _biuraTabelka ?? NO_DATA_STRING;			
			}
		}

		public int OkregWyborczyNumer {
			get {
				return _okregWyborczyNumer;		
			}
		}

		public string SejmKlubyNazwa {
			get {
				return _sejmKlubyNazwa;		
			}
		}

		public int LiczbaProjektowUchwal {
			get {
				return _liczbaProjektowUchwal;		
			}
		}

		public int LiczbaProjektowUstaw {
			get {
				return _liczbaProjektowUstaw;		
			}
		}

		public string DataUrodzenia {
			get {
				return _dataUrodzenia ?? NO_DATA_STRING;		
			}
		}

		public float Frekwencja {
			get {
				return _frekwencja;	
			}
		}

		public int MowcaId {
			get {
				return _mowca_id;
			}
		}

		public override string ToString ()
		{
			return string.Format ("[Posel: _id={0}, _imie={1}, _nazwisko={2}, _okregWyborczyNumer={3}, _sejmKlubyNazwa={4}, _liczbaProjektowUchwal={5}, _liczbaProjektowUstaw={6}, _dataUrodzenia={7}, _frekwencja={8}, _mowca_id={9}]", _id, _imie, _nazwisko, _okregWyborczyNumer, _sejmKlubyNazwa, _liczbaProjektowUchwal, _liczbaProjektowUstaw, _dataUrodzenia, _frekwencja, _mowca_id);
		}
		
		
	}
}

