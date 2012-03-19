using Cirrious.Conference.Core;
using Cirrious.Conference.Core.Converters;
using Cirrious.MvvmCross.Application;
using Cirrious.MvvmCross.Dialog.Touch;
using Cirrious.MvvmCross.Touch.Interfaces;
using Cirrious.MvvmCross.Touch.Platform;
using Cirrious.MvvmCross.Binding.Binders;

namespace Cirrious.Conference.UI.Touch
{
   public class Setup
        : MvxTouchDialogBindingSetup
    {
        public Setup(MvxApplicationDelegate applicationDelegate, IMvxTouchViewPresenter presenter)
            : base(applicationDelegate, presenter)
        {
        }

        #region Overrides of MvxBaseSetup
				
        protected override MvxApplication CreateApp()
        {
            var app = new ConferenceApp();
            return app;
        }

		protected override void FillValueConverters(Cirrious.MvvmCross.Binding.Interfaces.Binders.IMvxValueConverterRegistry registry)
        {
            base.FillValueConverters(registry);

            var filler = new MvxInstanceBasedValueConverterRegistryFiller(registry);
            filler.AddFieldConverters(typeof(Converters));
        }

        #endregion
    }

}

