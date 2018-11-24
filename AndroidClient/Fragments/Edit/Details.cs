using Android.OS;
using Android.Text;
using Android.Views;
using Android.Widget;
using Java.Lang;

namespace Client.Fragments.Edit
{
    public class Details : BaseFragment,
        View.IOnClickListener,
        ITextWatcher
    {
        public ImageView EditDetailsAvatar { get; set; }
        public ImageButton EditDetailsSetAvatar { get; set; }
        public EditText EditEmail { get; set; }
        public EditText EditPassword { get; set; }
        public EditText EditPasswordConfirm { get; set; }
        public Button EditDetailsSaveButton { get; set; }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = inflater.Inflate(Resource.Layout.EditDetails, container, false);

            EditDetailsAvatar = view.FindViewById<ImageView>(Resource.Id.EditDetailsAvatar);
            EditDetailsSetAvatar = view.FindViewById<ImageButton>(Resource.Id.EditDetailsSetAvatar);
            EditEmail = view.FindViewById<EditText>(Resource.Id.EditEmail);
            EditPassword = view.FindViewById<EditText>(Resource.Id.EditPassword);
            EditPasswordConfirm = view.FindViewById<EditText>(Resource.Id.EditPasswordConfirm);
            EditDetailsSaveButton = view.FindViewById<Button>(Resource.Id.EditDetailsSaveButton);

            EditEmail.AddTextChangedListener(this);
            EditPassword.AddTextChangedListener(this);
            EditPasswordConfirm.AddTextChangedListener(this);
            EditDetailsSetAvatar.SetOnClickListener(this);
            EditDetailsSaveButton.SetOnClickListener(this);
             
            return view;
        }

        public void OnClick(View view)
        {
            if(view == EditDetailsSaveButton)
            {

            }
            if(view == EditDetailsSetAvatar)
            {

            }
        }

        public void AfterTextChanged(IEditable s)
        {
            
        }
        
    }
}