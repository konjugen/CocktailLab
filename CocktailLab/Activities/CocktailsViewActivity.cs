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
using CocktailLab.Adapters;
using ElastiCoctail;
using Nest;
using quotation.Adapters;

namespace CocktailLab.Activities
{
    [Activity(Label = "Cocktails")]
    [MetaData("android.support.PARENT_ACTIVITY", Value = ".MainActivity")]
    public class CocktailsViewActivity : BaseActivity
    {
        private List<string> selectedItem = new List<string>();
        private readonly List<SearchResultModel> _result = new List<SearchResultModel>();
        private CocktailAdapter _adapter;
        private RecyclerView cocktailRecyclerView;

        protected override int LayoutResource
        {
            get { return Resource.Layout.CocktailsView; }
        }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            _adapter = new CocktailAdapter(this, FindViewById<RecyclerView>(Resource.Id.cocktailRecyclerView));

            cocktailRecyclerView = (RecyclerView)FindViewById(Resource.Id.cocktailRecyclerView);

            cocktailRecyclerView.SetLayoutManager(new LinearLayoutManager(this));

            cocktailRecyclerView.SetAdapter(_adapter);

            Search();

        }

        private void Search()
        {

            var item = Intent.Extras.GetStringArrayList("selectedCocktailitems");
            selectedItem = item.ToList();

            //Toast.MakeText(this.ApplicationContext, string.Join(",", checkedSearchItems), ToastLength.Short);

            var request = new SearchRequest
            {
                From = 0,
                Size = 10,
                Query = new TermsQuery { Field = "items", Terms = selectedItem }
            };

            var response = ElasticClientManager.Instance.Client.Search<CoctailModel>(request);

            foreach (var hit in response.Hits)
            {
                var model = new SearchResultModel()
                {
                    Coctail = hit.Source,
                    Score = hit.Score
                };

                _result.Add(model);
            }

            foreach (var current in _result)
                _adapter.Add(current);

            //string str = null;
            //foreach (var item in _result)
            //{
            //    str = item.Coctail.Title;
            //}

            //var toast = Toast.MakeText(ApplicationContext, str, ToastLength.Long);
            //toast.Show();
        }
    }
}