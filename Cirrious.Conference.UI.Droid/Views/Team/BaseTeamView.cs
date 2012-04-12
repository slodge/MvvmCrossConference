using Cirrious.Conference.Core.ViewModels;

namespace Cirrious.Conference.UI.Droid.Views.Sponsors
{
    public class BaseTeamView<TViewModel> : BaseView<TViewModel> where TViewModel : BaseTeamViewModel
    {
        protected sealed override void OnViewModelSet()
        {
            SetContentView(Resource.Layout.Page_Team);
        }
   }
}