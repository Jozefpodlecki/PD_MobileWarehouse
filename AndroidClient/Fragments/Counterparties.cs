
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
    public class Counterparties : Fragment,
        View.IOnClickListener,
        ITextWatcher
    {
        public FloatingActionButton AddCounterpartyButton { get; set; }
        public AutoCompleteTextView SearchCounterparty { get; set; }
        public RecyclerView CounterpartyList { get; set; }
        public TextView EmptyCounterpartyView { get; set; }
        public new MainActivity Activity => (MainActivity)base.Activity;
        public FilterCriteria Criteria;
        private NoteService _service;
        private CounterpartiesRowItemAdapter _adapter;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = inflater.Inflate(Resource.Layout.Counterparties, container, false);

            AddCounterpartyButton = view.FindViewById<FloatingActionButton>(Resource.Id.AddInvoiceButton);
            SearchCounterparty = view.FindViewById<AutoCompleteTextView>(Resource.Id.SearchInvoice);
            CounterpartyList = view.FindViewById<RecyclerView>(Resource.Id.InvoicesList);
            EmptyCounterpartyView = view.FindViewById<TextView>(Resource.Id.EmptyInvoiceView);

            SearchCounterparty.AddTextChangedListener(this);
            AddCounterpartyButton.SetOnClickListener(this);

            Criteria = new FilterCriteria
            {
                Page = 0,
                ItemsPerPage = 5
            };

            _service = new NoteService(Activity);

            var items = new List<Common.DTO.Counterparty>();

            _adapter = new CounterpartiesRowItemAdapter(items);

            //SearchCounterparty.Adapter = adapter;
            CounterpartyList.SetAdapter(_adapter);

            GetCounterparties();

            return view;
        }

        public void GetCounterparties()
        {
            List<Common.DTO.Counterparty> items = null;

            var task = Task.Run(async () =>
            {
                items = await _service.GetCounterparties(Criteria);
            });

            task.Wait();

            _adapter.UpdateList(items);
        }

        public void OnClick(View view)
        {
            Activity.NavigationManager.GoToAddCounterparty();
        }

        public void AfterTextChanged(IEditable s)
        {
            Criteria.Name = s.ToString();

            GetCounterparties();
        }
        
        public void BeforeTextChanged(ICharSequence s, int start, int count, int after) { }
        public void OnTextChanged(ICharSequence s, int start, int before, int count) { }
    }
}