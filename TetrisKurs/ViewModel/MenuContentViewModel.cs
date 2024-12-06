using TetrisKurs.Base;

namespace TetrisKurs.ViewModel
{
    class MenuContentViewModel: ViewModelBase
    {
        private readonly MainPageViewModel _viewModel;
        public MenuContentViewModel(MainPageViewModel viewModel)
        {
            _viewModel = viewModel;
            StartBtnCommannd = new Command(StartButtom);
            RecordsBtnCommannd = new Command(RecordsButtom);

        }

        public void StartButtom()
        {
            _viewModel.Start();
        }

        public void RecordsButtom()
        {
            _viewModel.Records();
        }

        public Command StartBtnCommannd { get; }
        public Command RecordsBtnCommannd { get; }

    }
}
