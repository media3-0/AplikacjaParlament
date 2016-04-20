﻿//
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
using System.IO;

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

				var request = new RequestParamsHandler (String.Concat (RepositoriesContants.API_BASE_URI, "poslowie/", id));
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
                request.AddField("poslowie.mowca_id");
				request.AddField ("ludzie.id");
				request.AddField ("poslowie.miejsce_zamieszkania");

				request.Layers.Add (new BiuraPoselskieLayer("biura"));

				Posel p = await handler.GetJsonObjectAsync (request);
				return p;
			} catch (IOException ex){
				System.Diagnostics.Debug.WriteLine("IOException on GetJsonObjectAsync", ex.ToString());
				throw new ApiRequestException (String.Concat("Problem z połączeniem:\n", ex.Message));

			} catch (Exception ex) {
				System.Diagnostics.Debug.WriteLine("GetJsonObjectAsync", ex.ToString());
				throw new ApiRequestException (String.Concat("Problem z dostępem do API:\n", ex.Message));
			}
		}

		async public Task<List<Posel>> GetPoselList ()
		{
			try {
				IJsonArrayRequestHandler<Posel> handler = new JsonArrayRequestHandler<Posel> (ConnectionProvider.Instance);

				var request = new RequestParamsHandler (String.Concat (RepositoriesContants.API_DATASET_URI, "poslowie.json"));
				request.AddCondition("poslowie.kadencja", "8");
				// request.AddCondition("poslowie.mandat_wygasl", "0");
				request.AddField ("poslowie.id");
				request.AddField ("poslowie.imie_pierwsze");
				request.AddField ("poslowie.nazwisko");
				request.AddField ("ludzie.id");
                request.AddField("poslowie.mowca_id");
                request.AddField ("sejm_kluby.nazwa");
				request.AddField ("poslowie.okreg_wyborczy_numer");
				request.Limit = 1000;
				request.SetOrder ("_title asc");

				List<Posel> p = await handler.GetJsonArrayAsync (request);
				return p;

			} catch (IOException ex){
				System.Diagnostics.Debug.WriteLine("IOException on GetJsonArrayAsync", ex.ToString());
				throw new ApiRequestException (String.Concat("Problem z połączeniem:\n", ex.Message));

			} catch (Exception ex) {
				System.Diagnostics.Debug.WriteLine("GetJsonArrayAsync", ex.ToString());
				throw new ApiRequestException (String.Concat("Problem z dostępem do API:\n", ex.Message));
			}
		}

		async public Task<List<Speech>> GetPoselSpeeches (int id)
		{
			try {
				IJsonArrayRequestHandler<Speech> handler = new JsonArrayRequestHandler<Speech> (ConnectionProvider.Instance);

				var request = new RequestParamsHandler (String.Concat (RepositoriesContants.API_DATASET_URI, "sejm_wystapienia.json"));
				request.AddCondition ("ludzie.posel_id", id.ToString ());
				request.AddField ("sejm_wystapienia.id");
				request.AddField ("sejm_debaty.tytul");
				request.AddField ("sejm_wystapienia.data");
				request.AddField ("sejm_wystapienia.skrot");
				request.Limit = 1000;
				request.SetOrder ("sejm_wystapienia.data asc");

				List<Speech> p = await handler.GetJsonArrayAsync (request);
				return p;

			} catch (IOException ex){
				System.Diagnostics.Debug.WriteLine("IOException on GetJsonArrayAsync", ex.ToString());
				throw new ApiRequestException (String.Concat("Problem z połączeniem:\n", ex.Message));

			} catch (Exception ex) {
				System.Diagnostics.Debug.WriteLine("GetJsonArrayAsync", ex.ToString());
				throw new ApiRequestException (String.Concat("Problem z dostępem do API:\n", ex.Message));
			}
		}

		async public Task<List<Vote>> GetPoselVotes (int id)
		{
			try {
				IJsonArrayRequestHandler<Vote> handler = new JsonArrayRequestHandler<Vote> (ConnectionProvider.Instance);

				var request = new RequestParamsHandler (String.Concat (RepositoriesContants.API_DATASET_URI, "poslowie_glosy.json"));
				request.AddCondition ("posel_id", id.ToString ());
				// Na chwilę obecną pobieramy tylko głosowania dotyczące tylko przyjęć całych projektów ustaw (pomijamy poprawki itd)
				request.AddCondition ("sejm_glosowania.typ_id", 26.ToString ());

				request.AddField ("poslowie_glosy.glosowanie_id");
				request.AddField ("sejm_glosowania.posiedzenie_id");
				request.AddField ("sejm_glosowania.tytul");
				request.AddField ("poslowie_glosy.glos_id");
				request.AddField ("sejm_glosowania.typ_id");
				request.AddField ("sejm_glosowania.czas");

				request.Limit = 1000;

				request.SetOrder ("sejm_glosowania.czas desc");

				List<Vote> p = await handler.GetJsonArrayAsync (request);
				return p;

			} catch (IOException ex){
				System.Diagnostics.Debug.WriteLine("IOException on GetJsonArrayAsync", ex.ToString());
				throw new ApiRequestException (String.Concat("Problem z połączeniem:\n", ex.Message));

			} catch (Exception ex) {
				System.Diagnostics.Debug.WriteLine("GetJsonArrayAsync", ex.ToString());
				throw new ApiRequestException (String.Concat("Problem z dostępem do API:\n", ex.Message));
			}
		}

		async public Task<List<Voting>> GetAllVotes ()
		{
			try {
				IJsonArrayRequestHandler<Voting> handler = new JsonArrayRequestHandler<Voting> (ConnectionProvider.Instance);

				var request = new RequestParamsHandler (String.Concat (RepositoriesContants.API_DATASET_URI, "sejm_glosowania.json"));

				request.AddField ("sejm_glosowania.id");
				request.AddField ("sejm_glosowania.wynik_id");
				request.AddField ("sejm_glosowania.tytul");
				request.AddField ("sejm_glosowania.czas");

				request.Limit = 1000;

				request.SetOrder ("sejm_glosowania.czas desc");

				List<Voting> p = await handler.GetJsonArrayAsync (request);
				return p;

			} catch (IOException ex){
				System.Diagnostics.Debug.WriteLine("IOException on GetJsonArrayAsync", ex.ToString());
				throw new ApiRequestException (String.Concat("Problem z połączeniem:\n", ex.Message));

			} catch (Exception ex) {
				System.Diagnostics.Debug.WriteLine("GetJsonArrayAsync", ex.ToString());
				throw new ApiRequestException (String.Concat("Problem z dostępem do API:\n", ex.Message));
			}
		}

		async public Task<List<Interpellation>> GetPoselInterpellations (int id)
		{
			try {
				IJsonArrayRequestHandler<Interpellation> handler = new JsonArrayRequestHandler<Interpellation> (ConnectionProvider.Instance);

				var request = new RequestParamsHandler (String.Concat (RepositoriesContants.API_DATASET_URI, "sejm_interpelacje.json"));
				request.AddCondition ("sejm_interpelacje.mowca_id", id.ToString ());
                request.AddField ("sejm_interpelacje.id");
                request.AddField ("sejm_interpelacje.tytul_skrocony");
                request.AddField ("sejm_interpelacje.data_wplywu");
                request.AddField ("sejm_interpelacje.adresaci_str");
				request.Limit = 1000;
				request.SetOrder ("sejm_interpelacje.data_wplywu desc");

				List<Interpellation> p = await handler.GetJsonArrayAsync (request);
				return p;

			} catch (IOException ex){
				System.Diagnostics.Debug.WriteLine("IOException on GetJsonArrayAsync", ex.ToString());
				throw new ApiRequestException (String.Concat("Problem z połączeniem:\n", ex.Message));

			} catch (Exception ex) {
				System.Diagnostics.Debug.WriteLine("GetJsonArrayAsync", ex.ToString());
				throw new ApiRequestException (String.Concat("Problem z dostępem do API:\n", ex.Message));
			}
		}

		async public Task<ISpeech> GetPoselSpeech(int id)
		{
			try {
				IJsonObjectRequestHandler<Speech> handler = new JsonObjectRequestHandler<Speech> (ConnectionProvider.Instance);

				var request = new RequestParamsHandler (String.Concat (RepositoriesContants.API_BASE_URI, "sejm_wystapienia/", id));
				request.AddField ("sejm_wystapienia.id");
				request.AddField ("sejm_debaty.tytul");
				request.AddField ("sejm_wystapienia.data");
				request.AddField ("sejm_wystapienia.skrot");

				request.Layers.Add (new TrescWystapieniaLayer("html"));

				Speech p = await handler.GetJsonObjectAsync (request);
				return p;
			} catch (IOException ex){
				System.Diagnostics.Debug.WriteLine("IOException on GetJsonObjectAsync", ex.ToString());
				throw new ApiRequestException (String.Concat("Problem z połączeniem:\n", ex.Message));

			} catch (Exception ex) {
				System.Diagnostics.Debug.WriteLine("GetJsonObjectAsync", ex.ToString());
				throw new ApiRequestException (String.Concat("Problem z dostępem do API:\n", ex.Message));
			}
		}

		async public Task<IInterpellation> GetPoselInterpellation(int id)
		{
			try {
				IJsonObjectRequestHandler<Interpellation> handler = new JsonObjectRequestHandler<Interpellation> (ConnectionProvider.Instance);

				var request = new RequestParamsHandler (String.Concat (RepositoriesContants.API_BASE_URI, "sejm_interpelacje/", id));
				request.AddField ("sejm_interpelacje.tytul");
				request.AddField ("sejm_interpelacje.data_wplywu");
				request.AddField ("sejm_interpelacje.adresaci_str");
				request.AddField ("sejm_interpelacje.tytul_skrocony");

				request.Layers.Add (new InterpelacjaLayer("dane"));

				Interpellation p = await handler.GetJsonObjectAsync (request);
				return p;
			} catch (IOException ex){
				System.Diagnostics.Debug.WriteLine("IOException on GetJsonObjectAsync", ex.ToString());
				throw new ApiRequestException (String.Concat("Problem z połączeniem:\n", ex.Message));

			} catch (Exception ex) {
				System.Diagnostics.Debug.WriteLine("GetJsonObjectAsync", ex.ToString());
				throw new ApiRequestException (String.Concat("Problem z dostępem do API:\n", ex.Message));
			}
		}

		async public Task<IVoting> GetSejmVoting(int id)
		{
			try {
				IJsonObjectRequestHandler<Voting> handler = new JsonObjectRequestHandler<Voting> (ConnectionProvider.Instance);

				var request = new RequestParamsHandler (String.Concat (RepositoriesContants.API_BASE_URI, "sejm_glosowania/", id));
				request.AddField ("sejm_glosowania.id");
				request.AddField ("sejm_glosowania.wynik_id");
				request.AddField ("sejm_glosowania.tytul");
				request.AddField ("sejm_glosowania.czas");

				request.Layers.Add (new SejmGlosowanieLayer("wynikiIndywidualne"));

				IVoting p = await handler.GetJsonObjectAsync (request);
				return p;
			} catch (IOException ex){
				System.Diagnostics.Debug.WriteLine("IOException on GetJsonObjectAsync", ex.ToString());
				throw new ApiRequestException (String.Concat("Problem z połączeniem:\n", ex.Message));

			} catch (Exception ex) {
				System.Diagnostics.Debug.WriteLine("GetJsonObjectAsync", ex.ToString());
				throw new ApiRequestException (String.Concat("Problem z dostępem do API:\n", ex.Message));
			}
		}

		async public Task<List<PoselNewest>> GetPoselNewest (int id)
		{
			try {
				IJsonArrayRequestHandler<PoselNewest> handler = new JsonArrayRequestHandler<PoselNewest> (ConnectionProvider.Instance);

				var request = new RequestParamsHandler (String.Concat (RepositoriesContants.API_BASE_URI, "poslowie/" + id + "/feed.json"));
				request.Limit = 40;
				request.Contexts.Add(new PoslowieNowosciContext());

				List<PoselNewest> p = await handler.GetJsonArrayAsync (request);
				return p;

			} catch (IOException ex){
				System.Diagnostics.Debug.WriteLine("IOException on GetJsonArrayAsync", ex.ToString());
				throw new ApiRequestException (String.Concat("Problem z połączeniem:\n", ex.Message));

			} catch (Exception ex) {
				System.Diagnostics.Debug.WriteLine("GetJsonArrayAsync", ex.ToString());
				throw new ApiRequestException (String.Concat("Problem z dostępem do API:\n", ex.Message));
			}
		}

        async public Task<List<PoselWspolpracownik>> GetPoselWspolpracownicy(int id) 
        {
            try {
                IJsonArrayRequestHandler<PoselWspolpracownik> handler = new JsonArrayRequestHandler<PoselWspolpracownik>(ConnectionProvider.Instance);

                var request = new RequestParamsHandler(String.Concat(RepositoriesContants.API_DATASET_URI, "poslowie_wspolpracownicy.json"));
                request.AddCondition("poslowie.id", id.ToString());
                request.AddField("poslowie_wspolpracownicy.data");
                request.AddField("poslowie_wspolpracownicy.nazwa");
                request.AddField("poslowie_wspolpracownicy.funkcja");
                request.AddField("poslowie_wspolpracownicy.dokument_id");
                request.Limit = 50;

                List<PoselWspolpracownik> p = await handler.GetJsonArrayAsync(request);
                return p;

            } catch (IOException ex) {
                System.Diagnostics.Debug.WriteLine("IOException on GetJsonArrayAsync", ex.ToString());
                throw new ApiRequestException(String.Concat("Problem z połączeniem:\n", ex.Message));

            } catch (Exception ex) {
                System.Diagnostics.Debug.WriteLine("GetJsonArrayAsync", ex.ToString());
                throw new ApiRequestException(String.Concat("Problem z dostępem do API:\n", ex.Message));
            }
        }

		async public Task<List<PoselOswiadczeniaMajatkowe>> GetPoselOswiadczeniaMajatkowe (int id)
		{
			try {
				IJsonArrayRequestHandler<PoselOswiadczeniaMajatkowe> handler = new JsonArrayRequestHandler<PoselOswiadczeniaMajatkowe> (ConnectionProvider.Instance);

				var request = new RequestParamsHandler (String.Concat (RepositoriesContants.API_DATASET_URI, "poslowie_oswiadczenia_majatkowe.json"));
				request.AddCondition("poslowie.id", id.ToString());
				request.AddField("poslowie_oswiadczenia_majatkowe.data");
				request.AddField("poslowie_oswiadczenia_majatkowe.label");
				request.AddField("poslowie_oswiadczenia_majatkowe.dokument_id");
				request.Limit = 50;

				List<PoselOswiadczeniaMajatkowe> p = await handler.GetJsonArrayAsync (request);
				return p;

			} catch (IOException ex){
				System.Diagnostics.Debug.WriteLine("IOException on GetJsonArrayAsync", ex.ToString());
				throw new ApiRequestException (String.Concat("Problem z połączeniem:\n", ex.Message));

			} catch (Exception ex) {
				System.Diagnostics.Debug.WriteLine("GetJsonArrayAsync", ex.ToString());
				throw new ApiRequestException (String.Concat("Problem z dostępem do API:\n", ex.Message));
			}
		}

		async public Task<List<PoselRejestrKorzysci>> GetPoselRejestrKorzysci (int id)
		{
			try {
				IJsonArrayRequestHandler<PoselRejestrKorzysci> handler = new JsonArrayRequestHandler<PoselRejestrKorzysci> (ConnectionProvider.Instance);

				var request = new RequestParamsHandler (String.Concat (RepositoriesContants.API_DATASET_URI, "poslowie_rejestr_korzysci.json"));
				request.AddCondition("poslowie.id", id.ToString());
				request.AddField("poslowie_rejestr_korzysci.data");
				request.AddField("poslowie_rejestr_korzysci.label");
				request.AddField("poslowie_rejestr_korzysci.dokument_id");
				request.Limit = 50;

				List<PoselRejestrKorzysci> p = await handler.GetJsonArrayAsync (request);
				return p;

			} catch (IOException ex){
				System.Diagnostics.Debug.WriteLine("IOException on GetJsonArrayAsync", ex.ToString());
				throw new ApiRequestException (String.Concat("Problem z połączeniem:\n", ex.Message));

			} catch (Exception ex) {
				System.Diagnostics.Debug.WriteLine("GetJsonArrayAsync", ex.ToString());
				throw new ApiRequestException (String.Concat("Problem z dostępem do API:\n", ex.Message));
			}
		}
	}
}

