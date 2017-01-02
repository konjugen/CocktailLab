using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;

namespace CocktailLab.Holders
{
	public class CocktailViewHolder : RecyclerView.ViewHolder
	{
		public TextView cocktailText { get; set; }
		public ImageView cocktailimage { get; set; }
		public ProgressBar progressBar { get; set; }
		public TextView scoreText { get; set; }

		public CocktailViewHolder(View itemView) : base(itemView)
		{
			cocktailText = itemView.FindViewById<TextView>(Resource.Id.cocktailTextView);
			cocktailimage = itemView.FindViewById<ImageView>(Resource.Id.cocktailimageView);
			progressBar = itemView.FindViewById<ProgressBar>(Resource.Id.scoreBar);
			scoreText = itemView.FindViewById<TextView>(Resource.Id.scoreTextView);
		}
	}
}