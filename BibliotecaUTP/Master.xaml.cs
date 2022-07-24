using BibliotecaUTP.Models;
using BibliotecaUTP.Services;
using PropertyChanged;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BibliotecaUTP {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [AddINotifyPropertyChangedInterface]
    public partial class Master : ContentPage {

        public Master() {
            InitializeComponent();
        }

        private void Button_Clicked(object sender, EventArgs e) {
            if (!string.IsNullOrEmpty(txtBusqueda.Text)) {
                RSS rss = new RSS();
                App.MasterDetailPage.IsPresented = false;

                App.MasterDetailPage.Detail = new NavigationPage(new Detail(txtBusqueda.Text, ((PickerValue)pckCampo.SelectedItem).key, ((PickerValue)pckSede.SelectedItem).key, ((PickerValue)pckTipo.SelectedItem).key));
            } else {
                DisplayAlert("Búsqueda", "Ingrese la bibliografia a buscar", "OK");
            }
        }
    }
}