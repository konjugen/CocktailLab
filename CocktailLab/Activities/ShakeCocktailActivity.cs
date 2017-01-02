using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Support.V4.App;
using Android.Util;
using CocktailLab.Activities;
using ElastiCoctail;
using Nest;
using quotation.Adapters;
using LayoutDirection = Android.Views.LayoutDirection;

namespace CocktailLab
{
    [Activity(Label = "Shake a Cocktail", ParentActivity = typeof(MainActivity))]
    [MetaData("android.support.PARENT_ACTIVITY", Value = ".MainActivity")]
    public class ShakeCocktailActivity : BaseActivity, Android.Hardware.ISensorEventListener
    {
        private SearchAdapter _searchAdapter;
        private AutoCompleteTextView _actv;
        private LinearLayout _mainLayout;
        private CheckedTextView _checkedTextView;
        private List<SearchResultModel> result = new List<SearchResultModel>();
        private List<SearchAdapter.SearchItem> checkedSearchItems = new List<SearchAdapter.SearchItem>();

        protected override int LayoutResource
        {
            get { return Resource.Layout.second; }
        }
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            _checkedTextView = (CheckedTextView)FindViewById(Resource.Id.checkedTextView);
            _mainLayout = (LinearLayout)FindViewById(Resource.Id.main_content);
            var button = (Button) FindViewById(Resource.Id.btnSearch);

            button.Click += btnSearch_Click;

            _actv = (AutoCompleteTextView)FindViewById(Resource.Id.autocomplete_search);

            _actv.Threshold = 1;

            var disp = WindowManager.DefaultDisplay;
            var widht = disp.Width;

            _actv.DropDownWidth = widht;
            var searchImage = GetDrawable(Resource.Drawable.search);
            searchImage.SetBounds(0, 0, 75, 75);
            _actv.SetCompoundDrawables(searchImage, null, null, null);

            _actv.ItemClick += actv_ItemClick;

            _searchAdapter = new SearchAdapter(this);
            var autoCompleteItems = new[] { "Light rum", "Applejack", "Gin", "Dark rum", "Sweet Vermouth", "Strawberry schnapps", "Scotch", "Apricot brandy", "Triple sec", "Southern Comfort", "Orange bitters", "Brandy", "Lemon vodka", "Blended whiskey", "Dry Vermouth", "Amaretto", "Tea", "Creme de Cacao", "Apple brandy", "Dubonnet Blanc", "Apple schnapps", "Añejo rum", "Champagne", "Coffee liqueur", "Rum", "Cachaca", "Sugar", "Blackberry brandy", "Calvados", "Ice", "Lemon", "Coffee brandy", "Bourbon", "Irish whiskey", "Vodka", "Tequila", "Bitters", "Lime juice", "Egg", "Mint", "Sherry", "Cherry brandy", "Canadian whisky", "Kahlua", "Yellow Chartreuse", "Cognac", "demerara Sugar", "Sake", "Dubonnet Rouge", "Anis", "White Creme de Menthe", "Gold tequila", "Sweet and sour", "Salt", "Galliano", "Green Creme de Menthe", "Kummel", "Anisette", "Carbonated water", "Lemon peel", "White wine", "Sloe gin", "Melon liqueur", "Swedish Punsch", "Peach brandy", "Passion fruit juice", "Peppermint schnapps", "Creme de Noyaux", "Grenadine", "Port", "Red wine", "Rye whiskey", "Grapefruit juice", "Ricard", "Banana liqueur", "Vanilla ice-cream", "Whiskey", "Creme de Banane", "Lime juice cordial", "Strawberry liqueur", "Sambuca", "Peach schnapps", "Apple juice", "Berries", "Blueberries", "Orange juice", "Pineapple juice", "Cranberries", "Brown sugar", "Milk", "Egg yolk", "Lemon juice", "Soda water", "Coconut liqueur", "Cream", "Pineapple", "Sugar syrup", "Ginger ale", "Worcestershire sauce", "Ginger", "Strawberries", "Chocolate syrup", "Yoghurt", "Grape juice", "Orange", "Apple cider", "Banana", "Mango", "Soy milk", "Lime", "Cantaloupe", "Grapes", "Kiwi", "Tomato juice", "Cocoa powder", "Chocolate", "Heavy cream", "Peach Vodka", "Ouzo", "Coffee", "Spiced rum", "Water", "Espresso", "Angelica root", "Condensed milk", "Honey", "Whipping cream", "Half-and-half", "Bread", "Plums", "Johnnie Walker", "Vanilla", "Apple", "Orange rum", "Everclear", "Kool-Aid", "Lemonade", "Cranberry juice", "Eggnog", "Carbonated soft drink", "Cloves", "Raisins", "Almond", "Beer", "Pink lemonade", "Sherbet", "Peach nectar", "Firewater", "Absolut Citron", "Malibu rum", "Midori melon liqueur", "151 proof rum", "Bacardi Limon", "Bailey's irish cream", "Lager", "Orange vodka", "Blue Curacao", "Absolut Vodka", "Jägermeister", "Jack Daniels", "Drambuie", "Whisky", "White rum", "Pisco", "Irish cream", "Yukon Jack", "Goldschlager", "Butterscotch schnapps", "Grand Marnier", "Peachtree schnapps", "Absolut Kurant", "Ale", "Chambord raspberry liqueur", "Tia maria", "Chocolate liqueur", "Frangelico", "Barenjager", "Hpnotiq", "Coca-Cola", "Tuaca", "Tang", "Tropicana", "Grain alcohol", "Schnapps", "Cider", "Aftershock", "Sprite", "Rumple Minze", "Key Largo schnapps", "Pisang Ambon", "Pernod", "7-Up", "Limeade", "Gold rum", "Wild Turkey", "Cointreau", "Lime vodka", "Maraschino cherry juice", "Creme de Cassis", "Zima", "Crown Royal", "Cardamom", "Orange Curacao", "Tabasco sauce", "Peach liqueur", "Curacao", "Cherry Heering", "Fruit punch", "Vermouth", "Cherry juice", "Cinnamon schnapps", "Orange peel", "Advocaat", "Clamato juice", "Sour mix", "Apfelkorn", "Green Chartreuse", "Root beer schnapps", "Coconut rum", "Raspberry schnapps", "Black Sambuca", "Vanilla vodka", "Root beer", "Absolut Peppar", "Vanilla schnapps", "Orange liqueur", "Kiwi liqueur", "Hot chocolate", "Jello", "Mountain Dew", "Blueberry schnapps", "Maui", "Tennessee whiskey", "White chocolate liqueur", "Cream of coconut", "Citrus vodka", "Fruit juice", "Cranberry vodka", "Campari", "Corona", "Chocolate ice-cream", "Jim Beam", "Aquavit", "Hawaiian Punch", "Blackberry schnapps", "Chocolate milk", "Watermelon schnapps", "Beef bouillon", "Dr. Pepper", "Iced tea", "Hot Damn", "Club soda", "Benedictine", "Dark Creme de Cacao", "Black rum", "Cherry Cola", "Absinthe", "Angostura bitters", "Tequila Rose", "Guinness stout", "Orange soda", "Wildberry schnapps", "Lemon-lime soda", "Godiva liqueur", "Baileys irish cream", "Schweppes Russchian", "Melon vodka", "Sour Apple Pucker", "Raspberry vodka", "coconut milk", "Ginger beer", "Light cream", "Powdered sugar", "Wine", "Maraschino liqueur", "Kirschwasser", "Passion fruit syrup", "Tonic water", "Maraschino cherry", "Tawny port", "Orgeat syrup", "Raspberry syrup", "White port", "Madeira", "Maple syrup", "Forbidden Fruit", "Egg white", "Carrot", "Blackberries", "Butter", "Bitter lemon", "Mint syrup", "Almond flavoring", "Allspice", "Papaya", "Fruit", "Cinnamon", "Coriander", "Wormwood", "Vanilla extract", "Corn syrup", "Coconut syrup", "Banana rum", "Ice-cream", "White grape juice", "Cherry liqueur", "Pineapple-orange-banana juice", "Raspberry cordial", "Lemon soda", "Celery salt", "Erin Cream", "Crystal light", "Margarita mix", "Fanta", "Blackcurrant cordial", "Sarsaparilla", "Cynar", "Purple passion", "Pineapple vodka", "Pina colada mix", "Surge", "", "Peychaud bitters", "Candy", "Strawberry juice", "Raspberry jam", "Grape soda", "Cranberry liqueur", "Nutmeg", "Cherries", "Peach juice", "Passoa", "Mezcal", "Cola", "Lime liqueur", "Mandarine Napoleon", "Hazelnut liqueur", "Pepsi Cola", "Sunny delight", "Raspberry liqueur", "Soy sauce", "Guava juice", "Whipped cream", "Olive", "Cherry", "Cocktail onion", "Papaya juice", "Cayenne pepper", "Clove", "Cumin seed", "Aperol", "Cornstarch", "Anise", "Apricot", "Yeast", "Daiquiri mix", "Cucumber", "Blackcurrant squash", "Hoopers Hooch", "Nuts", "Strawberry syrup", "Schweppes Lemon", "Food coloring", "Agave Syrup", "Black pepper", "Vanilla syrup", "Gatorade", "Orange spiral", "Lillet", "Peach", "Lime peel", "Asafoetida", "Sirup of roses", "Fennel seeds", "Licorice root", "Peppermint extract", "Glycerine", "Pineapple rum", "Squirt", "Celery", "Apricot nectar", "Blackcurrant schnapps", "Vanilla liqueur", "Banana syrup", "Raspberry juice", "Coconut cream", "Cream soda", "squeezed orange" };
            _searchAdapter.OriginalItems = autoCompleteItems.Select(s => new SearchAdapter.SearchItem() { Text = s }).ToArray();
            _actv.Adapter = _searchAdapter;

            // Register this as a listener with the underlying service.
            var sensorManager = GetSystemService(SensorService) as Android.Hardware.SensorManager;
            var sensor = sensorManager.GetDefaultSensor(Android.Hardware.SensorType.Accelerometer);
            sensorManager.RegisterListener(this, sensor, Android.Hardware.SensorDelay.Game);
        }

        #region Android.Hardware.ISensorEventListener implementation
        bool hasUpdated = false;
        DateTime lastUpdate;
        float last_x = 0.0f;
        float last_y = 0.0f;
        float last_z = 0.0f;

        const int ShakeDetectionTimeLapse = 250;
        const double ShakeThreshold = 800;

        public void OnAccuracyChanged(Android.Hardware.Sensor sensor, Android.Hardware.SensorStatus accuracy)
        {
        }

        public void OnSensorChanged(Android.Hardware.SensorEvent e)
        {
            if (e.Sensor.Type == Android.Hardware.SensorType.Accelerometer)
            {
                float x = e.Values[0];
                float y = e.Values[1];
                float z = e.Values[2];

                DateTime curTime = System.DateTime.Now;
                if (hasUpdated == false)
                {
                    hasUpdated = true;
                    lastUpdate = curTime;
                    last_x = x;
                    last_y = y;
                    last_z = z;
                }
                else
                {
                    if ((curTime - lastUpdate).TotalMilliseconds > ShakeDetectionTimeLapse)
                    {
                        float diffTime = (float)(curTime - lastUpdate).TotalMilliseconds;
                        lastUpdate = curTime;
                        float total = x + y + z - last_x - last_y - last_z;
                        float speed = Math.Abs(total) / diffTime * 10000;

                        if (speed > ShakeThreshold)
                        {
                            //Toast.MakeText(this, "shake detected w/ speed: " + speed, ToastLength.Short).Show();
                            Search();
                        }

                        last_x = x;
                        last_y = y;
                        last_z = z;
                    }
                }
            }
        }
#endregion

        private void Search()
        {
            var list = new List<string>();
            
            var intent = new Intent(ApplicationContext, typeof(CocktailsViewActivity));
            //var id = ((View)sender).Id;
            foreach (var item in checkedSearchItems.Where(q => q.Checked))
            {
                list.Add(item.Text);
                intent.PutStringArrayListExtra("selectedCocktailitems", list);
            }           
            StartActivity(intent);
        }

        private void btnSearch_Click(object sender, EventArgs eventArgs)
        {
            Search();
        }

        private void actv_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            var searchItem = _searchAdapter.OriginalItems[e.Position];
            ViewGroup.LayoutParams lparams = new ViewGroup.LayoutParams(
            ViewGroup.LayoutParams.WrapContent, ViewGroup.LayoutParams.WrapContent);
            CheckBox tv = new CheckBox(this);
            tv.LayoutParameters = lparams;
            tv.SetText(searchItem.Text, null);
            tv.SetTextSize(ComplexUnitType.Dip, 20);
            tv.SetPadding(50, 0, 0, 0);
            tv.Checked = true;
            tv.CheckedChange += TvOnCheckedChange;
            tv.Tag = searchItem;
            searchItem.Checked = true;
            
            _mainLayout.AddView(tv);
            _actv.Text = ""; 
            checkedSearchItems.Add(searchItem);       
        }

        private void TvOnCheckedChange(object sender, CompoundButton.CheckedChangeEventArgs checkedChangeEventArgs)
        {
            var checkBox = sender as CheckBox;
            var searchItem = checkBox?.Tag as SearchAdapter.SearchItem;
            if (searchItem != null)
            {
                searchItem.Checked = checkBox.Checked;
            }
            if (searchItem != null && !searchItem.Checked)
            {
                checkedSearchItems.Remove(searchItem);
            }
            else
            {
                checkedSearchItems.Add(searchItem);
            }          
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Android.Resource.Id.Home:
                    NavUtils.NavigateUpFromSameTask(this);
                    break;
            }

            return base.OnOptionsItemSelected(item);
        }
    }
}