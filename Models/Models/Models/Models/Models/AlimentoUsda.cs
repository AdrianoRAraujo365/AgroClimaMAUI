namespace AgroClimaMaui.Models
{
    public class AlimentoUsda
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public string Categoria { get; set; }
        public List<Nutriente> Nutrientes { get; set; }
        public FichaTecnica FichaTecnica { get; set; }
    }

    public class Nutriente
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public double Quantidade { get; set; }
        public string Unidade { get; set; }
        public string Categoria { get; set; }
    }

    public class FichaTecnica
    {
        public double Calorias { get; set; }
        public double Proteina { get; set; }
        public double Gordura { get; set; }
        public double Carboidrato { get; set; }
        public double Fibra { get; set; }
        public double Acucar { get; set; }
        public double Sodio { get; set; }
        public string Porcao { get; set; }
    }
