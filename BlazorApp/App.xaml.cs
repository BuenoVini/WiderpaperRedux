namespace BlazorApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new MainPage();
        }

        protected override Window CreateWindow(IActivationState activationState)
        => new Window(MainPage)
        {
            Width = 1280,
            Height = 720,
            X = 100,
            Y = 100,
            MinimumWidth = 854,
            MinimumHeight = 480,
        };
    }
}