using Android.OS;
using Android.Views;
using Android.Widget;
using System.Threading;
using System.Threading.Tasks;

namespace Client.Fragments.Edit
{
    public abstract class BaseEditFragment<T> : BaseFragment,
        View.IOnClickListener
        where T : Java.Lang.Object
    {
        public Button SaveButton { get; set; }
        public T Entity { get; set; }

        public BaseEditFragment(int layoutId) : base(layoutId)
        {
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            Entity = (T)Arguments.GetParcelable(Constants.Entity);
            var view = base.OnCreateView(inflater, container, savedInstanceState);

            SaveButton = view.FindViewById<Button>(Resource.Id.SaveButton);

            SaveButton.SetOnClickListener(this);

            return view;
        }

        public void OnClick(View view)
        {
            if (view.Id == Resource.Id.AddButton)
            {
                SaveButton.Enabled = false;

                if (!Validate())
                {
                    SaveButton.Enabled = true;

                    return;
                }

                var token = CancelAndSetTokenForView(view);

                Task.Run(async () =>
                {
                    await OnSaveButtonClick(token);
                }, token);

                return;
            }

            OnOtherButtonClick(view);
        }

        public virtual void OnOtherButtonClick(View view)
        {

        }

        public abstract bool Validate();
        public abstract Task OnSaveButtonClick(CancellationToken token);
    }
}