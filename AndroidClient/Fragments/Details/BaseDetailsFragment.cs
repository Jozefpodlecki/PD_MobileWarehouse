using Android.OS;
using Android.Views;
using Android.Widget;

namespace Client.Fragments.Details
{
    public abstract class BaseDetailsFragment<T> : BaseFragment
        where T : Java.Lang.Object
    {
        private TextView CreatedAt { get; set; }
        private TextView CreatedBy { get; set; }
        private TextView LastModifiedAt { get; set; }
        private TextView LastModifiedBy { get; set; }
        public T Entity { get; set; }

        public BaseDetailsFragment(int layoutId) : base(layoutId)
        {
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            Entity = (T)Arguments.GetParcelable(Constants.Entity);
            var view = base.OnCreateView(inflater, container, savedInstanceState);

            if (RoleManager.Claims.ContainsKey(Common.SiteClaimValues.SeeDetails))
            {
                var baseEntity = Entity as Models.BaseEntity;
                var baseDetailsLayout = view.FindViewById(Resource.Id.BaseDetailsLayout);
                baseDetailsLayout.Visibility = ViewStates.Visible;
                CreatedAt = baseDetailsLayout.FindViewById<TextView>(Resource.Id.CreatedAt);
                CreatedBy = baseDetailsLayout.FindViewById<TextView>(Resource.Id.CreatedBy);
                LastModifiedAt = baseDetailsLayout.FindViewById<TextView>(Resource.Id.LastModifiedAt);
                LastModifiedBy = baseDetailsLayout.FindViewById<TextView>(Resource.Id.LastModifiedBy);

                var systemUser = Resources.GetString(Resource.String.System);

                CreatedAt.Text = baseEntity.CreatedAt.ToString("U");
                CreatedBy.Text = baseEntity.CreatedBy?.ToString() ?? systemUser;
                LastModifiedAt.Text = baseEntity.LastModifiedAt.ToString("U");
                LastModifiedBy.Text = baseEntity.LastModifiedBy?.ToString() ?? systemUser;
            }
            
            return view;
        }
    }
}