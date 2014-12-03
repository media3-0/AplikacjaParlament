//
//  DataObjectParser.cs
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
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace AplikacjaParlamentShared.Api
{
	public class DataObjectParser
	{
		static public T ParseJObjectToType<T>(JObject obj)
		{
			JObject data;
			try {
				data = obj.Value<JObject>("data"); 
			}catch (System.InvalidOperationException){
				// Wyjątek InvalidOperationException informuje mnie że element data nie został znaleziony w odpowiedzi json. Oznacza to że żądane dane nie istnieją w API (lub jest ich wewnętrzny błąd)
				throw new NoDataJsonElementException ();
			}
			return data.ToObject<T>();
		}

		static public List<T> ParseJArrayToList<T>(JArray array)
		{
			List<T> list = new List<T> (array.Count);
			try {
				foreach(JObject obj in array)
				{
					list.Add (obj.ToObject<T>());
				}
			}catch (System.InvalidOperationException){
				throw new NoDataJsonElementException ();
			}
			return list;
		}
	}
}

