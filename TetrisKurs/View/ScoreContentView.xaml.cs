using TetrisKurs.ViewModel;

namespace TetrisKurs.View;

public partial class ScoreContentView : ContentView
{
	public ScoreContentView(MainPageViewModel mainPageViewModel)
	{
		InitializeComponent();
		BindingContext = new ScoreViewModel(mainPageViewModel);
	}
}