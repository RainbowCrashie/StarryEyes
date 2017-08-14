using System;
using System.Collections.ObjectModel;
using System.Linq;
using StarryEyes.Models;
using StarryEyes.ViewModels.Timelines.Tabs;

namespace StarryEyes.ViewModels.WindowParts.Flips.Analysis
{
    public class AnalysisFlipViewModel : PartialFlipViewModelBase
    {
        #region Fields and Properties

        protected override bool IsWindowCommandsRelated
        {
            get { return false; }
        }

        public StackedBarGraph Graph
        {
            get { return _graph; }
            set
            {
                if (value == _graph)
                    return;

                _graph = value;
                RaisePropertyChanged(() => Graph);
            }
        }
        private StackedBarGraph _graph;

        private bool _isInProgress;
        public bool IsInProgress
        {
            get { return _isInProgress; }
            set
            {
                if (value == _isInProgress)
                    return;

                _isInProgress = value;
                RaisePropertyChanged(() => IsInProgress);
            }
        }

        #endregion

        #region Ctor

        public AnalysisFlipViewModel()
        {
        }
        #endregion

        #region Methods

        public override void Open()
        {
            if (IsVisible) return;
            base.Open();
        }
        public override void Close()
        {
            if (!IsVisible) return;
            base.Close();
            MainWindowModel.SetFocusTo(FocusRequest.Timeline);
        }

        public void OpenStatisticsFlip(TabViewModel tl)
        {
            Open();
            Analyze(tl);
        }

        public void Analyze(TabViewModel tl)
        {
            IsInProgress = true;

            Graph = new StackedBarGraph();

            var allStatuses = tl.Timeline;
            Console.WriteLine($"{tl.Name}: {allStatuses.Count}");

            //filter statuses here
            var now = DateTime.Now;
            var filteredStatuses = allStatuses.Where(status => now - status.CreatedAt < TimeSpan.FromMinutes(30));

            foreach (var statusViewModel in filteredStatuses)
            {
                Graph.RegisterStatus(statusViewModel);
            }

            Graph.Users = new ObservableCollection<AnalysisUser>(Graph.Users.OrderByDescending(user => user.Sum));

            IsInProgress = false;
        }

        private Livet.Commands.ViewModelCommand _closeCommand;
        public Livet.Commands.ViewModelCommand CloseCommand => _closeCommand ?? (_closeCommand = new Livet.Commands.ViewModelCommand(Close));

        
        #endregion

    }
}