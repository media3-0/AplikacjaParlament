//
//  SejmListAdapter.cs
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
using Com.Androidquery;
using Com.Androidquery.Callback;
using Android.Graphics;

using AplikacjaParlamentAndroid.Helpers;

using Java.Lang;
using Object = Java.Lang.Object;

namespace AplikacjaParlamentAndroid.Adapters
{

	public class SejmListAdapter : BaseAdapter<Posel>, IFilterable, ISectionIndexer
	{

		private class Wrapper : Java.Lang.Object
		{
			public TextView ImieNazwisko { get; set; }
			public TextView Partia { get; set; }
			public ImageView Miniature { get; set; }
			public TextView Okreg { get; set; }
		}

		private Activity context;
		private List<Posel> list;
        private List<Posel> originalData;

        private Dictionary<string, int> alphaIndexer;
        private string[] sections;
        private Object[] sectionsObjects;

        public Filter Filter { get; private set; }

		public SejmListAdapter(Activity context, List<Posel> list)
		{
			this.context = context;
			this.list = list;

            this.list.Sort(delegate(Posel p1, Posel p2) {
                if (p1.Nazwisko == null && p2.Nazwisko == null) return 0;
                else if (p1.Nazwisko == null) return -1;
                else if (p2.Nazwisko == null) return 1;
                else return p1.Nazwisko.CompareTo(p2.Nazwisko);
            });

            Filter = new SejmListFilter(this);

            alphaIndexer = new Dictionary<string, int>();

            int size = list.Count;

            for (int x = 0; x < size; x++) {
                var posel = list[x];
                string ch = posel.Nazwisko.Substring(0, 1);
                ch = ch.ToUpper();
                if(!alphaIndexer.ContainsKey(ch))
                    alphaIndexer.Add(ch, x);
            }

            sections = new string[alphaIndexer.Keys.Count];

            alphaIndexer.Keys.CopyTo(sections, 0);

            sectionsObjects = new Object[sections.Length];
            for (int i = 0; i < sections.Length; i++) {
                sectionsObjects[i] = new String(sections[i]);
            }
		}

		public override View GetView(int position, View convertView, ViewGroup parent)
		{
			Wrapper wrapper = null;
			var view = convertView;
			if (convertView == null)
			{
				view = context.LayoutInflater.Inflate(Resource.Layout.SejmListElement, null);
				wrapper = new Wrapper();
				wrapper.ImieNazwisko = view.FindViewById<TextView>(Resource.Id.imieNazwisko);
				wrapper.Partia = view.FindViewById<TextView>(Resource.Id.partia);
				wrapper.Miniature = view.FindViewById<ImageView> (Resource.Id.miniature);
				wrapper.Okreg = view.FindViewById<TextView> (Resource.Id.okreg);
				view.Tag = wrapper;
			}
			else
			{
				wrapper = convertView.Tag as Wrapper;
			}

			var posel = list[position];
			wrapper.ImieNazwisko.Text = System.String.Concat(posel.Imie, " ", posel.Nazwisko);
			wrapper.Partia.Text = posel.SejmKlubyNazwa;
			wrapper.Okreg.Text = posel.OkregWyborczyNumer.ToString();

			string imgUrl = posel.GetWebURL ();
            //wrapper.Miniature.SetImageResource (Android.Resource.Drawable.IcMenuGallery);
            //loadImage (wrapper, );

            try {

			    AQuery aq = new AQuery(view);

			    Bitmap imgLoading = aq.GetCachedImage(Android.Resource.Drawable.IcMenuGallery);

			    if (aq.ShouldDelay(position, convertView, parent, imgUrl))
			    {
				    ((AQuery)aq.Id(Resource.Id.miniature)).Image(imgLoading, 1f);
			    }
			    else
			    {
				    ((AQuery)aq.Id(Resource.Id.miniature)).Image(imgUrl, true, true, 0, 0, imgLoading, 0, 1f);
			    }
            } catch (Exception exc){
				//raportowanie błędów przy używaniu AQuery
				Xamarin.Insights.Report (exc); 
			}

			return view;
		}

		async private void loadImage(Wrapper wrapper, string url){
			await ImagesHelper.SetImageFromUrlAsync(wrapper.Miniature,url, context);
		}

		public override Posel this[int position]
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

        public int GetPositionForSection(int section) {
            return alphaIndexer[sections[section]];
        }

        public Object[] GetSections()
        {
            return sectionsObjects;
        }

        public int GetSectionForPosition(int position) {
            return 1;
        }

        private class SejmListFilter : Filter
        {
            private readonly SejmListAdapter adapter;

            public SejmListFilter(SejmListAdapter sejmListAdapter) {
                adapter = sejmListAdapter;
            }

            protected override Filter.FilterResults PerformFiltering(Java.Lang.ICharSequence constraint) {
                var returnObj = new FilterResults();
                var results = new List<Posel>();
                if (adapter.originalData == null)
                    adapter.originalData = adapter.list;

                if (constraint == null) return returnObj;

                if (adapter.originalData != null && adapter.originalData.Any()) {
                    // Compare constraint to all names lowercased. 
                    // It they are contained they are added to results.
                    results.AddRange(
                        adapter.originalData.Where(
                            posel => System.String.Concat(posel.Imie.ToLower(), " ", posel.Nazwisko.ToLower()).Contains(constraint.ToString())));
                }

                // Nasty piece of .NET to Java wrapping, be careful with this!
                returnObj.Values = FromArray(results.Select(r => r.ToJavaObject()).ToArray());
                returnObj.Count = results.Count;

                constraint.Dispose();

                return returnObj;
            }

            protected override void PublishResults(ICharSequence constraint, FilterResults results) {
                using (var values = results.Values)
                    adapter.list = values.ToArray<Object>()
                        .Select(r => r.ToNetObject<Posel>()).ToList();

                adapter.NotifyDataSetChanged();

                // Don't do this and see GREF counts rising
                constraint.Dispose();
                results.Dispose();
            }
        }
	}
}