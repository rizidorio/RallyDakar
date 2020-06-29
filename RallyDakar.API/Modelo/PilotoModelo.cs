namespace RallyDakar.API.Modelo
{
    public class PilotoModelo
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }

        public int EquipeId { get; set; }

        public string NomeCompleto {
            get { return $"{Nome} {Sobrenome}"; }
        }
    }
}
