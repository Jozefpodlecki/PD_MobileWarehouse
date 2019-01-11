using System.Threading;
using System.Threading.Tasks;
using Android.OS;
using Android.Views;
using Android.Widget;

namespace Client.Fragments.Add
{
    public abstract class BaseAddFragment<T> : BaseFragment,
        View.IOnClickListener
        where T: Java.Lang.Object
    {
        public Button AddButton { get; set; }
        public T Entity { get; set; }

        public BaseAddFragment(int layoutId) : base(layoutId)
        {
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = base.OnCreateView(inflater, container, savedInstanceState);

            var includeLayout = view.FindViewById(Resource.Id.AddButtonLayout);
            AddButton = includeLayout.FindViewById<Button>(Resource.Id.AddButton);

            AddButton.SetOnClickListener(this);

            return view;
        }

        public void OnClick(View view)
        {
            if(view.Id == Resource.Id.AddButton)
            {
                AddButton.Enabled = false;

                if (!Validate())
                {
                    AddButton.Enabled = true;

                    return;
                }

                var token = CancelAndSetTokenForView(view);

                Task.Run(async () =>
                {
                    await OnAddButtonClick(token);
                }, token);

                return;
            }

            OnOtherButtonClick(view);
        }

        public virtual void OnOtherButtonClick(View view)
        {

        }

        public abstract bool Validate();
        public abstract Task OnAddButtonClick(CancellationToken token);
    }
}