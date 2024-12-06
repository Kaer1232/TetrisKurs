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
            CurrentViewModel = new MenuContentView(this);
        }

        public void Start()
        {
            CurrentViewModel = new ChoiceDifficultyContentView(this);
        }

        public void Records()
        {
            CurrentViewModel = new ScoreContentView(this);
        }

        public void Back()
        {
            CurrentViewModel = new MenuContentView(this);
        }
    }
}
