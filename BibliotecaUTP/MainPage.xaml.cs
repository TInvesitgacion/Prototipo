using PropertyChanged;
using Xamarin.Forms;

namespace BibliotecaUTP {

    [AddINotifyPropertyChangedInterface]
    public partial class MainPage : MasterDetailPage {

        public MainPage() {
            InitializeComponent();
            this.Master = new Master();
            this.Detail = new NavigationPage(new Detail());
            App.MasterDetailPage = this;

            BindingContext = this;
        }
    }
}
