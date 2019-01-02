using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Text;
using Android.Views;
using Client.Adapters;
using Common;

namespace Client.Fragments
{
    public class GoodsDispatchedNotes : BaseListFragment
    {
        private GoodsDispatchedNotesAdapter _adapter;

        public GoodsDispatchedNotes() : base(
            PolicyTypes.Notes.Add,
            Resource.String.GoodsDispatchedNotesEmpty,
            Resource.String.SearchGoodsDispatchedNotes)
        {
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = base.OnCreateView(inflater, container, savedInstanceState);

            var token = CancelAndSetTokenForView(ItemList);

            SetLoadingContent();

            _adapter = new GoodsDispatchedNotesAdapter(Context, RoleManager);
            _adapter.IOnClickListener = this;

            ItemList.SetAdapter(_adapter);

            Task.Run(async () =>
            {
                await GetItems(token);
            }, token);

            return view;
        }

        public override async Task GetItems(CancellationToken token)
        {
            var result = await NoteService.GetGoodsDispatchedNotes(Criteria, token);

            RunOnUiThread(() =>
            {
                if (result.Error.Any())
                {
                    ShowToastMessage(Resource.String.ErrorOccurred);

                    return;
                }

                _adapter.UpdateList(result.Data);

                if (result.Data.Any())
                {
                    SetContent();

                    return;
                }

                SetEmptyContent();
            });
        }

        public override void OnClick(View view)
        {
            if (view.Id == AddItemFloatActionButton.Id)
            {
                NavigationManager.GoToAddGoodsDispatchedNote();
            }
        }
    }
}