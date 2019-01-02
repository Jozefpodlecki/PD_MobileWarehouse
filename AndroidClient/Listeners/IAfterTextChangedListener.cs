using Android.Widget;

namespace Client.Listeners
{
    public interface IAfterTextChangedListener
    {
        void AfterTextChanged(EditText view, string text);
    }
}