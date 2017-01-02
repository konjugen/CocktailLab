using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Media;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Support.V7.Widget;
using CocktailLab.Activities;
using CocktailLab.Holders;
using Elasticsearch.Net;
using ElastiCoctail;
using Java.IO;

namespace CocktailLab.Adapters
{
    public class CocktailAdapter : RecyclerView.Adapter
    {
        private CocktailsViewActivity cocktailViewActivity;
        private RecyclerView cocktailRecyclerView;
        private List<SearchResultModel> items = new List<SearchResultModel>();

        public CocktailAdapter(CocktailsViewActivity cocktailViewActivity, RecyclerView cocktailRecyclerView)
        {
            this.cocktailViewActivity = cocktailViewActivity;
            this.cocktailRecyclerView = cocktailRecyclerView;
        }

        public override int ItemCount
        {
            get { return items.Count; }
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            var ch = holder as CocktailViewHolder;

			var score = (int)Math.Ceiling(items[position].Score);

			ch.progressBar.Progress = score;
			ch.scoreText.Text = "5/" + score;

            ch.cocktailText.Text = items[position].Coctail.Title;
            var imageBitmap = GetImageBitmapFromUrl(items[position].Coctail.ImgUrl);
            imageBitmap = getRoundedShape(imageBitmap);
            ch.cocktailimage.SetImageBitmap(imageBitmap);
            ch.cocktailimage.SetScaleType(ImageView.ScaleType.FitStart);
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            var itemView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.CocktailCardView, parent, false);
            var ch = new CocktailViewHolder(itemView);

            return ch;
        }

        private Bitmap GetImageBitmapFromUrl(string url)
        {
            Bitmap imageBitmap = null;

            using (var webClient = new WebClient())
            {
                var imageBytes = webClient.DownloadData(url);
                if (imageBytes != null && imageBytes.Length > 0)
                {
                    imageBitmap = BitmapFactory.DecodeByteArray(imageBytes, 0, imageBytes.Length);
                }
            }

            return imageBitmap;
        }

        public Bitmap getRoundedShape(Bitmap scaleBitmapImage)
        {
            int targetWidth = 200;
            int targetHeight = 200;
            Bitmap targetBitmap = Bitmap.CreateBitmap(targetWidth,
                targetHeight, Bitmap.Config.Argb8888);

            Canvas canvas = new Canvas(targetBitmap);
            Android.Graphics.Path path = new Android.Graphics.Path();
            path.AddCircle(((float)targetWidth - 1) / 2,
                ((float)targetHeight - 1) / 2,
                (Math.Min(((float)targetWidth),
                    ((float)targetHeight)) / 2),
                Android.Graphics.Path.Direction.Ccw);

            canvas.ClipPath(path);
            Bitmap sourceBitmap = scaleBitmapImage;
            canvas.DrawBitmap(sourceBitmap,
                new Rect(0, 0, sourceBitmap.Width,
                    sourceBitmap.Height),
                new Rect(0, 0, targetWidth, targetHeight), null);
            return targetBitmap;
        }

        internal void Add(SearchResultModel current)
        {
            items.Add(current);
            NotifyDataSetChanged();
        }
    }
}