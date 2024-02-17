using PhoneNumberExcercise.ViewModel;

namespace PhoneNumberExcercise
{
    public partial class MainPage : ContentPage
    {
        public MainPage(MainViewModel mainViewModel)
        {
            InitializeComponent();
            BindingContext = mainViewModel;
        }
    }
}
