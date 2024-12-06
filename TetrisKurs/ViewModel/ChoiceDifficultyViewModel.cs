using TetrisKurs.Base;
using TetrisKurs.View;
using TetrisKurs.ViewModels;

namespace TetrisKurs.ViewModel
{
    public partial class ChoiceDifficultyViewModel: ViewModelBase
    {
        GameViewModel game = new GameViewModel();
        private readonly MainPageViewModel _viewModel;
        public ChoiceDifficultyViewModel(MainPageViewModel viewModel)
        {
            _viewModel = viewModel;
            EasyBtmCommand = new Command(EasyGame);
            MiddleBtmCommand = new Command(MiddleGame);
            HardBtmCommand = new Command(HardGame);
            BackBtmCommand = new Command(BackMenu);
        }

        private async void EasyGame()
        {   Choice = 1;
            game.Play(Choice);
            await Shell.Current.GoToAsync(nameof(GameTitrisPageView));
        }

        public async void MiddleGame()
        {
            game.Play(Choice);
            await Shell.Current.GoToAsync(nameof(GameTitrisPageView));
            
        }

        private async void HardGame()
        {
            game.Play(Choice);
            await Shell.Current.GoToAsync(nameof(GameTitrisPageView));
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
