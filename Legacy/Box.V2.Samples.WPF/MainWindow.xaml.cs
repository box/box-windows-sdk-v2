using Box.V2.Samples.WPF.ViewModels;
using System.Windows;

namespace Box.V2.Samples.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MainViewModel _main;
      
        public MainWindow()
        {
            InitializeComponent();
            _main = ViewModelLocator.Main;

            oauth.AuthCodeReceived += async (s, e) =>
            {
                var auth = s as OAuth2Sample;
                if (auth == null)
                    return;

                await _main.Init(auth.AuthCode);
            };
        }

        private void OnLoad(object sender, RoutedEventArgs e)
        {
            if (!_main.IsLoggedIn)
                oauth.GetAuthCode(_main.Config.AuthCodeUri, _main.Config.RedirectUri);
        }
    }
}
