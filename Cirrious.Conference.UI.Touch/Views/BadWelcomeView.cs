using Cirrious.Conference.Core.ViewModels.HomeViewModels;
using Cirrious.MvvmCross.Binding.Touch.Views;
using Cirrious.MvvmCross.Views;

namespace Cirrious.Conference.UI.Touch.Views
{
    public class BadWelcomeView
        : MvxBindingTouchViewController<WelcomeViewModel>
    {
        protected BadWelcomeView(MvxShowViewModelRequest request) : base(request)
        {
        }
    }
}