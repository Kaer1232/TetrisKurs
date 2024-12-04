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
        }

        public void StartButtom()
        {
            _viewModel.Navigation();
        }

        public Command StartBtnCommannd { get; }
    }
}
