namespace Easy.Application.ViewModel
{
    public class CandidatoViewModel
    {
        public CandidatoViewModel(string id, string urlPublica, string cargo, string nome)
        {
            Id = id;
            UrlPublica = urlPublica;

            if (nome != null)
            {
                Nome = nome;
            }

            if (nome != null)
            {
                Cargo = cargo;
            }
        }

        public static CandidatoViewModel Create(string id, string urlPublica, string cargo, string nome)
        {
            return new CandidatoViewModel(id, urlPublica, cargo, nome);
        }

        public string Id { get; private set; }
        public string UrlPublica { get; private set; }
        public string Cargo { get; private set; }
        public string Nome { get; private set; }
    }
}