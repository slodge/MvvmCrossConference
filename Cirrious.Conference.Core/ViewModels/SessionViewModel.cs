using Cirrious.Conference.Core.Models;
using Cirrious.MvvmCross.Commands;
using Cirrious.MvvmCross.ExtensionMethods;
using Cirrious.MvvmCross.Interfaces.Commands;
using Cirrious.MvvmCross.Interfaces.Platform.Tasks;
using Cirrious.MvvmCross.Interfaces.ServiceProvider;

namespace Cirrious.Conference.Core.ViewModels
{
    public class SessionViewModel
        : BaseConferenceViewModel
        , IMvxServiceConsumer<IMvxShareTask>
    {
        public SessionViewModel(string key)
        {
            SessionWithFavoriteFlag session;
            if (!Service.Sessions.TryGetValue(key, out session))
            {
#warning TODO - report error!
                RequestClose(this);
                return;
            }

            Session = session;
        }

        public SessionWithFavoriteFlag Session { get; private set; }

        public IMvxCommand ShareCommand
        {
            get { return new MvxRelayCommand(Share); }
        }

        public IMvxCommand ShowWebsiteCommand
        {
            get { return new MvxRelayCommand(() => base.ShowWebPage(Session.Session.SpeakerWebsiteURL)); }
        }

        private void Share()
        {
            string prefix = string.Empty;
            if (!string.IsNullOrEmpty(Session.Session.SpeakerTwitterName))
            {
                prefix = "@" + Session.Session.SpeakerTwitterName + " ";
            }

            var service = this.GetService<IMvxShareTask>();
            var toShare = string.Format("{0}#dddsw {1}, {2}", prefix, Session.Session.Speaker, Session.Session.Title);
            if (toShare.Length > 140)
                toShare = toShare.Substring(0, 135).Trim() + "...";
            service.ShareShort(toShare);
        }
    }
}