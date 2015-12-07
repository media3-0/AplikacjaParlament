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
using System.Collections.Generic;

namespace AplikacjaParlamentShared.Models
{
	/**
	 * Interfejs tylko dla posłów
	 */
	public interface IPosel : IPerson
	{
		int OkregWyborczyNumer { get; set; }
		string SejmKlubyNazwa {get; set; }
		int LiczbaProjektowUchwal { get; set; }
		int LiczbaProjektowUstaw { get; set; }
		string DataUrodzenia { get; set; }
		float Frekwencja { get; set; }
		int MowcaId { get; set; }
		string Zawod { get; set; }
		string MiejsceZamieszkania { get; set; }

		List<BiuroPoselskie> Biura { get; set; }

		string GetWebURL();
	}
}

