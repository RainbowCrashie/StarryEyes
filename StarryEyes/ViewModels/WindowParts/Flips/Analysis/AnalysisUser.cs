using Livet;
using StarryEyes.ViewModels.Timelines.Statuses;

namespace StarryEyes.ViewModels.WindowParts.Flips.Analysis
{
    public class AnalysisUser : ViewModel
    {
        public UserViewModel UserViewModel
        {
            get { return _userViewModel; }
            set
            {
                if (value == _userViewModel)
                    return;

                _userViewModel = value;
                RaisePropertyChanged(() => UserViewModel);
            }
        }
        private UserViewModel _userViewModel;

        public int TweetCount
        {
            get { return _tweetCount; }
            set
            {
                if (value == _tweetCount)
                    return;

                _tweetCount = value;
                RaisePropertyChanged(() => TweetCount);
            }
        }
        private int _tweetCount;

        public int RetweetCount
        {
            get { return _retweetCount; }
            set
            {
                if (value == _retweetCount)
                    return;

                _retweetCount = value;
                RaisePropertyChanged(() => RetweetCount);
            }
        }
        private int _retweetCount;

        public int Sum => TweetCount + RetweetCount;

        public StackedBarGraph Parent
        {
            get { return _parent; }
            set
            {
                if (value == _parent)
                    return;

                _parent = value;
                RaisePropertyChanged(() => Parent);
            }
        }
        private StackedBarGraph _parent;

        public AnalysisUser(UserViewModel userViewModel, StackedBarGraph parent) : this(userViewModel, 0, 0, parent) {}

        public AnalysisUser(UserViewModel userViewModel, int tweetCount, int retweetCount, StackedBarGraph parent)
        {
            UserViewModel = userViewModel;
            TweetCount = tweetCount;
            RetweetCount = retweetCount;
            Parent = parent;
        }
    }
}