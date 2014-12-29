//
//  PoslowieNowosciContext.cs
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
using AplikacjaParlamentShared.Models;

namespace AplikacjaParlamentShared.Api
{
	public class PoslowieNowosciContext : Context
	{
		public PoslowieNowosciContext ()
		{
		}

		public override Object ParseJObject(){
			PoselNewest poselNewest = new PoselNewest ();

			JObject context = jsonData.Value<JArray> ("contexts").First as JObject;

			poselNewest.Action = context.Value<int> ("action");
			poselNewest.Sentence = context.Value<string> ("sentence");
			poselNewest.Data = jsonData.Value<JToken> ("data");

			return poselNewest;
		}
	}
}

