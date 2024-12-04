using CommunityToolkit.Mvvm.ComponentModel;

namespace TetrisKurs.Base
{
    public partial class ViewModelBase: ObservableObject
    {
        [ObservableProperty]
        string title;
    }
}
