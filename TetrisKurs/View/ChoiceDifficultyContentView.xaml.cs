using TetrisKurs.ViewModel;

namespace TetrisKurs.View;

public partial class ChoiceDifficultyContentView : ContentView
{
	public ChoiceDifficultyContentView(MainPageViewModel mainPageViewModel)
	{
		InitializeComponent();
		BindingContext = new ChoiceDifficultyViewModel(mainPageViewModel);
	}
}