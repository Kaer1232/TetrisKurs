using TetrisKurs.ViewModel;

namespace TetrisKurs.View;

public partial class MenuContentView : ContentView
{
	public MenuContentView(MainPageViewModel mainPageViewModel)
	{
		InitializeComponent();
		BindingContext = new MenuContentViewModel(mainPageViewModel);
	}
}