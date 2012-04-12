namespace Cirrious.Conference.Core.ViewModels
{
    public class TeamViewModel : BaseTeamViewModel
    {
        public TeamViewModel()
        {
            LoadFrom(Service.Team.Values);
        }        
    }
}