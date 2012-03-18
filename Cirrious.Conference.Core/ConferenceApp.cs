using Cirrious.Conference.Core.ApplicationObjects;
using Cirrious.Conference.Core.Interfaces;
using Cirrious.Conference.Core.Models;
using Cirrious.Conference.Core.Models.Twitter;
using Cirrious.MvvmCross.Application;
using Cirrious.MvvmCross.ExtensionMethods;
using Cirrious.MvvmCross.Interfaces.Localization;
using Cirrious.MvvmCross.Interfaces.ServiceProvider;
using Cirrious.MvvmCross.Interfaces.ViewModels;

namespace Cirrious.Conference.Core
{
    public class ConferenceApp
        : MvxApplication
        , IMvxServiceProducer<IMvxStartNavigation>
        , IMvxServiceProducer<IMvxTextProvider>
        , IMvxServiceProducer<IConferenceService>
        , IMvxServiceProducer<ITwitterSearchProvider>
    {
        public ConferenceApp()
        {
            InitialiseText();
            InitaliseServices();
            InitialiseStartNavigation();
        }

        private void InitaliseServices()
        {
            var repository = new ConferenceService();
            repository.BeginAsyncLoad();
            this.RegisterServiceInstance<IConferenceService>(repository);

            this.RegisterServiceInstance<ITwitterSearchProvider>(new TwitterSearchProvider());
        }

        private void InitialiseText()
        {
            var builder = new TextProviderBuilder();
            // TODO - could choose a language here: builder.LoadResources(whichLanguage);
            this.RegisterServiceInstance<IMvxTextProvider>(builder.TextProvider);
        }

        private void InitialiseStartNavigation()
        {
            var startApplicationObject = new StartApplicationObject();
            this.RegisterServiceInstance<IMvxStartNavigation>(startApplicationObject);
        }
    }
}
