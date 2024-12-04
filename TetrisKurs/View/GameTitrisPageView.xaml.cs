using TetrisKurs.Base;
using TetrisKurs.Model.GameModels;
using TetrisKurs.ViewModel.GameViewModels;
using TetrisKurs.ViewModels;
using This = TetrisKurs.View.GameTitrisPageView;


namespace TetrisKurs.View;

public partial class GameTitrisPageView : ContentPage
{
    private GameViewModel _viewModel;
    public GameTitrisPageView()
    {
        BindingContext = new GameViewModel();
        this.Game = new GameViewModel();
        this.InitializeComponent();
        This.SetupField(this.field, this.Game.Field.Cells, 30);
        This.SetupField(this.field, this.Game.NextField.Cells, 18);
        this.Game.Play();
    }
    private GameViewModel Game
    {
        get { return BindingContext as GameViewModel; }
        set { this.BindingContext = value; }
    }
    private static void SetupField(Grid field, CellViewModel[,] cells, byte blockSize)
    {
        for (int r = 0; r < cells.GetLength(0); r++)
            field.RowDefinitions.Add(new RowDefinition { Height = new GridLength(blockSize) });

        for (int c = 0; c < cells.GetLength(1); c++)
            field.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(blockSize) });

        foreach (var item in cells.WithIndex())
        {
            var brush = new SolidColorBrush();
            var control = new Label
            {
                BindingContext = item.Element,
                Background = brush.Color,
                VerticalTextAlignment = TextAlignment.Center,
            };
            control.SetBinding(Label.BackgroundColorProperty, ("Color.Value"));

            Grid.SetRow(control, item.X);
            Grid.SetColumn(control, item.Y);
            field.Children.Add(control);
        }
    }
    private void OnPanUpdated(object sender, PanUpdatedEventArgs e)
    {
        switch (e.StatusType)
        {
            case GestureStatus:
                // Если движение происходит, определим направление
                if (e.TotalX > 0)
                {
                    AttachEvent(2);
                    this.Game.Field.MoveTetrimino(MoveDirection.Right);
                }
                else if (e.TotalX < 0)
                {
                    AttachEvent(4);
                    this.Game.Field.MoveTetrimino(MoveDirection.Left);
                }
                else if (e.TotalY < 0)
                {
                    this.Game.Field.RotationTetrimino(RotationDirection.Right);
                }

                if (e.TotalY > 0)
                {
                    AttachEvent(3);
                    this.Game.Field.MoveTetrimino(MoveDirection.Down); break;
                }
                break;
        }
    }
    public void AttachEvent(int move)
    {
        switch (move)
        {
            case 1: this.Game.Field.RotationTetrimino(RotationDirection.Right); break;
            case 2: this.Game.Field.MoveTetrimino(MoveDirection.Right); break;
            case 3: this.Game.Field.MoveTetrimino(MoveDirection.Down); break;
            case 4: this.Game.Field.MoveTetrimino(MoveDirection.Left); break;
        }
    }
}