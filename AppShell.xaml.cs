using AgroClimaMaui.Views;

namespace AgroClimaMaui
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute("inicio", typeof(InicioPage));
            Routing.RegisterRoute("clima", typeof(ClimaPage));
            Routing.RegisterRoute("nutricao", typeof(NutricaoPage));
            Routing.RegisterRoute("resultados", typeof(ResultadosPage));
        }
    }
}
