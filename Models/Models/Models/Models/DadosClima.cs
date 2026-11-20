namespace AgroClimaMaui.Models
{
    public class DadosClima
    {
        public double TemperaturaMedia { get; set; }
        public double TemperaturaMaxima { get; set; }
        public double TemperaturaMinima { get; set; }
        public double Umidade { get; set; }
        public double PressaoAtmosferica { get; set; }
        public string Descricao { get; set; }
        public string Icone { get; set; }
        public DateTime DataAtualizacao { get; set; }
        public List<double[,]> MapaCalor { get; set; }
    }

    public class MapaCalor
    {
        public double[,] Dados { get; set; }
        public int Linhas { get; set; }
        public int Colunas { get; set; }
        public double ValorMinimo { get; set; }
        public double ValorMaximo { get; set; }
    }
