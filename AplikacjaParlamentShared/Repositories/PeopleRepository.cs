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
using System.Collections.Generic;

namespace AplikacjaParlamentShared.Repositories
{
	public class PeopleRepository : IPeopleRepository
	{

		public const string API_BASE_URI = "http://api.mojepanstwo.pl/dane/";
		public const string API_DATASET_URI = API_BASE_URI + "dataset/";

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

				var request = new RequestParamsHandler (String.Concat (API_BASE_URI, "poslowie/", id));
				request.AddField ("poslowie.id");
				request.AddField ("poslowie.imie_pierwsze");
				request.AddField ("poslowie.nazwisko");
				request.AddField ("poslowie.biuro_html");
				request.AddField ("poslowie.okreg_wyborczy_numer");
				request.AddField ("sejm_kluby.nazwa");
				request.AddField ("poslowie.liczba_projektow_uchwal");
				request.AddField ("poslowie.liczba_projektow_ustaw");
				request.AddField ("poslowie.data_urodzenia");
				request.AddField ("poslowie.frekwencja");
				request.AddField ("poslowie.mowca_id");

				Posel p = await handler.GetJsonObjectAsync (request.GetRequest ());
				return p;
			} catch (Java.IO.IOException ex){
				Android.Util.Log.Error("Java.IO.IOException on GetJsonObjectAsync", ex.ToString());
				throw new ApiRequestException (String.Concat("Problem z połączeniem:\n", ex.Message));

			} catch (Exception ex) {
				Android.Util.Log.Error("GetJsonObjectAsync", ex.ToString());
				throw new ApiRequestException (String.Concat("Problem z dostępem do API:\n", ex.Message));
			}
		}

		async public Task<List<Posel>> GetPoselList ()
		{
			try {
				IJsonArrayRequestHandler<Posel> handler = new JsonArrayRequestHandler<Posel> (ConnectionProvider.Instance);

				var request = new RequestParamsHandler (String.Concat (API_DATASET_URI, "poslowie/search.json"));
				request.AddField ("poslowie.id");
				request.AddField ("poslowie.imie_pierwsze");
				request.AddField ("poslowie.nazwisko");
				request.AddField ("poslowie.mowca_id");
				request.AddField ("sejm_kluby.nazwa");
				request.Limit = 1000;
				request.SetOrder ("poslowie.nazwisko asc");

				List<Posel> p = await handler.GetJsonArrayAsync (request.GetRequest ());
				return p;

			} catch (Java.IO.IOException ex){
				Android.Util.Log.Error("Java.IO.IOException on GetJsonArrayAsync", ex.ToString());
				throw new ApiRequestException (String.Concat("Problem z połączeniem:\n", ex.Message));

			} catch (Exception ex) {
				Android.Util.Log.Error("GetJsonArrayAsync", ex.ToString());
				throw new ApiRequestException (String.Concat("Problem z dostępem do API:\n", ex.Message));
			}
		}

		async public Task<List<Speech>> GetPoselSpeeches (int id)
		{
			try {
				IJsonArrayRequestHandler<Speech> handler = new JsonArrayRequestHandler<Speech> (ConnectionProvider.Instance);

				var request = new RequestParamsHandler (String.Concat (API_DATASET_URI, "sejm_wystapienia/search.json"));
				request.AddCondition ("ludzie.id", id.ToString ());
//				request.AddField ("sejm_wystapienia.id");
//				request.AddField ("sejm_debaty.tytul");
//				request.AddField ("sejm_wystapienia.data");
//				request.AddField ("sejm_wystapienia.skrot");
				request.Limit = 1000;
				request.SetOrder ("sejm_wystapienia.data asc");

				List<Speech> p = await handler.GetJsonArrayAsync (request.GetRequest ());
				return p;

			} catch (Java.IO.IOException ex){
				Android.Util.Log.Error("Java.IO.IOException on GetJsonArrayAsync", ex.ToString());
				throw new ApiRequestException (String.Concat("Problem z połączeniem:\n", ex.Message));

			} catch (Exception ex) {
				Android.Util.Log.Error("GetJsonArrayAsync", ex.ToString());
				throw new ApiRequestException (String.Concat("Problem z dostępem do API:\n", ex.Message));
			}
		}
	}
}

