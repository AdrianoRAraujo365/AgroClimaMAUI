namespace AgroClimaMaui.Models
{
    public class Estado
    {
        public int Id { get; set; }
        public string Sigla { get; set; }
        public string Nome { get; set; }
        public string Regiao { get; set; }
    }

    public class Regiao
    {
        public int Id { get; set; }
        public string Sigla { get; set; }
        public string Nome { get; set; }
    }
}
