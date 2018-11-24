using Android.Content;
using Android.Widget;
using Client.Filters;
using Client.Models;
using Client.Services;

namespace Client.Adapters
{
    public class CityArrayAdapter : BaseArrayAdapter<City, NoteService>
    {
        private readonly CityFilter _filter;

        public CityArrayAdapter(
            Context context,
            NoteService service,
            int resourceId = Android.Resource.Layout.SimpleListItem1,
            int textViewResourceId = Android.Resource.Id.Text1) : base(context, service, resourceId, textViewResourceId)
        {
            _filter = new CityFilter(this);
        }

        public override Filter Filter => _filter;
    }
}