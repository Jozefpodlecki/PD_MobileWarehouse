using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Support.V7.Widget;
using Android.Text;
using Android.Views;
using Android.Widget;
using Client;
using Client.Adapters;
using Client.Fragments;
using Client.Managers;
using Client.Services;
using Common;
using static Android.Support.V7.Widget.RecyclerView;

namespace AndroidClient.Fragments
{

    public class RecyclerViewOnScrollListener : OnScrollListener
    {

        public override void OnScrolled(RecyclerView recyclerView, int dx, int dy)
        {
            base.OnScrolled(recyclerView, dx, dy);
        }
    }

    public class Products : BaseFragment,
        View.IOnClickListener,
        ITextWatcher
    {
        public RecyclerView ProductListView { get; set; }
        public FloatingActionButton AddProductButton { get; set; }
        public AutoCompleteTextView SearchProduct { get; set; }
        public LayoutManager _layoutManager;
        private ProductAdapter _adapter;
        public TextView EmptyProductsView;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        public class TimerState
        {
            public int Counter { get; set; }
            public Timer Timer { get; set; }
        }

        public void RefreshList(object state)
        {
            var timerState = (TimerState)state;

            Activity.RunOnUiThread(() => {
                GetProducts();
            });
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = inflater.Inflate(Resource.Layout.Products, container, false);

            //var actionBar = Activity.SupportActionBar;
            //actionBar.Title = "Products";

            AddProductButton = view.FindViewById<FloatingActionButton>(Resource.Id.AddProductButton);
            AddProductButton.SetOnClickListener(this);

            ProductListView = view.FindViewById<RecyclerView>(Resource.Id.ProductList);
            //ProductListView.SetItemAnimator(this);
            ProductListView.AddOnScrollListener(new RecyclerViewOnScrollListener());

            SearchProduct = view.FindViewById<AutoCompleteTextView>(Resource.Id.SearchProduct);
            SearchProduct.AddTextChangedListener(this);

            EmptyProductsView = view.FindViewById<TextView>(Resource.Id.EmptyProductsView);

            var timerState = new TimerState();
            var timerCallback = new TimerCallback(RefreshList);

            var timer = new Timer(timerCallback, timerState, 10000, 20000);
            timerState.Timer = timer;

            _adapter = new ProductAdapter(Activity);
            _layoutManager = new LinearLayoutManager(Activity);
            ProductListView.SetLayoutManager(_layoutManager);
            ProductListView.SetAdapter(_adapter);

            GetProducts();
            
            return view;
        }

        public void OnClick(View v)
        {
            var navigationManager = new NavigationManager(Activity);
            navigationManager.GoToAddProduct();
            
        }

        public void GetProducts()
        {

            HttpResult<List<Client.Models.Product>> result = null;

            var task = Task.Run(async () =>
            {
                result = await ProductService.GetProducts(Criteria);
            });

            task.Wait();

            if (result.Error != null)
            {
                EmptyProductsView.Visibility = ViewStates.Visible;
                ProductListView.Visibility = ViewStates.Invisible;
            }
            else
            {
                _adapter.UpdateList(result.Data);
                ProductListView.SetAdapter(_adapter);

                EmptyProductsView.Visibility = ViewStates.Invisible;
                ProductListView.Visibility = ViewStates.Visible;
            }

        }

        public void AfterTextChanged(IEditable s)
        {
            var text = s.ToString();

            if (string.IsNullOrEmpty(text))
            {
                Criteria.Name = null;
            }
            else
            {
                Criteria.Name = text;
            }

            GetProducts();
        }
        
    }
}