using TetrisKurs.ViewModel;

namespace TetrisKurs.View;

public partial class ChoiceDifficultyContentView : ContentView
{
	public ChoiceDifficultyContentView()
	{
		InitializeComponent();
		BindingContext = new ChoiceDifficultyViewModel();
	}
}