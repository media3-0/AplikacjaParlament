//
//  LoadingOverlay.cs
//
//  Author:
//       Jakub Syty <j.syty@media30.pl>
//
//  Copyright (c) 2015 Fundacja Media 3.0
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
using UIKit;
using CoreGraphics;
using System;

public class LoadingOverlay : UIView {
	// control declarations
	UIActivityIndicatorView activitySpinner;
	UILabel loadingLabel;

	public LoadingOverlay (CGRect frame) : base (frame)
	{
		// configurable bits
		BackgroundColor = UIColor.Black;
		Alpha = 0.75f;
		AutoresizingMask = UIViewAutoresizing.All;

		nfloat labelHeight = 22;
		nfloat labelWidth = Frame.Width - 20;

		// derive the center x and y
		nfloat centerX = Frame.Width / 2;
		nfloat centerY = Frame.Height / 2;

		// create the activity spinner, center it horizontall and put it 5 points above center x
		activitySpinner = new UIActivityIndicatorView(UIActivityIndicatorViewStyle.WhiteLarge);
		activitySpinner.Frame = new CGRect ( 
			centerX - (activitySpinner.Frame.Width / 2) ,
			centerY - activitySpinner.Frame.Height - 20 ,
			activitySpinner.Frame.Width,
			activitySpinner.Frame.Height);
		activitySpinner.AutoresizingMask = UIViewAutoresizing.All;
		AddSubview (activitySpinner);
		activitySpinner.StartAnimating ();

		// create and configure the "Loading Data" label
		loadingLabel = new UILabel(new CGRect (
			centerX - (labelWidth / 2),
			centerY + 20 ,
			labelWidth ,
			labelHeight
		));
		loadingLabel.BackgroundColor = UIColor.Clear;
		loadingLabel.TextColor = UIColor.White;
		loadingLabel.Text = "Ładowanie danych...";
		loadingLabel.TextAlignment = UITextAlignment.Center;
		loadingLabel.AutoresizingMask = UIViewAutoresizing.All;
		AddSubview (loadingLabel);

	}

	/// <summary>
	/// Fades out the control and then removes it from the super view
	/// </summary>
	public void Hide ()
	{
		UIView.Animate ( 
			0.5, // duration
			() => { Alpha = 0; }, 
			() => { RemoveFromSuperview(); }
		);
	}
}