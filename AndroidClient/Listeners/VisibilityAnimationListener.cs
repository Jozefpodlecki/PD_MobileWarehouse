using Android.Views;
using Android.Views.Animations;
using static Android.Views.Animations.Animation;

namespace Client.Listeners
{
    public class VisibilityAnimationListener : Java.Lang.Object, IAnimationListener
    {
        private readonly View _view;
        private readonly ViewStates _viewState;

        public VisibilityAnimationListener(View view, ViewStates viewState)
        {
            _view = view;
            _viewState = viewState;
        }

        public void OnAnimationEnd(Animation animation)
        {
            _view.Visibility = _viewState;
        }

        public void OnAnimationRepeat(Animation animation)
        {

        }

        public void OnAnimationStart(Animation animation)
        {

        }
    }
}