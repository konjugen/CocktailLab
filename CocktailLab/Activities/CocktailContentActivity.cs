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

namespace CocktailLab.Activities
{
    [Activity(Label = "Materials")]
    [MetaData("android.support.PARENT_ACTIVITY", Value = ".MainActivity")]
    public class CocktailsContentActivity : BaseActivity
    {
        protected override int LayoutResource
        {
            get { return Resource.Layout.CocktailsContentView; }
        }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
        }
    }
}