using Android.OS;
using Android.Support.Design.Widget;
using Android.Support.V7.Widget;
using Android.Text;
using Android.Views;
using Android.Views.Animations;
using Android.Widget;
using System.Threading;
using System.Threading.Tasks;

namespace Client.Fragments
{
    public abstract class BaseListFragment : BaseFragment,
        View.IOnClickListener,
        ITextWatcher
    {
        public FloatingActionButton AddItemFloatActionButton { get; set; }
        public AutoCompleteTextView SearchItem { get; set; }
        public RecyclerView ItemList { get; set; }
        public TextView EmptyItemsView { get; set; }
        public ProgressBar LoadingItemsProgressBar { get; set; }
        private string _addItemClaim;
        protected ViewStates? AddItemViewState;
        private int _emptyItemsResourceStringId;
        private int _searchItemsResourceStringId;

        public BaseListFragment(
            int emptyItemsResourceStringId,
            int searchItemsResourceStringId
            ) : base(Resource.Layout.List)
        {
            AddItemViewState = ViewStates.Gone;
            _emptyItemsResourceStringId = emptyItemsResourceStringId;
            _searchItemsResourceStringId = searchItemsResourceStringId;
        }

        public BaseListFragment(
            string addItemClaim,
            int emptyItemsResourceStringId,
            int searchItemsResourceStringId
            ) : base(Resource.Layout.List)
        {
            _addItemClaim = addItemClaim;
            _emptyItemsResourceStringId = emptyItemsResourceStringId;
            _searchItemsResourceStringId = searchItemsResourceStringId;
        }

        public override void OnBindElements(View view)
        {
            AddItemFloatActionButton = view.FindViewById<FloatingActionButton>(Resource.Id.AddItemFloatActionButton);
            SearchItem = view.FindViewById<AutoCompleteTextView>(Resource.Id.SearchItem);
            ItemList = view.FindViewById<RecyclerView>(Resource.Id.ItemList);
            EmptyItemsView = view.FindViewById<TextView>(Resource.Id.EmptyItemsView);
            LoadingItemsProgressBar = view.FindViewById<ProgressBar>(Resource.Id.LoadingItemsProgressBar);
            EmptyItemsView.Text = Resources.GetString(_emptyItemsResourceStringId);
            SearchItem.Hint = Resources.GetString(_searchItemsResourceStringId);
            AddItemFloatActionButton.SetOnClickListener(this);
            SearchItem.AddTextChangedListener(this);

            if (!AddItemViewState.HasValue)
            {
                AddItemViewState = RoleManager
                    .Claims
                    .ContainsKey(_addItemClaim) ? ViewStates.Visible : ViewStates.Gone;
            }

            AddItemFloatActionButton.Visibility = AddItemViewState.Value;

            var layoutManager = new LinearLayoutManager(Context);
            layoutManager.Orientation = LinearLayoutManager.Vertical;
            ItemList.SetLayoutManager(layoutManager);
            var animationController = AnimationUtils.LoadLayoutAnimation(Context, Resource.Animation.layout_animation_fall_down);
            ItemList.LayoutAnimation = animationController;
            ItemList.ScheduleLayoutAnimation();
        }

        public void SetLoadingContent()
        {
            ItemList.Visibility = ViewStates.Gone;
            EmptyItemsView.Visibility = ViewStates.Gone;
            LoadingItemsProgressBar.Visibility = ViewStates.Visible;
        }

        public void SetEmptyContent()
        {
            ItemList.Visibility = ViewStates.Gone;
            EmptyItemsView.Visibility = ViewStates.Visible;
            LoadingItemsProgressBar.Visibility = ViewStates.Gone;
        }

        public void SetContent()
        {
            ItemList.Visibility = ViewStates.Visible;
            EmptyItemsView.Visibility = ViewStates.Gone;
            LoadingItemsProgressBar.Visibility = ViewStates.Gone;
        }

        public virtual void AfterTextChanged(IEditable text)
        {
            SetLoadingContent();

            Criteria.Name = text.ToString();

            var token = CancelAndSetTokenForView(ItemList);

            Task.Run(async () =>
            {
                await GetItems(token);
            }, token);
        }

        public abstract Task GetItems(CancellationToken token);

        public abstract void OnClick(View view);
    }
}