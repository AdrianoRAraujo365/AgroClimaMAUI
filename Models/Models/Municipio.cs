namespace AgroClimaMaui.Models
{
    public class Municipio
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Microrregiao { get; set; }
        public string Mesorregiao { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string ProdutoAgricolaPrincipal { get; set; }
    }
}
