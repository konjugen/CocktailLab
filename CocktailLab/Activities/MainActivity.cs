using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace CocktailLab
{
    [Activity(Label = "CocktailLab", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : BaseActivity
    {
        private ImageView _tasteImageView, _shakeImageView;

        protected override int LayoutResource
        {
            get { return Resource.Layout.main; }
        }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            _tasteImageView = (ImageView)FindViewById(Resource.Id.tasteImageView);
            _shakeImageView = (ImageView)FindViewById(Resource.Id.shakeImageView);

            _tasteImageView.Click += _tasteImageView_Click;
            _shakeImageView.Click += _shakeImageView_Click;

            SupportActionBar.SetDisplayHomeAsUpEnabled(false);
            SupportActionBar.SetHomeButtonEnabled(false);

        }

        private void _shakeImageView_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(ShakeCocktailActivity));
        }

        private void _tasteImageView_Click(object sender, EventArgs e)
        {
            const string text = "YAPIM AŞAMASINDA!";

            var toast = Toast.MakeText(ApplicationContext, text, ToastLength.Long);
            toast.Show();
        }
    }
}

