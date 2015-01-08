//
//  FileCache.cs
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
using System.Security.Cryptography;
using System.Text;
using System.IO;
using System.Net;
using System.Collections.Generic;

namespace AplikacjaParlamentAndroid
{
	public class FileCache
	{
		public static string SaveLocation = Android.OS.Environment.ExternalStorageDirectory.AbsolutePath;
		public static async Task<string> Download(string url)
		{
			if (string.IsNullOrEmpty (SaveLocation))
				throw new Exception ("Save location is required");
			var fileName = md5 (url);
			return await Download (url, fileName);
		}
		static object locker = new object ();
		public static async Task<string> Download(string url, string fileName)
		{
			try{
				var path = Path.Combine (SaveLocation, fileName);
				if (File.Exists (path))
					return path;
				await GetDownload(url,path);
				return path;
			}
			catch(Exception ex) {
				Android.Util.Log.Error ("CacheFileError", ex.Message);
				return "";
			}
		}
		static Dictionary<string,Task> downloadTasks = new Dictionary<string, Task> ();
		static Task GetDownload(string url, string fileName)
		{
			lock (locker) {
				Task task;
				if (downloadTasks.TryGetValue (fileName, out task))
					return task;
				var client = new WebClient ();
				downloadTasks.Add (fileName, task = client.DownloadFileTaskAsync (url, fileName));
				return task;
			}
		}
		static void removeTask(string fileName)
		{
			lock (locker) {
				downloadTasks.Remove (fileName);
			}
		}
		static MD5CryptoServiceProvider checksum = new MD5CryptoServiceProvider ();
		static int hex (int v)
		{
			if (v < 10)
				return '0' + v;
			return 'a' + v-10;
		}
		static string md5 (string input)
		{
			var bytes = checksum.ComputeHash (Encoding.UTF8.GetBytes (input));
			var ret = new char [32];
			for (int i = 0; i < 16; i++){
				ret [i*2] = (char)hex (bytes [i] >> 4);
				ret [i*2+1] = (char)hex (bytes [i] & 0xf);
			}
			return new string (ret);
		}
	}
}

