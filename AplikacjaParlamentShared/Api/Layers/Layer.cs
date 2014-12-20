//
//  Layer.cs
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

namespace AplikacjaParlamentShared.Api
{
	abstract public class Layer : ILayer
	{
		protected JToken content;
		protected JToken JsonObject;

		public string Label { get; set; }

		public Layer(string name){
			Label = name;
		}

		protected JToken GetLayer(string name){
			if (content == null) return null;
			JToken obj;
			bool hasValue = (content as JObject).TryGetValue ("layers", out obj);
			if (!hasValue) //jeżeli nie istnieje element layers w odpowiedzi json
				throw new NoLayersJsonElementException ();
			JToken lay;
			hasValue = (obj as JObject).TryGetValue (name, out lay);
			if (!hasValue) 
				throw new NoLayersJsonElementException ();
			if(lay.GetType() == typeof(JValue) && (lay as JValue).Value == null )
				throw new NoLayersJsonElementException ();
			return lay;
		}

		public void AssignContent(JToken token){
			this.content = token;
			this.JsonObject = GetLayer (Label);
		}

		public abstract void ParseJObject (Object obj);
	}
}

