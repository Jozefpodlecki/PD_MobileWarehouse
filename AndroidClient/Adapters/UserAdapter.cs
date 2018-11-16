using Android.App;
using Android.Support.V7.Widget;
using Android.Views;
using AndroidClient.Models;
using Client.ViewHolders;
using System.Collections.Generic;

namespace Client.Adapters
{
    public class UserAdapter : RecyclerView.Adapter
    {
        private List<Common.DTO.User> _users;
        private Activity _activity;

        public UserAdapter(
            Activity activity,
            List<Common.DTO.User> users
            )
        {
            _activity = activity;
            _users = users;
        }

        public void UpdateList(List<Common.DTO.User> users)
        {
            _users = users;
            NotifyDataSetChanged();
        }

        public override int ItemCount => _users.Count;

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            var userAdapterViewHolder = holder as UserAdapterViewHolder;

            var user = _users[position];

            userAdapterViewHolder.UserRowItemName.Text = user.Username;
            userAdapterViewHolder.UserRowItemRole.Text = user.Role;
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            var itemView = LayoutInflater.From(parent.Context)
                .Inflate(Resource.Layout.UserRowItem, parent, false);

            return new UserAdapterViewHolder(itemView);
        }
    }
}