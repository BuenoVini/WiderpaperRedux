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
            X = 100,
            Y = 100,
            MinimumWidth = 1000,
            MaximumWidth = 1000,
            MinimumHeight = 700,
            MaximumHeight = 700,
            Title = "Widerpaper Redux"
        };
    }
}