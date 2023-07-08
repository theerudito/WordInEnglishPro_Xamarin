using WordInEnglishPro.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WordInEnglishPro.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Home : ContentPage
    {
        public Home()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            BindingContext = new VMHome(Navigation);
        }
    }
}