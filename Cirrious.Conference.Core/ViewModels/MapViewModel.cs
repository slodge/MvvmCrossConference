using Cirrious.MvvmCross.Commands;
using Cirrious.MvvmCross.ExtensionMethods;
using Cirrious.MvvmCross.Interfaces.Commands;
using Cirrious.MvvmCross.Interfaces.Platform.Tasks;

namespace Cirrious.Conference.Core.ViewModels
{
    public class MapViewModel
        : BaseViewModel
    {
        public string Name { get { return "University of the West of England (UWE)"; } }
        public string Address { get { return "Bristol BS16 1QY"; } }        
        public string LocationWebPage { get { return "http://www.dddsouthwest.com/"; } }

        public string Phone { get { return ""; } }
        public string Email { get { return "admin@dddsouthwest.com"; } }
        public double Latitude { get { return 51.499954; } }
        public double Longitude { get { return -2.54694; } }

        public IMvxCommand PhoneCommand
        {
            get
            {
                return new MvxRelayCommand(() => MakePhoneCall(Name, Phone));
            }
        }

        public IMvxCommand EmailCommand
        {
            get
            {
                return new MvxRelayCommand(() => ComposeEmail(Email, "About DDDSW", "About DDDSW"));
            }
        }

        public IMvxCommand WebPageCommand
        {
            get
            {
                return new MvxRelayCommand(() => ShowWebPage(LocationWebPage));
            }
        }
    }
}