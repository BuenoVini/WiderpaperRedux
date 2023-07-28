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
            MinimumWidth = 450,
            MinimumHeight = 850,
            MaximumWidth = 450
        };
    }
}