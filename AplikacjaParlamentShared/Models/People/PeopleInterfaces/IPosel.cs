//
//  IPosel.cs
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
	 * Interfejs tylko dla posłów
	 */
	public interface IPosel : IPerson
	{
		// tabela w HTML z danymi biur (telefony  i emaile)
		string BiuraTabelka { get; }
		// poslowie.okreg_wyborczy_numer
		int OkregWyborczyNumer { get; }
		// sejm_kluby.nazwa
		string SejmKlubyNazwa {get; }
		// poslowie.liczba_projektow_uchwal
		int LiczbaProjektowUchwal { get; }
		// poslowie.liczba_projektow_ustaw
		int LiczbaProjektowUstaw { get; }
		// poslowie.data_urodzenia
		string DataUrodzenia { get; }
		// poslowie.frekwencja
		float Frekwencja { get; }
		// poslowie.mowca_id (potrzebne do pobrania miniatury)
		int MowcaId { get; }
	}
}

