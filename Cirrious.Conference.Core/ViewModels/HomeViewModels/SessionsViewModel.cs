using Cirrious.Conference.Core.ViewModels.SessionLists;
using Cirrious.MvvmCross.Commands;
using Cirrious.MvvmCross.Interfaces.Commands;

namespace Cirrious.Conference.Core.ViewModels.HomeViewModels
{
    public class SessionsViewModel
        : BaseConferenceViewModel
    {    
        public IMvxCommand ShowTopicsCommand
        {
            get { return new MvxRelayCommand(() => RequestNavigate<TopicsViewModel>()); }
        }
        public IMvxCommand ShowSpeakersCommand    
        {
            get { return new MvxRelayCommand(() => RequestNavigate<SpeakersViewModel>()); }
        }
        public IMvxCommand ShowSessionsCommand
        {
            get { return new MvxRelayCommand(() => RequestNavigate<SessionListViewModel>()); }
        }
    }
}