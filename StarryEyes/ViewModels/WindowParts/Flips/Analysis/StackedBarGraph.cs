using System;
using System.Collections.ObjectModel;
using System.Linq;
using Livet;
using StarryEyes.ViewModels.Timelines.Statuses;

namespace StarryEyes.ViewModels.WindowParts.Flips.Analysis
{
    public class StackedBarGraph : ViewModel
    {
        private ObservableCollection<AnalysisUser> _users;
        public ObservableCollection<AnalysisUser> Users
        {
            get { return _users ?? (_users = new ObservableCollection<AnalysisUser>()); }
            set
            {
                if (value == _users)
                    return;

                _users = value;
                RaisePropertyChanged(() => Users);
            }
        }
        
        public int Max => Users.Max(user => user.Sum);

        private DateTime? _since;
        public DateTime? Since
        {
            get { return _since; }
            set
            {
                if (value == _since)
                    return;

                _since = value;
                RaisePropertyChanged(() => Since);
            }
        }

        private DateTime? _until;
        public DateTime? Until
        {
            get { return _until; }
            set
            {
                if (value == _until)
                    return;

                _until = value;
                RaisePropertyChanged(() => Until);
            }
        }
        
        public StackedBarGraph()
        {
            RouteUsersObservationToProperties();
        }

        private void RouteUsersObservationToProperties()
        {
            Users.CollectionChanged += (sender, args) => RaisePropertyChanged(() => Max);
        }

        public void RegisterStatus(StatusViewModel statusViewModel)
        {
            var userCreated = statusViewModel.IsRetweet ? statusViewModel.Retweeter : statusViewModel.User;

            var sUser = Users.FirstOrDefault(user => user.UserViewModel.Model.User.Id == userCreated.Model.User.Id);
            if (sUser == null)
            {
                sUser = new AnalysisUser(userCreated, this);
                Users.Add(sUser);
            }

            MarkDates(statusViewModel);

            if (statusViewModel.IsRetweet)
                sUser.RetweetCount++;
            else
                sUser.TweetCount++;
        }

        private void MarkDates(StatusViewModel statusViewModel)
        {
            if (Since == null || statusViewModel.CreatedAt < Since)
                Since = statusViewModel.CreatedAt;
            if (Until == null || statusViewModel.CreatedAt > Until)
                Until = statusViewModel.CreatedAt;
        }
    }
}