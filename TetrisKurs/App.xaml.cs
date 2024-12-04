using TetrisKurs.View;

namespace TetrisKurs
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(GameTitrisPageView), typeof(GameTitrisPageView));
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new AppShell());
        }
    }
}