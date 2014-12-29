//
//  VoteListAdapter.cs
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
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AplikacjaParlamentShared.Models;

namespace AplikacjaParlamentAndroid.Adapters
{
	public class VoteListAdapter : BaseAdapter<Vote>
	{

		private class Wrapper : Java.Lang.Object
		{
			public TextView tvData { get; set; }
			public TextView tvTytul { get; set; }
			public ImageView ivVote { get; set; }
		}

		private Activity context;
		private List<Vote> list; 

		public VoteListAdapter(Activity context, List<Vote> list)
		{
			this.context = context;
			this.list = list;
		}

		public override View GetView(int position, View convertView, ViewGroup parent)
		{
			Wrapper wrapper = null;
			var view = convertView;
			if (convertView == null)
			{
				view = context.LayoutInflater.Inflate(Resource.Layout.VoteListElement, null);
				wrapper = new Wrapper();
				wrapper.tvData = view.FindViewById<TextView>(Resource.Id.tvData);
				wrapper.tvTytul = view.FindViewById<TextView>(Resource.Id.tvTytul);
				wrapper.ivVote = view.FindViewById<ImageView> (Resource.Id.ivVote);
				view.Tag = wrapper;
			}
			else
			{
				wrapper = convertView.Tag as Wrapper;
			}

			var vote = list[position];
			wrapper.tvData.Text = vote.Data.Split (' ')[0].ToString ();
			wrapper.tvTytul.Text = vote.Tytul;

			if (vote.GlosId == 1)
				wrapper.ivVote.SetImageResource (Resource.Drawable.g_tak);

			return view;
		}

		public override Vote this[int position]
		{
			get { return list[position]; }
		}

		public override int Count
		{
			get { return list.Count; }
		}

		public override long GetItemId(int position)
		{
			return position;
		}
	}
}