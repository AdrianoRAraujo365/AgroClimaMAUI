namespace AgroClimaMaui.Views
{
    public partial class ResultadosPage : ContentPage
    {
        public ResultadosPage()
        {
            InitializeComponent();
        }

        private async void OnNovaConsultaClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("///inicio");
        }
    }
}
