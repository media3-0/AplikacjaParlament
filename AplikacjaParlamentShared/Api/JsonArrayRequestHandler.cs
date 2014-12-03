//
//  JsonObjectRequestHandler.cs
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
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace AplikacjaParlamentShared.Api
{
	public class JsonArrayRequestHandler<T> : IJsonArrayRequestHandler<T>
	{
		private IConnectionProvider ConnectionProvider; 

		public JsonArrayRequestHandler (IConnectionProvider connectionProvider)
		{
			this.ConnectionProvider = connectionProvider;
		}

		public async Task<List<T>> GetJsonArrayAsync (RequestParamsHandler request)
		{
			var response = await ConnectionProvider.GetHttpClient().GetAsync(request.GetRequest ());

			response.EnsureSuccessStatusCode();

			string content = await response.Content.ReadAsStringAsync();
			JToken obj;
			bool hasValue = JObject.Parse (content).TryGetValue ("search", out obj);
			if (!hasValue) //jeżeli nie istnieje element search w odpowiedzi json
				throw new NoObjectJsonElementException ();
			if(obj.Type == JTokenType.Boolean) //jeżeli element search z odpowiedzi json ma typ boolean (nie muszę sprawdzać wartości, wystarczy wiedza na temat typu)
				throw new ObjectJsonBooleanElementException();

			JArray dataobjects = (JArray)obj ["dataobjects"];

			List<T> list = new List<T> (dataobjects.Count);
			foreach (var item in dataobjects) {
				list.Add(DataObjectParser.ParseJObjectToType<T>(item as JObject));
			}
			return list;
		}
	}
}

