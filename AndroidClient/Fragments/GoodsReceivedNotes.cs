using Android.OS;
using Android.Support.Design.Widget;
using Android.Support.V7.Widget;
using Android.Text;
using Android.Views;
using Android.Widget;
using Client.Adapters;
using Client.ViewHolders;
using Common;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using static Android.Support.V7.Widget.RecyclerView;

namespace Client.Fragments
{
    public class GoodsReceivedNotes : BaseListFragment
    {
        private GoodsReceivedNotesAdapter _adapter;

        public GoodsReceivedNotes() : base(
            SiteClaimValues.Notes.Add,
            Resource.String.GoodsReceivedNotesEmpty,
            Resource.String.SearchGoodsReceivedNotes
            )
        {
        }

        public override void OnItemsLoad(CancellationToken token)
        {
            _adapter = new GoodsReceivedNotesAdapter(Context, RoleManager);
            _adapter.IOnClickListener = this;

            ItemList.SetAdapter(_adapter);

            Task.Run(async () =>
            {
                await GetItems(token);
            }, token);
        }

        public override async Task GetItems(CancellationToken token)
        {
            var result = await NoteService.GetGoodsReceivedNotes(Criteria, token);

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
                NavigationManager.GoToAddGoodsReceivedNote();
            }
            if (view.Id == Resource.Id.GoodsReceivedNoteRowItemInfo)
            {
                var viewHolder = (GoodsReceivedNotesViewHolder)view.Tag;
                var item = _adapter.GetItem(viewHolder.AdapterPosition);
                NavigationManager.GoToGoodsReceivedNoteDetails(item);
            }
            if (view.Id == Resource.Id.GoodsReceivedNoteRowItemDelete)
            {
                var viewHolder = (GoodsReceivedNotesViewHolder)view.Tag;
                var item = _adapter.GetItem(viewHolder.AdapterPosition);

                Task.Run(async () =>
                {
                    var result = await NoteService.DeleteGoodsReceivedNote(item.Invoice.Id);

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