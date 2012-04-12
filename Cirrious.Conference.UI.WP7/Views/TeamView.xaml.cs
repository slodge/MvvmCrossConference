using Cirrious.Conference.Core.ViewModels;

namespace Cirrious.Conference.UI.WP7.Views
{
    public class BaseTeamView : BaseView<TeamViewModel>
    {
    }

    public partial class TeamView : BaseTeamView
    {
        public TeamView()
        {
            InitializeComponent();
        }
    }
}