//
//  BillsRepository.cs
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
using System.Threading.Tasks;
using System.Collections.Generic;
using System.IO;
using AplikacjaParlamentShared.Models;
using AplikacjaParlamentShared.Api;

namespace AplikacjaParlamentShared.Repositories
{
	public class BillsRepository : IBillsRepository
	{

		private static BillsRepository instance;

		public static BillsRepository Instance {
			get {
				return instance ?? (instance = new BillsRepository());
			}
		}

		public BillsRepository ()
		{
		}

		async public Task<List<ProjektAktuPrawnego>> GetProjektyAktowPrawnychList(){
			try {
				IJsonArrayRequestHandler<ProjektAktuPrawnego> handler = new JsonArrayRequestHandler<ProjektAktuPrawnego> (ConnectionProvider.Instance);

				var request = new RequestParamsHandler (String.Concat (RepositoriesContants.API_DATASET_URI, "prawo_projekty/search.json"));
				request.AddField ("prawo_projekty.id");
				request.AddField ("prawo_projekty.data_status");
				request.AddField ("prawo_projekty.status_str");
				request.AddField ("prawo_projekty.tytul");
				request.AddField ("prawo_projekty.autorzy_str");
				request.AddField ("prawo_projekty.opis_skrocony");
				request.Limit = 50;
				request.SetOrder ("prawo_projekty.data_status");

				List<ProjektAktuPrawnego> p = await handler.GetJsonArrayAsync (request);
				return p;

			} catch (IOException ex){
				System.Diagnostics.Debug.WriteLine("Java.IO.IOException on GetJsonArrayAsync", ex.ToString());
				throw new ApiRequestException (String.Concat("Problem z połączeniem:\n", ex.Message));

			} catch (Exception ex) {
				System.Diagnostics.Debug.WriteLine("GetJsonArrayAsync", ex.ToString());
				throw new ApiRequestException (String.Concat("Problem z dostępem do API:\n", ex.Message));
			}
		}
	}
}

