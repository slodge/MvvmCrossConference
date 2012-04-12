using Cirrious.Conference.Core.ViewModels;
using Cirrious.MvvmCross.Views;

namespace Cirrious.Conference.UI.Touch.Views
{
    public class TeamView
        : BaseTeamView<TeamViewModel>
    {
        public TeamView(MvxShowViewModelRequest request)
            : base(request)
        {
        }
    }
}