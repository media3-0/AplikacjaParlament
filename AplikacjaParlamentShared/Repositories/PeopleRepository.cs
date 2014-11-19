//
//  PeopleRepository.cs
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

using AplikacjaParlamentShared.Models;
using AplikacjaParlamentShared.Api;
using System.Threading.Tasks;

namespace AplikacjaParlamentShared.Repositories
{
	public class PeopleRepository : IPeopleRepository
	{

		private static PeopleRepository instance;

		public static PeopleRepository Instance {
			get {
				return instance ?? (instance = new PeopleRepository());
			}
		}

		private PeopleRepository ()
		{
		}

		async public Task<IPosel> GetPosel (int id)
		{
			try {
				IJsonObjectRequestHandler<Posel> handler = new JsonObjectRequestHandler<Posel> (ConnectionProvider.Instance);
				Posel p = await handler.GetJsonObjectAsync (String.Concat ("http://api.mojepanstwo.pl/dane/poslowie/", id));
				return p;
			} catch (Java.IO.IOException ex){
				Android.Util.Log.Error("Java.IO.IOException on GetJsonObjectAsync", ex.ToString());
				throw new ApiRequestException (String.Concat("Problem z połączeniem:\n", ex.Message));

			} catch (Exception ex) {
				Android.Util.Log.Error("GetJsonObjectAsync", ex.ToString());
				throw new ApiRequestException (String.Concat("Problem z dostępem do API:\n", ex.Message));
			}
		}
	}
}

