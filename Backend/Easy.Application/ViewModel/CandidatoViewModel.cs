namespace Easy.Application.ViewModel
{
    public class CandidatoViewModel
    {
        public CandidatoViewModel(string id, string urlPublica, string descricaoProfissional, string nome)
        {
            Id = id;
            UrlPublica = urlPublica;
            Nome = nome;
            DescricaoProfissional = descricaoProfissional;
        }

        public static CandidatoViewModel Create(string id, string urlPublica, string descricaoProfissional, string nome)
        {
            return new CandidatoViewModel(id, urlPublica, descricaoProfissional, nome);
        }

        public string Id { get; private set; }
        public string UrlPublica { get; private set; }
        public string DescricaoProfissional { get; private set; }
        public string Nome { get; private set; }
    }
}