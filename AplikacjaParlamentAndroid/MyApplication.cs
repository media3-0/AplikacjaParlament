//
//  MyApplication.cs
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
using Android.App;
using Android.Runtime;
using Xamarin;

namespace AplikacjaParlamentAndroid
{
	[Application]
	public class MyApplication : Application
	{
		public MyApplication(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
		{
		}

		public override void OnCreate()
		{
			base.OnCreate();

			// jeżeli typ buildu to Release to włącz system Xamarin.Insights
			#if ! DEBUG
			Insights.Initialize("9ab53be97f0603a11a92aba3d1532fb00259140c", Context);
			#endif
		}
	}
}
