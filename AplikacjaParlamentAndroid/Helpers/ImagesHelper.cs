//
//  Images.cs
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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Threading.Tasks;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Content.Res;
using Android.Widget;
using Android.Content;

namespace AplikacjaParlamentAndroid
{

	public interface IBitmapHolder
	{
		void SetImageBitmap (Bitmap bmp);
	}

	static public class ImagesHelper
	{
		//public static float ScreenWidth = 320;
		static Dictionary<string, Bitmap> bmpCache = new Dictionary<string, Bitmap> ();
		public static async Task SetImageFromUrlAsync (this ImageView imageView, string url, Context context)
		{
			var bmp = FromUrl(url);
			if (bmp == null)
				return;
			if (bmp.IsCompleted)
				imageView.SetImageBitmap (GetRoundedShape(bmp.Result, context));
			else
				imageView.SetImageBitmap (GetRoundedShape(await bmp, context));
		}
		public static async Task SetImageFromUrlAsync (IBitmapHolder imageView, string url)
		{
			var bmp = FromUrl(url);
			if (bmp.IsCompleted)
				imageView.SetImageBitmap (bmp.Result);
			else
				imageView.SetImageBitmap (await bmp);
		}
		public static async Task<Bitmap> FromUrl (string url)
		{
			Bitmap bmp;
			if (bmpCache.TryGetValue (url, out bmp))
				return bmp;
			var path = await FileCache.Download(url);
			if (string.IsNullOrEmpty (path))
				return null;
			bmp = await BitmapFactory.DecodeFileAsync (path);
			bmpCache [url] = bmp;
			return bmp;
		}

		public static Bitmap GetRoundedShape(Bitmap scaleBitmapImage, Context context) {
			if(scaleBitmapImage == null) 
				return BitmapFactory.DecodeResource(context.Resources, Android.Resource.Drawable.IcMenuGallery);
			int targetWidth = 220;
			int targetHeight = 284;
			Bitmap targetBitmap = Bitmap.CreateBitmap(targetWidth, 
				targetHeight,Bitmap.Config.Argb8888);

			Canvas canvas = new Canvas(targetBitmap);
			Android.Graphics.Path path = new Android.Graphics.Path();
			path.AddCircle(((float) targetWidth - 1) / 2,
				((float) targetHeight - 1) / 2 - 20,
				(Math.Min(((float) targetWidth), 
					((float) targetHeight)) / 2),
				Android.Graphics.Path.Direction.Ccw);

			canvas.ClipPath(path);
			Bitmap sourceBitmap = scaleBitmapImage;
			canvas.DrawBitmap(sourceBitmap, 
				new Rect(0, 0, sourceBitmap.Width,
					sourceBitmap.Height), 
				new Rect(0, 0, targetWidth, targetHeight), null);
			return targetBitmap;
		}
	}
}

