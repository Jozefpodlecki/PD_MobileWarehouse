
using Android.App;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Support.V7.Widget;
using Android.Text;
using Android.Views;
using Android.Widget;
using Client.Adapters;
using Client.Services;
using Common;
using Java.Lang;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Client.Fragments
{
    public class Counterparties : BaseFragment,
        View.IOnClickListener,
        ITextWatcher
    {
        public FloatingActionButton AddCounterpartyButton { get; set; }
        public AutoCompleteTextView SearchCounterparty { get; set; }
        public RecyclerView CounterpartyList { get; set; }
        public TextView EmptyCounterpartyView { get; set; }
        private CounterpartiesRowItemAdapter _adapter;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = inflater.Inflate(Resource.Layout.Counterparties, container, false);

            AddCounterpartyButton = view.FindViewById<FloatingActionButton>(Resource.Id.AddCounterpartyFloatActionButton);
            SearchCounterparty = view.FindViewById<AutoCompleteTextView>(Resource.Id.SearchCounterparty);
            CounterpartyList = view.FindViewById<RecyclerView>(Resource.Id.CounterpartyList);
            EmptyCounterpartyView = view.FindViewById<TextView>(Resource.Id.EmptyCounterpartyView);

            SearchCounterparty.AddTextChangedListener(this);
            AddCounterpartyButton.SetOnClickListener(this);
            
            LayoutManager = new LinearLayoutManager(Activity);
            LayoutManager.Orientation = LinearLayoutManager.Vertical;
            CounterpartyList.SetLayoutManager(LayoutManager);

            var items = new List<Models.Counterparty>();

            _adapter = new CounterpartiesRowItemAdapter(items, NoteService);

            //SearchCounterparty.Adapter = adapter;
            CounterpartyList.SetAdapter(_adapter);

            GetCounterparties();

            return view;
        }

        public void GetCounterparties()
        {
            var token = CancelAndSetTokenForView(CounterpartyList);

            var task = Task.Run(async () =>
            {
                var result = await NoteService.GetCounterparties(Criteria, token);

                Activity.RunOnUiThread(() =>
                {
                    if (result.Error != null)
                    {
                        ShowToastMessage("An error occurred");

                        return;
                    }

                    _adapter.UpdateList(result.Data);
                });
            });
        }

        public void OnClick(View view)
        {
            NavigationManager.GoToAddCounterparty();
        }

        public void AfterTextChanged(IEditable s)
        {
            Criteria.Name = s.ToString();

            GetCounterparties();
        }
        
    }
}