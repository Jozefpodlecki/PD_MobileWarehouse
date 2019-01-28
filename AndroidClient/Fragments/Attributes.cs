using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Client.Adapters;
using Client.Services;
using Client.ViewHolders;
using Common;

namespace Client.Fragments
{
    public class Attributes : BaseListFragment
    {
        private AttributesRowItemAdapter _adapter;

        public Attributes() : base(
            SiteClaimValues.Attributes.Add,
            Resource.String.NoAttributesAvailable,
            Resource.String.SearchAttributes)
        {
        }

        public override void OnItemsLoad(CancellationToken token)
        {
            _adapter = new AttributesRowItemAdapter(Context, RoleManager);
            _adapter.IOnClickListener = this;

            ItemList.SetAdapter(_adapter);

            Task.Run(async () =>
            {
                await GetItems(token);
            }, token);
        }

        public override async Task GetItems(CancellationToken token)
        {
            var result = await AttributeService.GetAttributes(Criteria, token);

            if (!CheckForAuthorizationErrors(result.Error)) return;

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
            var viewHolder = view.Tag as AttributesRowItemViewHolder;

            if (view.Id == Resource.Id.AttributesRowItemEdit)
            {
                var item = _adapter.GetItem(viewHolder.AdapterPosition);
                NavigationManager.GoToAttributeEdit(item);
            }
            if (view.Id == Resource.Id.AttributesRowItemDelete)
            {
                var item = _adapter.GetItem(viewHolder.AdapterPosition);

                Task.Run(async () =>
                {
                    var result = await AttributeService.DeleteAttribute(item.Id);

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
            if (view.Id == AddItemFloatActionButton.Id)
            {
                NavigationManager.GoToAddAttribute();
            }
        }
    }
}