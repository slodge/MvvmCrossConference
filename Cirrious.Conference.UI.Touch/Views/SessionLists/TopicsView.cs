using Cirrious.Conference.Core.ViewModels.SessionLists;
using Cirrious.MvvmCross.Views;

namespace Cirrious.Conference.UI.Touch.Views.SessionLists
{
    public class TopicsView
        : BaseSessionListView<TopicsViewModel, string>
    {
        public TopicsView(MvxShowViewModelRequest request)
            : base(request)
        {
        }
    }
}