//
//  RequestParamsHandler.cs
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
using System.Text;

namespace AplikacjaParlamentShared.Api
{
	public class RequestParamsHandler
	{

		private StringBuilder requestString;
		private bool firstParam = true;
		private string order = null;

		public RequestParamsHandler (string uri)
		{
			requestString = new StringBuilder ();
			requestString.Append (uri);
			Limit = 0;
		}

		public void AddField(string field)
		{
			requestString.Append (Delimiter ())
				.Append ("fields[]=")
				.Append (field);
		}

		public void SetOrder(string order)
		{
			this.order = order;
		}

		public int Limit {
			set; get;
		}

		private string Delimiter()
		{
			if (firstParam) {
				firstParam = false;
				return "?";
			}
			return "&";
		}

		public string GetRequest ()
		{
			StringBuilder finalRequest = new StringBuilder (requestString.ToString ());
			if(Limit > 0)
				finalRequest.Append (Delimiter ()).Append ("limit=").Append (Limit);
			if(order != null)
				finalRequest.Append (Delimiter ()).Append ("order=").Append (order);
			return finalRequest.ToString ();
		}
	}
}

