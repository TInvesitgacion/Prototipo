using BibliotecaUTP.Models;
using BibliotecaUTP.Services;
using PropertyChanged;
using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace BibliotecaUTP {
    [AddINotifyPropertyChangedInterface]
    public partial class Detail : ContentPage {
        private Biblioteca biblioteca;
        public List<Documentos> documentos { get; set; }

        private RSS rss;
        public int NumPage { get; set; }
        public int MaxNumPage { get; set; }

        private string busqueda;
        private string campo;
        private string sede;
        private string tipo;

        public Detail() {
            InitializeComponent();
            this.BindingContext = this;
            rss = new RSS();
            NumPage = 1;
            MaxNumPage = 1;
        }

        public Detail(string busqueda, string campo, string sede, string tipo) {
            InitializeComponent();
            this.BindingContext = this;

            rss = new RSS();
            NumPage = 1;

            this.busqueda = busqueda;
            this.campo = campo;
            this.sede = sede;
            this.tipo = tipo;
            biblioteca = rss.GetBiblioteca(busqueda, campo, sede, tipo, NumPage);
            this.documentos = biblioteca.Documentos;

            if (biblioteca.TotalResultado > 20) {
                MaxNumPage = Convert.ToInt32(biblioteca.TotalResultado / 20);
                if (biblioteca.TotalResultado % 20 != 0)
                    MaxNumPage++;
            }
            if (MaxNumPage <= 0) { MaxNumPage = 1; }
        }

        void OnSelectionChanged(object sender, SelectionChangedEventArgs e) {
            Documentos selectedItem = e.CurrentSelection[0] as Documentos;
        }

        private void btnRight_Clicked(object sender, EventArgs e) {
            if (NumPage + 1 <= MaxNumPage) {
                NumPage++;
                this.documentos = rss.GetBiblioteca(busqueda, campo, sede, tipo, NumPage).Documentos;
            }
        }

        private void btnLeft_Clicked(object sender, EventArgs e) {
            if (NumPage - 1 >= 1) {
                NumPage--;
                this.documentos = rss.GetBiblioteca(busqueda, campo, sede, tipo, NumPage).Documentos;
            }
        }

        private void txtPage_Unfocused(object sender, FocusEventArgs e) {
            if (Convert.ToInt32(txtPage.Text) > MaxNumPage) {
                txtPage.Text = MaxNumPage.ToString();
            } else if (Convert.ToInt32(txtPage.Text) < 1) {
                txtPage.Text = "1";
            }
            if (!string.IsNullOrEmpty(busqueda)) {
                this.documentos = rss.GetBiblioteca(busqueda, campo, sede, tipo, NumPage).Documentos;
            }
        }
    }
}