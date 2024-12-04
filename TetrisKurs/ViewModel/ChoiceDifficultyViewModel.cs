using TetrisKurs.Base;
using TetrisKurs.View;

namespace TetrisKurs.ViewModel
{
    public partial class ChoiceDifficultyViewModel: ViewModelBase
    {

        public ChoiceDifficultyViewModel()
        {
            EasyBtmCommand = new Command(EasyGame);
            MiddleBtmCommand = new Command(MiddleGame);
            HardBtmCommand = new Command(HardGame);
        }

        private void EasyGame()
        {

        }

        private async void MiddleGame()
        {
            try
            { 
                await Shell.Current.GoToAsync(nameof(GameTitrisPageView));
            }
            catch (Exception ex)
            {
                // Логирование или отладка
                Console.WriteLine($"Ошибка навигации: {ex.Message}");
            }
           
        }

        private void HardGame()
        {

        }

        private Command EasyBtmCommand;
        private Command MiddleBtmCommand;
        private Command HardBtmCommand;

    }
}
