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
    public abstract class BaseConferenceApp 
        : MvxApplication
        , IMvxServiceProducer<IMvxStartNavigation>
        , IMvxServiceProducer<IConferenceStart>
        , IMvxServiceProducer<IMvxTextProvider>
        , IMvxServiceProducer<IConferenceService>
        , IMvxServiceProducer<ITwitterSearchProvider>
        , IMvxServiceProducer<IErrorReporter>
        , IMvxServiceProducer<IErrorSource>
        , IMvxServiceProducer<IApplicationSettings>
    {
        protected BaseConferenceApp(StartApplicationObject startApplicationObject)
        {
            InitialiseApplicationSettings();
            InitialiseText();
            InitaliseServices();
            InitaliseErrorSystem();
            InitialiseStartNavigation(startApplicationObject);
        }

        private void InitialiseApplicationSettings()
        {
            this.RegisterServiceInstance<IApplicationSettings>(new ApplicationSettings());
        }


        private void InitaliseErrorSystem()
        {
            var errorHub = new ErrorApplicationObject();
            this.RegisterServiceInstance<IErrorReporter>(errorHub);
            this.RegisterServiceInstance<IErrorSource>(errorHub);
        }

        private void InitaliseServices()
        {
            var repository = new ConferenceService();
            this.RegisterServiceInstance<IConferenceService>(repository);

            this.RegisterServiceInstance<ITwitterSearchProvider>(new TwitterSearchProvider());
        }

        private void InitialiseText()
        {
            var builder = new TextProviderBuilder();
            // TODO - could choose a language here: builder.LoadResources(whichLanguage);
            this.RegisterServiceInstance<IMvxTextProvider>(builder.TextProvider);
        }

        protected void InitialiseStartNavigation(StartApplicationObject startApplicationObject)
        {
            this.RegisterServiceInstance<IConferenceStart>(startApplicationObject);
            this.RegisterServiceInstance<IMvxStartNavigation>(startApplicationObject);
        }
    }

    public class ConferenceApp
        : BaseConferenceApp
    {
        public ConferenceApp()
            : base(new StartApplicationObject(true))
        {
        }
    }

    public class NoSplashScreenConferenceApp
        : BaseConferenceApp
    {
        public NoSplashScreenConferenceApp()
            : base(new StartApplicationObject(false))
        {
        }
    }
}
