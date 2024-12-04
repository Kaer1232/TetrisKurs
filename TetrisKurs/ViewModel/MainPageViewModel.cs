using TetrisKurs.Base;
using TetrisKurs.View;

namespace TetrisKurs.ViewModel
{
    public class MainPageViewModel: ViewModelBase
    {
        private ContentView _currentViewModel;

        public ContentView CurrentViewModel
        {
            get => _currentViewModel;
            set
            {
                _currentViewModel = value;
                OnPropertyChanged();
            }
        }
        public MainPageViewModel()
        { 
            Title = "Tetris";
            CurrentViewModel = new MenuContentView { BindingContext = new MenuContentViewModel(this)};
        }

        public void Navigation()
        {
            CurrentViewModel = new ChoiceDifficultyContentView();
        }
    }
}
