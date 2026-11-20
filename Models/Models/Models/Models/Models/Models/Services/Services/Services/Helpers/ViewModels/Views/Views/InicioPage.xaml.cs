💻 Visualizador de Arquivos

Views/InicioPage.xaml.cs
using AgroClimaMaui.Helpers;
using AgroClimaMaui.Models;
using AgroClimaMaui.ViewModels;

namespace AgroClimaMaui.Views
{
    public partial class InicioPage : ContentPage
    {
        private readonly MainViewModel _viewModel;

        public InicioPage()
        {
            InitializeComponent();
            _viewModel = BindingContext as MainViewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.CarregarEstadosCommand.Execute(null);
        }

        private async void OnEstadoSelected(object sender, EventArgs e)
        {
            var picker = sender as Picker;
            if (picker?.SelectedItem is Estado estado)
                await _viewModel.CarregarMunicipiosCommand.ExecuteAsync(estado.Sigla);
        }

        private async void OnMunicipioSelected(object sender, EventArgs e)
        {
            var picker = sender as Picker;
            if (picker?.SelectedItem is Municipio municipio)
                await _viewModel.CarregarProdutosCommand.ExecuteAsync(municipio.Id);
        }

        // Métodos POO
        public FormularioModel GetForm() => FormHelper.GetForm(PickerEstado, PickerMunicipio, PickerProduto);
        public void SetForm(FormularioModel dados) => FormHelper.SetForm(PickerEstado, PickerMunicipio, PickerProduto, dados);
        public void ClearForm() => FormHelper.ClearForm(PickerEstado, PickerMunicipio, PickerProduto);
        public (bool IsValid, string Mensagem) ValidateForm() => FormHelper.ValidateForm(PickerEstado, PickerMunicipio);
    }
}
