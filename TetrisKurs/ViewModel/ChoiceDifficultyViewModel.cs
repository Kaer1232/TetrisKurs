using TetrisKurs.Base;
using TetrisKurs.View;

namespace TetrisKurs.ViewModel
{
    public partial class ChoiceDifficultyViewModel: ViewModelBase
    {
        private readonly MainPageViewModel _viewModel;

        public ChoiceDifficultyViewModel(MainPageViewModel viewModel)
        {
            _viewModel = viewModel;
            EasyBtmCommand = new Command(EasyGame);
            MiddleBtmCommand = new Command(MiddleGame);
            HardBtmCommand = new Command(HardGame);
            BackBtmCommand = new Command(BackMenu);
        }

        private void EasyGame()
        {

        }

        private async void MiddleGame()
        {
            await Shell.Current.GoToAsync(nameof(GameTitrisPageView));
        }

        private void HardGame()
        {

        }

        private void BackMenu()
        {
            _viewModel.Back();
        }

        private Command EasyBtmCommand;
        public Command MiddleBtmCommand { get; }
        private Command HardBtmCommand;
        public Command BackBtmCommand { get; }

    }
}
