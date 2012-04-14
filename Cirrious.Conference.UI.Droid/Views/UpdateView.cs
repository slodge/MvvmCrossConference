using Android.App;
using Cirrious.Conference.Core.ViewModels;

namespace Cirrious.Conference.UI.Droid.Views
{
    [Activity(Label = "DDD SouthWest", NoHistory = true)]
    public class UpdateView : BaseView<UpdateViewModel>
    {
        protected override void OnViewModelSet()
        {
            this.SetContentView(Resource.Layout.Page_Update);
        }
    }
}