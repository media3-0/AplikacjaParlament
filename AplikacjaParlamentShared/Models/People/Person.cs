//
//  Person.cs
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

namespace AplikacjaParlamentShared.Models
{
	/**
	 * Klasa bazowa reprezentująca każdą osobę w systemie (na późniejszym etapie będzie abstrakcyjna
	 */
	public class Person : IPerson // FIXME : zamienić na klasę abstrakcyjną po nadpisaniu wszystkich możliwości
	{
		public readonly static string NO_DATA_STRING = "Brak danych";

		private int _id;
		private string _imie;
		private string _nazwisko;
		private string _email;
		private string _stronaInternetowa;
		private string _telefon;

		public Person (int _id, string _imie, string _nazwisko, string _email, string _stronaInternetowa, string _telefon)
		{
			this._id = _id;
			this._imie = _imie;
			this._nazwisko = _nazwisko;
			this._email = _email;
			this._stronaInternetowa = _stronaInternetowa;
			this._telefon = _telefon;
		}
		

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

		public string Email {
			get {
				return _email ?? NO_DATA_STRING;
			}
		}

		public string StronaInternetowa {
			get {
				return _stronaInternetowa ?? NO_DATA_STRING;
			}
		}

		public string Telefon {
			get {
				return _telefon ?? NO_DATA_STRING;
			}
		}

		public override bool Equals (object obj)
		{
			if (obj == null)
				return false;
			if (ReferenceEquals (this, obj))
				return true;
			if (obj.GetType () != typeof(Person))
				return false;
			Person other = (Person)obj;
			return _id == other._id;
		}
		

		public override int GetHashCode ()
		{
			unchecked {
				return _id.GetHashCode ();
			}
		}

		public override string ToString ()
		{
			return string.Format ("[Person: _id={0}, _imie={1}, _nazwisko={2}, _email={3}, _stronaInternetowa={4}, _telefon={5}]", _id, _imie, _nazwisko, _email, _stronaInternetowa, _telefon);
		}
	}
}

