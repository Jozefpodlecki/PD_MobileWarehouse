using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Android.Views;
using Client.Adapters;
using Client.ViewHolders;
using Common;

namespace Client.Fragments
{
    public class GoodsDispatchedNotes : BaseListFragment
    {
        private GoodsDispatchedNotesAdapter _adapter;

        public GoodsDispatchedNotes() : base(
            SiteClaimValues.Notes.Add,
            Resource.String.GoodsDispatchedNotesEmpty,
            Resource.String.SearchGoodsDispatchedNotes)
        {
        }

        public override void OnItemsLoad(CancellationToken token)
        {
            _adapter = new GoodsDispatchedNotesAdapter(Context, RoleManager);
            _adapter.IOnClickListener = this;

            ItemList.SetAdapter(_adapter);

            Task.Run(async () =>
            {
                await GetItems(token);
            }, token);
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
            if (view.Id == Resource.Id.GoodsDispatchedNoteRowItemInfo)
            {
                var viewHolder = (GoodsDispatchedNotesViewHolder)view.Tag;
                var item = _adapter.GetItem(viewHolder.AdapterPosition);
                NavigationManager.GoToGoodsDispatchedNoteDetails(item);
            }
            if (view.Id == Resource.Id.GoodsDispatchedNoteRowItemDelete)
            {
                var viewHolder = (GoodsDispatchedNotesViewHolder)view.Tag;
                var item = _adapter.GetItem(viewHolder.AdapterPosition);

                Task.Run(async () =>
                {
                    var result = await NoteService.DeleteGoodsDispatchedNote(item.InvoiceId);

                    if (result.Error.Any())
                    {
                        var message = result.Error.FirstOrDefault().Value.FirstOrDefault();
                        RunOnUiThread(() =>
                        {
                            ShowToastMessage(message);
                        });

                        return;
                    }

                    RunOnUiThread(() =>
                    {
                        _adapter.RemoveItem(item);
                    });
                });
            }
        }
    }
}