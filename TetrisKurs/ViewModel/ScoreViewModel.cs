using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using System.Diagnostics;
using TetrisKurs.Base;
using TetrisKurs.Data;
using TetrisKurs.Model;

namespace TetrisKurs.ViewModel
{
    public partial class ScoreViewModel: ObservableObject
    {
        private readonly AppDbContext _dbContext;

        public ObservableCollection<RecordsModel> Top5Records { get; } = new ObservableCollection<RecordsModel>();


        private readonly MainPageViewModel _viewModel;

        public ScoreViewModel(MainPageViewModel viewModel)
        {
            _viewModel = viewModel;
            BackBtmCommand = new Command(BackMenu);
            _dbContext = new AppDbContext();
            _dbContext.Database.EnsureCreated();

            LoadTop5Records();

        }

        private void LoadTop5Records()
        {
            var records = _dbContext.Records.OrderByDescending(r => r.Score).Take(5).ToList();
            Top5Records.Clear();
            foreach (var record in records)
            {
                Top5Records.Add(record);
            }
        }

        private void BackMenu()
        {
            _viewModel.Back();
        }
        public Command BackBtmCommand { get; }
    }
}
