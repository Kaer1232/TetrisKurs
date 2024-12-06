using TetrisKurs.Base;
using TetrisKurs.View;
using Reactive.Bindings;
using TetrisKurs.ViewModel.GameViewModels;

namespace TetrisKurs.ViewModels
{
    public class GameViewModel : ViewModelBase
    {
        private Game Game { get; } = new Game();

        public FieldViewModel Field { get; }
        public GameResultViewModel Result { get; }

        public NextFieldViewModel NextField { get; }
        public IReadOnlyReactiveProperty<bool> IsPlaying => Game.IsPlaying;
        public IReadOnlyReactiveProperty<bool> IsOver => this.Game.IsOver;
        public Command PanUpdatedCommand { get; }

        public GameViewModel()
        {
            Result = new GameResultViewModel(Game.Result);
            Field = new FieldViewModel(Game.Field);
            NextField = new NextFieldViewModel(Game.NextTetrimino);
            PanUpdatedCommand = new Command<PanUpdatedEventArgs>(OnPanUpdated);
        }

        public void Play() => Game.Play();

        private void OnPanUpdated(PanUpdatedEventArgs e)
        {
            GameTitrisPageView gameTitrisPageView = new GameTitrisPageView();
            switch (e.StatusType)
            {
                case GestureStatus:
                    // Если движение происходит, определим направление
                    if (e.TotalX > 0)
                    {
                        gameTitrisPageView.AttachEvent(2);
                    }
                    else if (e.TotalX < 0)
                    {
                        gameTitrisPageView.AttachEvent(4);
                    }

                    if (e.TotalY > 0)
                    {
                        gameTitrisPageView.AttachEvent(3);
                        

                    }
                    break;
            }
        }
    }
}
